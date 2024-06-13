using Blopnote.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;
using static Blopnote.Browser;
using System.Diagnostics;
using System.IO;

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
        private int PreviousSelectionStart { get; set; }
        public Blopnote()
        {
            InitializeComponent();
            this.Icon = Resources.icon;
            openFileDialog1.Filter = ".txt files(*.txt)|*.txt";
            timer1.Interval = AUTOSAVE_FREQUENCY_IN_SECONDS * 1000;

            textField = new TextField(TextBoxWithText);
            fileState = new FileState(status, textField);
            lyricsBox = new LyricsBox(PanelForLyricsBox, TextBoxWithText.Font, VScrollBarForLyrics, toolTipLyrics);
            fileProcessor = new FileProcessor(textField, fileState, lyricsBox, openFileDialog1);
            textField.SongIsWritten += fileProcessor.SongIsWritten_Handler;
            fileProcessor.DirectoryChanged += (sender, e) =>
            {
                createNewTranslationForm.UpdateMaxLength(((FileProcessor)sender).DirectoryLength);
            };
            createNewTranslationForm = new CreateNewTranslationForm();
            sizeRegulator = new SizeRegulator(lyricsBox, textField);

            textField.PlaceOnce(topMargin: menuStrip1.Height);
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
            string folderPath = ConfigurationManager.AppSettings.Get(CONFIG_FOLDER_ATTRIBUTE);
            if (string.IsNullOrEmpty(folderPath))
            {
                folderBrowserDialog1.Description = "Choose the folder where translations will be stored. Program will create the folder 'lyrics' inside.";
#warning DRY
                folderPath = AskUserForPath();
                if (string.IsNullOrEmpty(folderPath))
                {
                    this.Close();
                }
                else
                {
                    UpdateConfig(CONFIG_FOLDER_ATTRIBUTE, folderPath);
                }
            }
            fileProcessor.ChangeDirectory(folderPath);
            Enabled = true;
        }

        private void Blopnote_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeToolStripMenuItem.PerformClick();
            //try
            //{
            //    driver.Close();
            //    driver.Dispose();
            //}
            //catch
            //{ }
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

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (createNewTranslationForm.ShowForDataInput() == DialogResult.OK)
            {
#warning awful + dirty code
                lyricsBox.EnsureCleared();
                HandleInsertedData();
                PrepareComponentsToDisplayNewTranslation(clearText: true);
            }
        }

        private void HandleInsertedData()
        {
            string fileName = createNewTranslationForm.FileName;
            string lyrics = createNewTranslationForm.Lyrics;

            try
            {
                fileProcessor.CreateNewTranslation(fileName, lyrics);
                textField.ObserveCompletion();
            }
            catch (Exception e)
            {
                MessageBox.Show(caption: "File error",
                    text: "File wasn't created.\nCause: " + e.Message);
            }
        }

        private void PrepareComponentsToDisplayNewTranslation(bool clearText)
        {
            textField.Enable();
            closeToolStripMenuItem.Enabled = true;
            changeFolderToolStripMenuItem.Enabled = false;
            if (clearText)
            {
                textField.Clear();
            }
            ShowLyrics.Enabled = fileState.LyricsIsUsed;

            if (fileState.LyricsIsUsed)
            {
                textField.NumberOfLinesToComplete = lyricsBox.LinesQuantity;
                TryAutoCompleteText();

                // Auto enabling of lyrics when user entered it
                if (!ShowLyrics.Checked)
                {
                    ShowLyrics.PerformClick();
                }
            }

            RegulateTextAndLyricsBoxes();

            timerSelectionStart.Start();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult answer = openFileDialog1.ShowDialog();
#warning maybe handle opened and empty file in different ways?
            if (answer == DialogResult.OK)
            {
                StopTimerAndTrySaveFile(false);
                lyricsBox.EnsureCleared();

                fileProcessor.OpenTranslation(openFileDialog1.FileName);
                PrepareComponentsToDisplayNewTranslation(clearText: false);

                textField.Focus();
            }
        }

        private void UpdateConfig(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(Path.Combine(Environment.CurrentDirectory, "Blopnote.exe"));
            config.AppSettings.Settings[key].Value = value;
            config.Save();
        }

        private string AskUserForPath()
        {
            folderBrowserDialog1.ShowDialog();
            return folderBrowserDialog1.SelectedPath;
        }

        private void changeFolderPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
#warning DRY
            string folderPath = AskUserForPath();
            if (!string.IsNullOrEmpty(folderPath))
            {
                fileProcessor.ChangeDirectory(folderPath);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopTimerAndTrySaveFile(true);
            closeToolStripMenuItem.Enabled = false;
            changeFolderToolStripMenuItem.Enabled = true;
            ShowLyrics.Enabled = false;
            fileState.DoesNotExist();
            RegulateTextAndLyricsBoxes();
            timerSelectionStart.Stop();
        }

        private void ShowLyricsToolStripMenuItem_Click(object sender, EventArgs e)
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
            if (ShowLyrics.Enabled == false)
            {
                ShowLyrics.Checked = false;
                lyricsBox.NoLyrics();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            StopTimerAndTrySaveFile(true);
        }

        private void StopTimerAndTrySaveFile(bool mandatorySave)
        {
#warning dirty
            timer1.Stop();
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

        private void TextBoxWithText_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control | Keys.Back:
                    e.SuppressKeyPress = true;
                    if (TextBoxWithText.SelectionStart > 0)
                    {
                        SendKeys.Send("+{LEFT}{DEL}");
                    }
                    break;
                case Keys.Control | Keys.C:
                    if (TextBoxWithText.SelectedText.Length == 0)
                    {
                        e.SuppressKeyPress = true;
                        textField.CopyCurrentLineToClipBoard();
                    }
                    break;
                case Keys.Control | Keys.W:
                    e.SuppressKeyPress = true;
                    Application.Exit();
                    break;
                default:
                    e.SuppressKeyPress = false;
                    break;
            }
        }

       // It's pointless to process ctrl+w in current version of app, but it's an another way to
       // process the hotkey everywhere in application.
       //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
       //{
       //    if (keyData == (Keys.Control | Keys.W))
       //    {
       //        Application.Exit();
       //        return true;
       //    }
       //    return base.ProcessCmdKey(ref msg, keyData);
       //}

        private void TextBoxWithText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ShowLyrics.Checked && (int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = true;
                TextBoxWithText.AppendText("\r\n");
                TextBoxWithText.SelectionStart = TextBoxWithText.Text.Length;
                TryAutoCompleteText();
            }
        }

        private void HighlightActualLine()
        {
            lyricsBox.HighlightAt(textField.LineIndex);
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

        private void timerSelectionStart_Tick(object sender, EventArgs e)
        {
            if (PreviousSelectionStart != TextBoxWithText.SelectionStart)
            {
                PreviousSelectionStart = TextBoxWithText.SelectionStart;
                HighlightActualLine();
            }
        }
    }
}