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

using static Blopnote.Browser;
using Blopnote.MVP;

namespace Blopnote
{
    public partial class Blopnote : Form, IBlopnoteView
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
        private int PreviousSelectionStart { get; set; }
        public Blopnote()
        {
            #region Old
            InitializeComponent();
            this.Icon = Resources.icon;
            openFileDialog1.Filter = ".txt files(*.txt)|*.txt";
            timerAutoSave.Interval = AUTOSAVE_FREQUENCY_IN_SECONDS * 1000;

            textField = new TextField(TextBoxWithText);
            fileState = new FileState(status, textField);
            lyricsBox = new LyricsBox(PanelForLyricsBox, TextBoxWithText.Font, VScrollBarForLyrics);
            fileProcessor = new FileProcessor(textField, fileState, lyricsBox, openFileDialog1);

            tabTranslatesOnly1LineToolStripMenuItem.Click += lyricsBox.SwitchTabMode;
            createToolStripMenuItem.Click += lyricsBox.ResetScrollBar;
            openToolStripMenuItem.Click += lyricsBox.ResetScrollBar;

            lyricsBox.TranslationByGoogleLoaded += textField.TranslationByGoogleLoaded;

            textField.SongIsWritten += fileProcessor.SongIsWritten_Handler;
            fileProcessor.DirectoryChanged += (sender, e) =>
            {
                createNewTranslationForm.UpdateMaxLength(fileProcessor.DirectoryLength);
            };

            fileState.UrlChanged += (sender, e) =>
            {
                followToolStripMenuItem.Enabled = fileState.IsUrlUsed;
            };

            fileState.FileOpenedOrClosed += (sender, e) =>
            {
                textField.Clear();

                closeToolStripMenuItem.Enabled = e.Opened;
                changeFolderToolStripMenuItem.Enabled = !e.Opened;
                changeUrlToolStripMenuItem.Enabled = e.Opened;
                changeLyricsToolStripMenuItem.Enabled = e.Opened;
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

                RegulateTextAndLyricsBoxes();
            };

            createNewTranslationForm = new CreateNewTranslationForm();
            sizeRegulator = new SizeRegulator(lyricsBox, textField);

            textField.PlaceOnce(topMargin: menuStrip1.Height);
            Browser.Latch();
            #endregion

            createToolStripMenuItem.Click += (sender, e) => CreateNewTranslation(sender, e);

        }

        #region FormEvents
        private void Blopnote_Load(object sender, EventArgs e)
        {
            fileState.DoesNotExist();
            lyricsBox.Hide();

            sizeRegulator.RegulateTo(WorkSpace);
        }

        private void Blopnote_Shown(object sender, EventArgs e)
        {
            Enabled = false;
            string directoryFullName = ConfigurationManager.AppSettings.Get(CONFIG_FOLDER_ATTRIBUTE);
            if (string.IsNullOrEmpty(directoryFullName))
            {
                folderBrowserDialog1.Description = "Choose the folder where translations will be stored. Program will create the folder 'songsInfo' inside.";
                directoryFullName = AskUserForPath();
                if (string.IsNullOrEmpty(directoryFullName))
                {
                    this.Close();
                }
                else
                {
                    UpdateConfig(CONFIG_FOLDER_ATTRIBUTE, directoryFullName);
                }
            }
            fileProcessor.SetInitialDirectory(directoryFullName);
            Enabled = true;
        }

        private void UpdateConfig(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(Path.Combine(Environment.CurrentDirectory, "Blopnote.exe"));
            config.AppSettings.Settings[key].Value = value;
            config.Save();
        }

