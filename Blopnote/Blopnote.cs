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
    internal partial class Blopnote : Form
    {
        private readonly TextField textField;
        private readonly FileProcessor fileProcessor;
        private readonly FileCondition fileCondition;
        private readonly FileNameAndLyricsInputWindow dataInputWindow;
        private readonly LyricsBox lyricsBox;

        // TODO I should disable toolstripmenuitem Show' in case when lyrics doesn't exist. When it exist, user can enable or disable 'Show' item.


        private const string DEFAULT_PATH_FOR_FILES = @"C:/Users/azgel/Desktop/translations";

        internal Blopnote()
        {
            InitializeComponent();
            this.Icon = Resources.icon;
            folderBrowserDialog1.Description = "The default path is " + DEFAULT_PATH_FOR_FILES;

            textField = new TextField(TextBoxWithText);
            fileCondition = new FileCondition(status, textField);
            fileProcessor = new FileProcessor(textField, fileCondition);
            dataInputWindow = new FileNameAndLyricsInputWindow();

            AdjustFields();
        }
        
        #warning should redo

        private void AdjustFields()
        {
            textField.PlaceToCorrectPosition(menuStrip1.Bottom);
            textField.AdjustTextFieldSizeTo(ClientSize);
        }

        private void Blopnote_SizeChanged(object sender, EventArgs e)
        {
            Size currentSizeOfWindow = ClientSize;
            textField.AdjustTextFieldSizeTo(currentSizeOfWindow);
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataInputWindow.ShowForDataInput();
            if (dataInputWindow.IsDataInserted)
            {
                HandleInsertedData();
                RefreshComponentsForNewFile();
                DisplayLyricsIfNeed();
            }
        }

        private void DisplayLyricsIfNeed()
        {
            #warning raw version
            lyricsBox.Display();
        }

        private void RefreshComponentsForNewFile()
        {
            textField.Enable();
            textField.Clear();
            ShowLyrics.Enabled = fileCondition.LyricsExists;
            if (fileCondition.LyricsExists && ShowLyrics.Checked == false)
            {
                // Auto enabling of lyrics when user entered it
                ShowLyrics.PerformClick();
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
            ShowLyrics.Enabled = false;
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
            ShowLyrics.Checked = false;
        }

        private void ShowLyricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileCondition.SwitchLyricsUsed();
        }
    }
}