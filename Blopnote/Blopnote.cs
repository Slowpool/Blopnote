using Blopnote.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using Microsoft.VisualBasic;

namespace Blopnote
{
    public partial class Blopnote : Form
    {
        private readonly TextField textField;
        private readonly FileProcessor fileProcessor;
        private readonly FileState fileState;
        private readonly CreateNewTranslationForm createNewTranslationForm;
        private readonly LyricsBox lyricsBox;
        private readonly SizeRegulator sizeRegulator;

        private Size WorkSpace => new Size(ClientSize.Width, ClientSize.Height - menuStrip1.Height - statusStrip1.Height);

        private const int AUTOSAVE_FREQUENCY_IN_SECONDS = 5;
        private const string CONFIG_FOLDER_ATTRIBUTE = "folderWithTranslationsPath";
        private int PreviousLineWithCarriage { get; set; }
        public Blopnote()
        {
            Logger.Instance.Log(LogType.EventHandler,"Blopnote constructor invoked");

            InitializeComponent();
            this.Icon = Resources.icon;
            openFileDialog1.Filter = ".txt files(*.txt)|*.txt";
            timerAutoSave.Interval = AUTOSAVE_FREQUENCY_IN_SECONDS * 1000;

            textField = new TextField(TextBoxWithText);
            fileState = new FileState(status, textField);
            lyricsBox = new LyricsBox(PanelForLyricsBox, TextBoxWithText.Font, VScrollBarForLyrics);
            fileProcessor = new FileProcessor(textField, fileState, lyricsBox, openFileDialog1);

            changeFolderToolStripMenuItem.Click += fileProcessor.ChangeDirecrotyHandler;
            tabTranslatesOnly1LineToolStripMenuItem.Click += lyricsBox.SwitchTabMode;
            createToolStripMenuItem.Click += lyricsBox.ResetScrollBar;
            openToolStripMenuItem.Click += lyricsBox.ResetScrollBar;

            lyricsBox.TranslationByGoogleLoaded += textField.TranslationByGoogleLoaded;

            textField.SongIsWritten += fileProcessor.SongIsWritten_Handler;
            fileProcessor.DirectoryChanged += (sender, e) =>
            {
                createNewTranslationForm.UpdateMaxLength(fileState.DirectoryLength);
            };

            fileState.UrlChanged += (sender, e) =>
            {
                followToolStripMenuItem.Enabled = fileState.IsUrlUsed;
            };

            fileState.FileOpenedOrClosed += (opened) =>
            {
                textField.Clear();

                closeToolStripMenuItem.Enabled = opened;
                changeFolderToolStripMenuItem.Enabled = !opened;
                changeUrlToolStripMenuItem.Enabled = opened;
                changeLyricsToolStripMenuItem.Enabled = opened;
                uselessToolStripMenuItem.Enabled = opened;
            };

            fileState.LyricsChanged += (sender, e) =>
            {
                if (fileState.IsLyricsUsed)
                {
                    lyricsBox.FilterAndStore(fileState.Lyrics);
                    if (!fileState.songInfo.Completed)
                    {
                        textField.ObserveCompletion();
                        textField.LinesToComplete = lyricsBox.LinesQuantity;
                    }
                    TryAutoCompleteText();
                }

                ShowLyrics.Enabled = fileState.IsLyricsUsed;
                // Auto enabling of lyrics when user entered it
                if (!ShowLyrics.Checked)
                {
                    ShowLyrics.PerformClick();
                }
            };

            ImportXmlToolStripMenuItem.Click += fileProcessor.ImportXmlHandler;

            createNewTranslationForm = new CreateNewTranslationForm();
            sizeRegulator = new SizeRegulator(lyricsBox, textField);

            textField.PlaceOnce(topMargin: menuStrip1.Height);

            Logger.Instance.Log(LogType.EventHandler, "Blopnote constructor finished");
        }

        #region FormEvents
        private void Blopnote_Load(object sender, EventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote load invoked");
            fileState.CloseFile();
            lyricsBox.Hide();

            sizeRegulator.RegulateTo(WorkSpace);
            Logger.Instance.Log(LogType.EventHandler, "Blopnote load finished");
        }

        private void Blopnote_Shown(object sender, EventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote shown invoked");

            Enabled = false;
            string directoryFullName = ConfigurationManager.AppSettings.Get(CONFIG_FOLDER_ATTRIBUTE);
            if (string.IsNullOrEmpty(directoryFullName))
            {
                if (!fileProcessor.AskPath(PathTypesToAsk.TranslationsDirectory, out directoryFullName))
                {
                    this.Close();
                    Application.Exit();
                }
                else
                {
                    UpdateConfig(CONFIG_FOLDER_ATTRIBUTE, directoryFullName);
                }
            }
            fileProcessor.SetInitialDirectory(directoryFullName);
            Enabled = true;

            Logger.Instance.Log(LogType.EventHandler, "Blopnote shown finished");
        }

        private void Blopnote_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote form closing invoked");

            closeToolStripMenuItem.PerformClick();
            // I don't like it. Wanna better approach, e.g. ~Browser (destructor)
            try
            {
                Browser.Instance.Dispose();
            }
            catch
            { }

