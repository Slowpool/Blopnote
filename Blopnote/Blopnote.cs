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
using Microsoft.Extensions.Logging;

namespace Blopnote
{
    public partial class Blopnote : Form, IInteractorWithBrowser
    {
        private readonly ILogger<Blopnote> Logger = BlopnoteLogger.CreateLogger<Blopnote>();

        private readonly TextField textField;
        private readonly FileProcessor fileProcessor;
        private readonly FileState fileState;
        private readonly CreateNewTranslationForm createNewTranslationForm;
        private readonly LyricsBox lyricsBox;
        private readonly SizeRegulator sizeRegulator;
        private Size WorkSpace => new Size(ClientSize.Width, ClientSize.Height - menuStrip1.Height - statusStrip1.Height);

        private const int AUTOSAVE_FREQUENCY_IN_SECONDS = 5;
        private const string CONFIG_FOLDER_ATTRIBUTE = "folderWithTranslationsPath";
        private int PreviousCarriageLine { get; set; }
        public static Font font = new Font("Consolas", 14.25F, FontStyle.Regular);
        public Blopnote()
        {
            Logger.LogInformation("Class constructing");

            InitializeComponent();
            this.Icon = Resources.icon;
            timerAutoSave.Interval = AUTOSAVE_FREQUENCY_IN_SECONDS * 1000;

            lyricsBox = new LyricsBox(PanelForLyricsBox, font, VScrollBarForLyrics);
            textField = new TextField(TextBoxWithText, lyricsBox.PreviewKeyDown, lyricsBox.KeyUp);
            fileState = new FileState(status, textField);
            fileProcessor = new FileProcessor(textField, fileState, lyricsBox);

            changeFolderToolStripMenuItem.Click += fileProcessor.ChangeDirecrotyHandler;
            tabTranslatesOnly1LineToolStripMenuItem.Click += lyricsBox.SwitchTabMode;
            createToolStripMenuItem.Click += lyricsBox.ResetScrollBar;
            openToolStripMenuItem.Click += lyricsBox.ResetScrollBar;

            lyricsBox.RawOnlineTranslationWasLoaded += textField.ObserveTabHolding;
            useOnlineTranslationToolStripMenuItem.Click += lyricsBox.OnlineTranslation_Changed;

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
                if (!opened)
                {
                    textField.StopObserveTabHolding();
                }

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
                    TryAutoCompleteLines();
                }

                ShowLyrics.Enabled = fileState.IsLyricsUsed;
                // Auto enabling of lyrics when user entered it
                if (!ShowLyrics.Checked)
                {
                    ShowLyrics.PerformClick();
                }
            };

            ImportXmlToolStripMenuItem.Click += fileProcessor.ImportXmlHandler;

            createNewTranslationForm = new CreateNewTranslationForm(reconnectBrowserToolStripMenuItem_Click);
            sizeRegulator = new SizeRegulator(lyricsBox, textField);

