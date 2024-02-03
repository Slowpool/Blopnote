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
                PrepareComponentsForDisplayingOfNewTranslation();
            }
        }

        private void HandleInsertedData()
        {
            string fileName = dataInputWindow.FileName;
            string lyrics = dataInputWindow.Lyrics;

            fileProcessor.CreateNewTranslation(fileName, lyrics);

#warning hidden for debug
            //try
            //{
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(caption: "File error",
            //        text: "File wasn't created.\nCause: " + e.Message);
            //}
        }

        private void PrepareComponentsForDisplayingOfNewTranslation()
        {
            textField.Enable();
            textField.Clear();
            ShowLyrics.Enabled = fileCondition.LyricsExists;

            // Auto enabling of lyrics when user entered it
            if (fileCondition.LyricsExists && ShowLyrics.Checked == false)
            {
                ShowLyrics.PerformClick();
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
                PrepareComponentsForDisplayingOfNewTranslation();
            }
        }

        private void TextBoxWithText_TextChanged(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Blopnote_Load(object sender, EventArgs e)
        {
            fileCondition.DoesNotExist();
            fileProcessor.ChangeDirectory(DEFAULT_PATH_FOR_FILES);
            lyricsBox.Hide();

            RegulateTextWithLyrics();
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
            timer1.Stop();
            try
            {
                fileProcessor.WriteText();
            }
            catch (Exception exception)
            {
                MessageBox.Show(caption: "File writing error",
                                text: "Error: " + exception.Message);
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
    }
}