            Logger.Instance.Log(LogType.EventHandler, "Blopnote form closing finished");
        }

        private void Blopnote_SizeChanged(object sender, EventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote size changed invoked");
            RegulateTextAndLyricsBoxes();
            Logger.Instance.Log(LogType.EventHandler, "Blopnote size changed finished");
        }
        #endregion

        private void RegulateTextAndLyricsBoxes()
        {
            Logger.Instance.Log(LogType.Method, "Blopnote regulate text and lyrics boxes invoked");

            sizeRegulator.RegulateTo(WorkSpace);
            lyricsBox.Left = TextBoxWithText.Right;

            Logger.Instance.Log(LogType.Method, "Blopnote regulate text and lyrics boxes finished");
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote create file click invoked");

            if (createNewTranslationForm.ShowForDataInput() == DialogResult.OK)
            {
#warning awful + dirty code
                HandleInsertedData();
                PrepareComponentsToDisplayTranslation();
            }

            Logger.Instance.Log(LogType.EventHandler, "Blopnote create file click finished");
        }

        private void HandleInsertedData()
        {
            Logger.Instance.Log(LogType.Method, "Blopnote handle inserted data invoked");

            try
            {
                fileProcessor.CreateNewTranslation(createNewTranslationForm.fileInfo, createNewTranslationForm.songInfo);
            }
            catch (Exception e)
            {
                MessageBox.Show(caption: "File error",
                    text: "File wasn't created.\nCause: " + e.Message);
            }

            Logger.Instance.Log(LogType.Method, "Blopnote handle inserted data finished");
        }

        private void PrepareComponentsToDisplayTranslation()
        {
            Logger.Instance.Log(LogType.Method, "Blopnote prepare components to display translation invoked");

            textField.Enable();
            
// Q: maybe there's no need to use this?
// A: there is.
// Q: maybe it's still worth it to remove it and leave just textField.Clear()
            //if (clearText)
            //{
            //    textField.Clear();
            //}

            RegulateTextAndLyricsBoxes();

            Logger.Instance.Log(LogType.Method, "Blopnote prepare components to display translation finished");
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote open file invoked");

            DialogResult answer = openFileDialog1.ShowDialog();
#warning maybe i should to handle an opened and empty file in different ways?
            if (answer == DialogResult.OK)
            {
                textField.StopObserving();
                StopTimerAndTrySaveFile(mandatorySave: false);
                //lyricsBox.EnsureCleared();

                fileProcessor.OpenTranslation(openFileDialog1.FileName);
                PrepareComponentsToDisplayTranslation();

                textField.Focus();
            }

            Logger.Instance.Log(LogType.EventHandler, "Blopnote open file finished");
        }

        private void UpdateConfig(string key, string value)
        {
            Logger.Instance.Log(LogType.Method, "Blopnote config updating invoked");

            var config = ConfigurationManager.OpenExeConfiguration(Path.Combine(Environment.CurrentDirectory, "Blopnote.exe"));
            config.AppSettings.Settings[key].Value = value;
            config.Save();

            Logger.Instance.Log(LogType.Method, "Blopnote config updating finished");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote close file invoked");

            StopTimerAndTrySaveFile(true);
            fileState.CloseFile();
            RegulateTextAndLyricsBoxes();

            Logger.Instance.Log(LogType.EventHandler, "Blopnote close file finished");
        }

        private void ShowLyrics_Click(object sender, EventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote show lyrics clicked invoked");

            if (ShowLyrics.Checked)
            {
                lyricsBox.Display();
            }
            else
            {
                lyricsBox.Hide();
            }
            RegulateTextAndLyricsBoxes();

            Logger.Instance.Log(LogType.EventHandler, "Blopnote show lyrics clicked finished");
        }

        private void ShowLyrics_EnabledChanged(object sender, EventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote show lyrics enabled changed invoked");

#warning why is it here?
            if (!ShowLyrics.Enabled)
            {
                ShowLyrics.Checked = false;
                lyricsBox.NoLyrics();
            }

            Logger.Instance.Log(LogType.EventHandler, "Blopnote show lyrics enabled changed finished");
        }

        private void timerAutoSave_Tick(object sender, EventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote timer for auto save ticked invoked");

            StopTimerAndTrySaveFile(true);

            Logger.Instance.Log(LogType.EventHandler, "Blopnote timer for auto save ticked finished");
        }

        private void StopTimerAndTrySaveFile(bool mandatorySave)
        {
            Logger.Instance.Log(LogType.Method, "Blopnote stop timer and try save file invoked");

            timerAutoSave.Stop();
            try
            {
                fileProcessor.Save();
            }
            catch (Exception exception)
            {
                if (mandatorySave)
                {
                    MessageBox.Show(caption: "File saving error",
                                    text: "Error text: " + exception.Message);
                }
            }

            Logger.Instance.Log(LogType.Method, "Blopnote stop timer and try save file finished");
        }

