using Blopnote.Properties;
using Microsoft.VisualBasic;
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

        private const string DEFAULT_PATH_FOR_FILES = @"C:/Users/azgel/Desktop/translations";

        public Blopnote()
        {
            InitializeComponent();
            this.Icon = Resources.icon;
            folderBrowserDialog1.Description = "The default path is " + DEFAULT_PATH_FOR_FILES;

            textField = new TextField(TextBoxWithText);
            fileCondition = new FileCondition(status, textField);
            lyricsBox = new LyricsBox(PanelForLyricsBox, TextBoxWithText.Font);
            fileProcessor = new FileProcessor(textField, fileCondition, lyricsBox);
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
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxWithText_TextChanged(object sender, EventArgs e)
        {
            
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
            fileCondition.DoesNotExist();
            ShowLyrics.Enabled = false;
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
    }
}