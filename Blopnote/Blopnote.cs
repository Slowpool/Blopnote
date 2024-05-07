using Blopnote.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote
{
    public partial class Blopnote : Form
    {
        private readonly TextField textField;
        private readonly FileProcessor fileProcessor;
        private readonly FileCondition fileCondition;
        private readonly FileNameAndLyricsInputWindow dataInputWindow;
        private readonly LyricsBox lyricsBox;
        private readonly SizeRegulator sizeRegulator;

        private Size WorkSpace => new Size(ClientSize.Width, ClientSize.Height - menuStrip1.Height - statusStrip1.Height);

        private const string DEFAULT_PATH_FOR_FILES = "C:\\Users\\azgel\\Desktop\\translations";
        private const int FREQUENT_OF_AUTOSAVE_IN_SECONDS = 5;
        public Blopnote()
        {
            InitializeComponent();
            this.Icon = Resources.icon;
            folderBrowserDialog1.Description = "The default path is " + DEFAULT_PATH_FOR_FILES;
            openFileDialog1.Filter = ".txt files(*.txt)|*.txt";
            timer1.Interval = FREQUENT_OF_AUTOSAVE_IN_SECONDS * 1000;

            textField = new TextField(TextBoxWithText);
            fileCondition = new FileCondition(status, textField);
            lyricsBox = new LyricsBox(PanelForLyricsBox, TextBoxWithText.Font, VScrollBarForLyrics);
            fileProcessor = new FileProcessor(textField, fileCondition, lyricsBox, openFileDialog1);
            dataInputWindow = new FileNameAndLyricsInputWindow();
            sizeRegulator = new SizeRegulator(lyricsBox, textField);

            textField.PlaceOnce(topMargin: menuStrip1.Height);
        }

        private void Blopnote_SizeChanged(object sender, EventArgs e)
        {
            RegulateTextWithLyrics();
        }

        private void RegulateTextWithLyrics()
        {
            sizeRegulator.RegulateTo(WorkSpace);
            lyricsBox.Left = TextBoxWithText.Right;
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataInputWindow.ShowForDataInput();
            if (dataInputWindow.IsDataInserted)
            {
                #warning awful + dirty code
                lyricsBox.ClearPreviousLyricsDisplayIfNeed();
                HandleInsertedData();
                PrepareComponentsForDisplayingOfNewTranslation(clearText: true);
            }
        }

        private void HandleInsertedData()
        {
            string fileName = dataInputWindow.FileName;
            string lyrics = dataInputWindow.Lyrics;

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

        private void PrepareComponentsForDisplayingOfNewTranslation(bool clearText)
        {
            textField.Enable();
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

            RegulateTextWithLyrics();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult answer = openFileDialog1.ShowDialog();
            if (answer == DialogResult.OK)
            {
                lyricsBox.ClearPreviousLyricsDisplayIfNeed();

                fileProcessor.OpenTranslation(openFileDialog1.FileName);
                PrepareComponentsForDisplayingOfNewTranslation(clearText: false);
            }
        }

        private void TextBoxWithText_TextChanged(object sender, EventArgs e)
        {
#warning bug searching
            TextBoxWithText.Focus();
            TextBoxWithText.Focus();
            timer1.Start();
            if (ShowLyrics.Checked)
            {
                HighlightCurrentLine();
            }
        }

        private void Blopnote_Load(object sender, EventArgs e)
        {
            fileCondition.DoesNotExist();
            fileProcessor.ChangeDirectory(DEFAULT_PATH_FOR_FILES);
            lyricsBox.Hide();

            sizeRegulator.RegulateTo(WorkSpace);
        }

        private void changeFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                fileProcessor.ChangeDirectory(folderBrowserDialog1.SelectedPath);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrySaveFile();
            ShowLyrics.Enabled = false;
            fileCondition.DoesNotExist();
            timer1.Stop();
            RegulateTextWithLyrics();
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
            RegulateTextWithLyrics();
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
            TrySaveFile();
        }

        private void TrySaveFile()
        {
            
            if (!string.IsNullOrEmpty(fileCondition.FileName))
            {
                try
                {
                    timer1.Stop();
                    fileProcessor.Save();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(caption: "File writing error",
                                    text: "Error: " + exception.Message);
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

        private void Blopnote_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeToolStripMenuItem.PerformClick();
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

        private void PanelForLyricsBox_MouseEnter(object sender, EventArgs e)
        {
            PanelForLyricsBox.Focus();
        }

        private void TextBoxWithText_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Control && e.KeyCode == Keys.C && )
            //{
            //    textField.CopyCurrentLineToClipBoard();
            //}
        }

        private void PanelForLyricsBox_MouseLeave(object sender, EventArgs e)
        {
            TextBoxWithText.Focus();
        }
    }
}