            textField.PlaceOnce(topMargin: menuStrip1.Height);
        }

        #region FormEvents
        private void Blopnote_Load(object sender, EventArgs e)
        {
            Logger.LogInformation("Form loading");
            fileState.CloseFile();
            lyricsBox.Hide();

            sizeRegulator.RegulateTo(WorkSpace);
        }

        private void Blopnote_Shown(object sender, EventArgs e)
        {
            Logger.LogInformation("Form shown");

            Enabled = false;
            string directoryFullName = ConfigurationManager.AppSettings.Get(CONFIG_FOLDER_ATTRIBUTE);
            if (string.IsNullOrEmpty(directoryFullName))
            {
                Logger.LogInformation("Config was not found");
                if (!fileProcessor.AskPath(PathTypesToAsk.TranslationsDirectory, out directoryFullName))
                {
                    Logger.LogInformation("User picked cancel, so app is closing");
                    Application.Exit();
                    return;
                }
                else
                {
                    Logger.LogInformation("User picked directory: {dir}", directoryFullName);
                    UpdateConfig(CONFIG_FOLDER_ATTRIBUTE, directoryFullName);
                }
            }
            fileProcessor.SetInitialDirectory(directoryFullName);
            Enabled = true;
        }

        private void Blopnote_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logger.LogInformation("Form closing");

            closeToolStripMenuItem.PerformClick();

            // I don't like it. Wanna better approach, e.g. ~Browser (destructor)
            //try
            //{
            //    Browser.Instance.Close();
            //}
            //catch (NullReferenceException exception)
            //{
            //    Logger.LogInformation(exception, "Browser wasn't created, so it wasn't disposed");
            //}
        }

        private void Blopnote_SizeChanged(object sender, EventArgs e)
        {
            RegulateTextAndLyricsBoxes();
        }
        #endregion

        private void RegulateTextAndLyricsBoxes()
        {
            Logger.LogInformation("Regulating of textbox and lyricsbox");

            sizeRegulator.RegulateTo(WorkSpace);
            lyricsBox.Left = TextBoxWithText.Right;
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.LogInformation("User clicked \"create file\"");

            if (createNewTranslationForm.ShowForDataInput() == DialogResult.OK)
            {
                Logger.LogInformation("User created new translation");
                using (new WaitingCursor())
                {
                    HandleInsertedData();
                    PrepareComponentsToDisplayTranslation();
                }
            }
            else
            {
                Logger.LogInformation("User closed window of creating of new translation");
            }
        }

        private void HandleInsertedData()
        {
            Logger.LogInformation("Handling of inserted data");

            try
            {
                fileProcessor.CreateNewTranslation(createNewTranslationForm.fileInfo, createNewTranslationForm.songInfo);
                Logger.LogInformation("User created translation.\r\nSong info: {@SongInfo}\r\nFile info: {@FileInfo}", fileState.songInfo, fileState.fileInfo);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "File creating error");
                MessageShower.Show(BlopnoteMessageTypes.FileCreatingError, e);
            }
        }

        private void PrepareComponentsToDisplayTranslation()
        {
            Logger.LogInformation("Preparing components for translation displaying");

            textField.Enable();
            
            RegulateTextAndLyricsBoxes();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.LogInformation("File opening");

            if (fileProcessor.AskPath(PathTypesToAsk.Song, out string fullFileName))
            {
                Logger.LogInformation("User opened file: {fileName}", fullFileName);

                using (new WaitingCursor())
                {
                    textField.StopObserving();
                    StopTimerAndTrySaveFile(mandatorySave: false);

                    fileProcessor.OpenTranslation(fullFileName);
                    PrepareComponentsToDisplayTranslation();

                    textField.Focus();
                }
            }
            else
            {
                Logger.LogInformation("User closed \"File opening\" dialog");
            }
        }

        private void UpdateConfig(string key, string value)
        {
            Logger.LogInformation("Config updating");

            var config = ConfigurationManager.OpenExeConfiguration(Path.Combine(Environment.CurrentDirectory, "Blopnote.exe"));
            config.AppSettings.Settings[key].Value = value;
            config.Save();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.LogInformation("File closing");

            StopTimerAndTrySaveFile(true);
            fileState.CloseFile();
            RegulateTextAndLyricsBoxes();
        }

        private void ShowLyrics_Click(object sender, EventArgs e)
        {
            Logger.LogInformation("Click on \"Show lyrics\"");

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
            Logger.LogInformation("Show lyrics changed: {value}", ShowLyrics.Enabled);

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
            Logger.LogInformation("Stopping of timer and trying to save file");

            timerAutoSave.Stop();
            try
            {
                fileProcessor.Save();
            }
            catch (Exception exception)
            {
                if (mandatorySave)
                {
                    Logger.LogError(exception, "File wasn't saved");
                    MessageShower.Show(BlopnoteMessageTypes.FileSavingError, exception);
                }
            }
        }

        private void TextBoxWithText_KeyDown(object sender, KeyEventArgs e)
        {
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
                TryAutoCompleteLines();
            }
        }

        // It's pointless to process ctrl+w in current version of app, but it's an another way to
        // process the hotkey everywhere in application.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.W))
            {
                Logger.LogInformation("User closed app via Ctrl+w");
                Application.Exit();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void TextBoxWithText_KeyPress(object sender, KeyPressEventArgs e)
        {
            Logger.LogInformation("Timer for autosave started");
            timerAutoSave.Start();
        }

        private void HighlightActualLine()
        {
            lyricsBox.HighlightAt(textField.CarriageLine);
        }

        private void TryAutoCompleteLines()
        {
            Logger.LogInformation("Trying to auto-complete lines");

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
        }

        private void tabTranslatesOnly1LineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabTranslatesOnly1LineToolStripMenuItem.Checked = !tabTranslatesOnly1LineToolStripMenuItem.Checked;

            Logger.LogInformation("Tab setting changed: {value}", tabTranslatesOnly1LineToolStripMenuItem.Checked);
        }

        private void followUrl_Click(object sender, EventArgs e)
        {
            Browser.OpenUrlForUser(fileState.Url);
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
            Logger.LogInformation("Inserting of new {item}", item);

            string newItem = Interaction.InputBox(Prompt: $"Insert {item}", Title: $"Changing {item}");

            if (string.IsNullOrEmpty(newItem))
            {
                Logger.LogInformation("Inserting of new {item} was aborted: user closed window", item);
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

        private void TextBoxWithText_SelectionChanged(object sender, EventArgs e)
        {
            Logger.LogInformation("Carriage changed");
            if (PreviousCarriageLine != textField.CarriageLine)
            {
                PreviousCarriageLine = textField.CarriageLine;
                HighlightActualLine();
            }
        }

        private void ImportDocToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void TryInteractWithBrowserOtherwiseShowError(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Browser error");
                MessageShower.Show(BlopnoteMessageTypes.BrowserError, exception);
                return;
            }
        }

        private void reconnectBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Browser.Instance.Reconnect();
        }

        private void useOnlineTranslationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            useOnlineTranslationToolStripMenuItem.Checked = !useOnlineTranslationToolStripMenuItem.Checked;
        }
    }
}