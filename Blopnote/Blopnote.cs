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
        private readonly Condition condition;

        public Blopnote()
        {
            InitializeComponent();
            this.Icon = Resources.icon;


            textField = new TextField(TextBoxWithText);
            condition = new Condition();
            fileProcessor = new FileProcessor(textField, condition);

            AdjustTextField();
        }

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

        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //fileProcessor.Save();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileProcessor.SaveFileAs();
        }

        private void TextBoxWithText_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Blopnote_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void SuggestSaveFileToUser()
        {
            // TODO fix it what if i exit from below method by closing dialog window
            fileProcessor.SaveFile();
        }
    }
}