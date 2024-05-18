using Blopnote.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;

namespace Blopnote
{
    public partial class Blopnote : Form
    {
        private readonly TextField textField;
        private readonly FileProcessor fileProcessor;
        private readonly FileCondition fileCondition;
        private readonly CreateNewTranslationWindow createNewTranslation;
        private readonly LyricsBox lyricsBox;
        private readonly SizeRegulator sizeRegulator;

        private Size WorkSpace => new Size(ClientSize.Width, ClientSize.Height - menuStrip1.Height - statusStrip1.Height);

        private const int AUTOSAVE_FREQUENCY_IN_SECONDS = 5;
        private const string CONFIG_FOLDER_ATTRIBUTE = "folderWithTranslationsPath";
        public Blopnote()
        {
            InitializeComponent();
            this.Icon = Resources.icon;
            openFileDialog1.Filter = ".txt files(*.txt)|*.txt";
            timer1.Interval = AUTOSAVE_FREQUENCY_IN_SECONDS * 1000;

            textField = new TextField(TextBoxWithText);
            fileCondition = new FileCondition(status, textField);
            lyricsBox = new LyricsBox(PanelForLyricsBox, TextBoxWithText.Font, VScrollBarForLyrics);
            fileProcessor = new FileProcessor(textField, fileCondition, lyricsBox, openFileDialog1);
            fileProcessor.DirectoryChanged += (sender, e) =>
            {
                createNewTranslation.UpdateMaxLength(((FileProcessor)sender).DirectoryLength);
            };
            createNewTranslation = new CreateNewTranslationWindow();
            sizeRegulator = new SizeRegulator(lyricsBox, textField);

            textField.PlaceOnce(topMargin: menuStrip1.Height);
        }

        #region FormEvents
        private void Blopnote_Load(object sender, EventArgs e)
        {
            fileCondition.DoesNotExist();
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
            if (createNewTranslation.ShowForDataInput() == DialogResult.OK)
            {
                #warning awful + dirty code
                lyricsBox.ClearPreviousLyricsIfNeed();
                HandleInsertedData();
                PrepareComponentsToDisplayNewTranslation(clearText: true);
            }
        }

        private void HandleInsertedData()
        {
            string fileName = createNewTranslation.FileName;
            string lyrics = createNewTranslation.Lyrics;

            try
            {
                fileProcessor.CreateNewTranslation(fileName, lyrics);
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
            if (clearText)
            {
                textField.Clear();
            }
            ShowLyrics.Enabled = fileCondition.LyricsExists;

            if (fileCondition.LyricsExists)
            {
                TryAutoCompleteText();
                
                // Auto enabling of lyrics when user entered it
                if (!ShowLyrics.Checked)
                {
                    ShowLyrics.PerformClick();
                }
            }

            RegulateTextAndLyricsBoxes();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult answer = openFileDialog1.ShowDialog();
            if (answer == DialogResult.OK)
            {
#warning maybe handle opened and empty file with different ways?
                StopTimerAndTrySaveFile(false);
                lyricsBox.ClearPreviousLyricsIfNeed();

                fileProcessor.OpenTranslation(openFileDialog1.FileName);
                PrepareComponentsToDisplayNewTranslation(clearText: false);

                textField.Focus();
            }
        }

        private void TextBoxWithText_TextChanged(object sender, EventArgs e)
        {
#warning bug searching
            textField.Focus();
            if (ShowLyrics.Checked)
            {
                HighlightCurrentLine();
            }
        }

        private void UpdateConfig(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(Environment.CurrentDirectory + @"\Blopnote.exe");
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
            ShowLyrics.Enabled = false;
            fileCondition.DoesNotExist();
            RegulateTextAndLyricsBoxes();
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
            if (e.Control && e.KeyCode == Keys.Back)
            {
                e.SuppressKeyPress = true;
                if (TextBoxWithText.SelectionStart > 0)
                {
                    SendKeys.Send("+{LEFT}{DEL}");
                }
            }
        }

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

        private void TextBoxWithText_KeyUp(object sender, KeyEventArgs e)
        {
#warning impelement ctrl+c and ctrl+d 
            //if (e.KeyCode == Keys.Control && e.KeyCode == Keys.C && )
            //{
            //    textField.CopyCurrentLineToClipBoard();
            //}
        }

        private void HighlightCurrentLine()
        {
            int currentLineIndex = textField.realTextBoxLinesLength - 1;
            lyricsBox.HighlightAt(currentLineIndex);
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
    }
}