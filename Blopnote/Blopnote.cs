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
        private readonly FileNameAndLyricsInputWindow fileNameAndLyricsInputWindow;

        private const string DEFAULT_PATH_FOR_FILES = @"C:/Users/azgel/Desktop/translations";

        internal Blopnote()
        {
            InitializeComponent();
            this.Icon = Resources.icon;
            folderBrowserDialog1.Description = "The default path is " + DEFAULT_PATH_FOR_FILES;

            textField = new TextField(TextBoxWithText);
            fileCondition = new FileCondition(status, textField);
            fileProcessor = new FileProcessor(textField, fileCondition);
            fileNameAndLyricsInputWindow = new FileNameAndLyricsInputWindow();

            AdjustTextField();
        }
        
        #warning should redo
        private void AdjustTextField()
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
            fileNameAndLyricsInputWindow.ShowForDataInput();
            if (fileNameAndLyricsInputWindow.IsDataInserted)
            {
                string fileName = fileNameAndLyricsInputWindow.FileName;
                string lyrics = fileNameAndLyricsInputWindow.Lyrics;

                if (string.IsNullOrEmpty(fileName))
                {
                    return;
                }
                fileName += ".txt";
                fileProcessor.CreateNewFile(fileName);

                if (string.IsNullOrEmpty(lyrics))
                {
                    return;
                }

                
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
        }

        private void ShowLyricsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}