        private void TextBoxWithText_KeyDown(object sender, KeyEventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote TextBoxWithText_KeyDown invoked");

            if (e.KeyData == (Keys.Control | Keys.C) && TextBoxWithText.SelectedText.Length == 0)
            {
                e.SuppressKeyPress = true;
                textField.CopyCurrentLineToClipBoard();
            }

            if (ShowLyrics.Checked && e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                TextBoxWithText.AppendText("\n");
                TextBoxWithText.SelectionStart = TextBoxWithText.Text.Length;
                TryAutoCompleteText();
            }

            Logger.Instance.Log(LogType.Method, "Blopnote TextBoxWithText_KeyDown finished");
        }

        // It's pointless to process ctrl+w in current version of app, but it's an another way to
        // process the hotkey everywhere in application.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote ProcessCmdKey invoked");

            if (keyData == (Keys.Control | Keys.W))
            {
                Application.Exit();
                return true;
            }

            Logger.Instance.Log(LogType.EventHandler, "Blopnote ProcessCmdKey finished");
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void TextBoxWithText_KeyPress(object sender, KeyPressEventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote TextBoxWithText_KeyPress invoked");

            timerAutoSave.Start();

            Logger.Instance.Log(LogType.EventHandler, "Blopnote TextBoxWithText_KeyPress finished");
        }

        private void HighlightActualLine()
        {
            Logger.Instance.Log(LogType.Method, "Blopnote HighlightActualLine invoked");

            lyricsBox.HighlightAt(textField.LineWithCarriage);

            Logger.Instance.Log(LogType.Method, "Blopnote HighlightActualLine finished");
        }

        private void TryAutoCompleteText()
        {
            Logger.Instance.Log(LogType.Method, "Blopnote TryAutoCompleteText invoked");

            int lineIndex = textField.realTextBoxLinesLength - 1;
            int lyricsBoxLineIndex;
            TypesOfLine lineType = lyricsBox.IsRepeatedLineOrKeyword(lineIndex);
            while (lineType != TypesOfLine.New)
            {
                switch (lineType)
                {
                    case TypesOfLine.Repeated:
                        lyricsBoxLineIndex = lyricsBox.IndexOfFirstOccurenceOfSameLine(lineIndex);
                        TextBoxWithText.AppendText(TextBoxWithText.Lines[lyricsBoxLineIndex]);
                        break;
                    case TypesOfLine.Keyword:
                        TextBoxWithText.AppendText(lyricsBox[lineIndex]);
                        break;
                }
                TextBoxWithText.AppendText("\n");
                TextBoxWithText.SelectionStart = TextBoxWithText.Text.Length;

                lineIndex++;
                lineType = lyricsBox.IsRepeatedLineOrKeyword(lineIndex);
            }

            Logger.Instance.Log(LogType.Method, "Blopnote TryAutoCompleteText finished");
        }

        private void tabTranslatesOnly1LineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote TryAutoCompleteText invoked");

            tabTranslatesOnly1LineToolStripMenuItem.Checked = !tabTranslatesOnly1LineToolStripMenuItem.Checked;

            Logger.Instance.Log(LogType.EventHandler, "Blopnote TryAutoCompleteText finished");
        }

        private void followUrl_Click(object sender, EventArgs e)
        {
            Logger.Instance.Log(LogType.EventHandler, "Blopnote followUrl_Click invoked");

            Browser.OpenUrlForUser(fileState.Url);

            Logger.Instance.Log(LogType.EventHandler, "Blopnote followUrl_Click invoked");
        }

        private void changeUrl_Click(object sender, EventArgs e)
        {
            InsertNew("URL");
        }

        private void changeLyricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertNew("Lyrics");
        }

        private void InsertNew(string item)
        {
            Logger.Instance.Log(LogType.Method, "Blopnote insert new lyrics or url invoked");

            string newItem = Interaction.InputBox(Prompt: $"Insert {item}", Title: $"Changing {item}");

            if (string.IsNullOrEmpty(newItem))
            {
                Logger.Instance.Log(LogType.Method, "Blopnote insert new lyrics or url finished");
                return;
            }

            if (item == "URL")
            {
                item = "Url";
            }

            var property = fileState.GetType().GetProperty(item);
            property.SetValue(fileState, newItem);
            fileProcessor.TryRewriteSongInfo($"{item} wasn't saved.");

            Logger.Instance.Log(LogType.Method, "Blopnote insert new lyrics or url finished");
        }

        private void TextBoxWithText_SelectionChanged(object sender, EventArgs e)
        {
            Logger.Instance.Log(LogType.Method, "Blopnote TextBoxWithText_SelectionChanged invoked");

            if (PreviousLineWithCarriage != textField.LineWithCarriage)
            {
                PreviousLineWithCarriage = textField.LineWithCarriage;
                HighlightActualLine();
            }

            Logger.Instance.Log(LogType.Method, "Blopnote TextBoxWithText_SelectionChanged finished");
        }

        private void ImportDocToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reconnectBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
#warning dry browser
            try
            {
                Browser.Instance.DoNothing();
            }
            catch (Exception exception)
            {
                MessageShower.Show(exception);
            }
        }
    }
}