        private void Blopnote_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeToolStripMenuItem.PerformClick();
            if (Browser.IsOpened)
            {
                driver.Close();
                driver.Dispose();
            }
        }

        private void Blopnote_SizeChanged(object sender, EventArgs e)
        {
            RegulateTextAndLyricsBoxes();
        }
        #endregion

        private void RegulateTextAndLyricsBoxes()
        {
            sizeRegulator.RegulateTo(WorkSpace);
            lyricsBox.Left = TextBoxWithText.Right;
        }

        event EventHandler CreateNewTranslation;
        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (createNewTranslationForm.ShowForDataInput() == DialogResult.OK)
            {
                HandleInsertedData();
                PrepareComponentsToDisplayTranslation();
            }
        }

        private void HandleInsertedData()
        {
            try
            {
                fileProcessor.CreateNewTranslation(createNewTranslationForm.fileInfo, createNewTranslationForm.songInfo);
            }
            catch (Exception e)
            {
                MessageBox.Show(caption: "File error",
                    text: "File wasn't created.\nCause: " + e.Message);
            }
        }

        private void PrepareComponentsToDisplayTranslation()
        {
            textField.Enable();
            
// Q: maybe there's no need to use this?
// A: there is.
// Q: maybe it's still worth it to remove it and leave just textField.Clear()
            //if (clearText)
            //{
            //    textField.Clear();
            //}

            RegulateTextAndLyricsBoxes();

            timerLineObserver.Start();
        }

        event EventHandler OpenTranslation;
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
#warning maybe i should to handle an opened and empty file in different ways?
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textField.StopObserving();
                StopTimerAndTrySaveFile(mandatorySave: false);

                fileProcessor.OpenTranslation(openFileDialog1.FileName);
                PrepareComponentsToDisplayTranslation();

                textField.Focus();
            }
        }

        private string AskUserForPath()
        {
            folderBrowserDialog1.ShowDialog();
            return folderBrowserDialog1.SelectedPath;
        }

        event EventHandler ChangeFolder;
        private void changeFolderPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folderPath = AskUserForPath();
            if (!string.IsNullOrEmpty(folderPath))
            {
                fileProcessor.SetInitialDirectory(folderPath);
            }
        }

        event EventHandler CloseTranslation;
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopTimerAndTrySaveFile(mandatorySave: true);
            fileState.DoesNotExist();
            timerLineObserver.Stop();
        }

        private void ShowLyrics_Click(object sender, EventArgs e)
        {
            if (ShowLyrics.Checked)
            {
                lyricsBox.Display();
            }
            else
            {
                lyricsBox.Hide();
            }
            RegulateTextAndLyricsBoxes();
        }

        private void ShowLyrics_EnabledChanged(object sender, EventArgs e)
        {
            if (!ShowLyrics.Enabled)
            {
                ShowLyrics.Checked = false;
                lyricsBox.NoLyrics();
            }
        }

        private void timerAutoSave_Tick(object sender, EventArgs e)
        {
            StopTimerAndTrySaveFile(true);
        }

        private void StopTimerAndTrySaveFile(bool mandatorySave)
        {
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
        }
        // It's pointless to process ctrl+w in current version of app, but it's an another way to
        // process the hotkey everywhere in application.
        // UPD: it's not pointless. When user doesn't open a file, main textbox is disabled and doesn't handle any presses. In such a case it'd better to handle ctrl+w hotkey in that way.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.W))
            {
                Application.Exit();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        public void EraseUntilDelimiter()
        {
            if (TextBoxWithText.SelectionStart > 0)
            {
                SendKeys.Send("+{LEFT}{DEL}");
            }
        }

        public bool TrySaveLineToClipBoard()
        {
            bool noSelectedText = TextBoxWithText.SelectedText.Length == 0;
            if (noSelectedText)
            {
                textField.CopyCurrentLineToClipBoard();
            }
            return noSelectedText;
        }

        private void TextBoxWithText_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void HighlightActualLine()
        {
            lyricsBox.HighlightAt(textField.LineIndexWithCarriage);
        }

        private void TryAutoCompleteText()
        {
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
                TextBoxWithText.AppendText("\r\n");
                TextBoxWithText.SelectionStart = TextBoxWithText.Text.Length;

                lineIndex++;
                lineType = lyricsBox.IsRepeatedLineOrKeyword(lineIndex);
            }
        }

        private void timerLineObserver_Tick(object sender, EventArgs e)
        {
            if (PreviousSelectionStart != TextBoxWithText.SelectionStart)
            {
                PreviousSelectionStart = TextBoxWithText.SelectionStart;
                HighlightActualLine();
            }
        }

        private void tabTranslatesOnly1LineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabTranslatesOnly1LineToolStripMenuItem.Checked = !tabTranslatesOnly1LineToolStripMenuItem.Checked;
        }

        private void followUrl_Click(object sender, EventArgs e)
        {
            OpenUrl(fileState.Url);
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
            string newItem = Interaction.InputBox(Prompt: $"Insert {item}", Title: $"Changing {item}");

            if (string.IsNullOrEmpty(newItem))
            {
                return;
            }

            if (item == "URL")
            {
                item = "Url";
            }

            var property = fileState.GetType().GetProperty(item);
            property.SetValue(fileState, newItem);
            fileProcessor.TryRewriteSongInfo($"{item} wasn't saved.");
        }
    }
}