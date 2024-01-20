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
    public partial class Blopnote : Form // argh
    {
        private readonly TextField textField;
        private readonly FileProcessor fileProcessor;
        private readonly Title title;

        public Blopnote()
        {
            InitializeComponent();
            this.Icon = Resources.icon;

            title = new Title(this);
            textField = new TextField(TextBoxWithText);
            fileProcessor = new FileProcessor(textField, title);

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
            fileProcessor.SaveFile();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileProcessor.SaveFileAs();
        }

        private void TextBoxWithText_TextChanged(object sender, EventArgs e)
        {
            title.FileState = FileStates.Unsaved;
        }

        private void Blopnote_FormClosing(object sender, FormClosingEventArgs e)
        {
            #warning stub
            string filePath = "here must be file path";
            var userAnswer = MessageBox.Show
            (
                caption: "Are you sure?",
                text: "Do you want to save file as " + filePath,
                buttons: MessageBoxButtons.YesNoCancel,
                icon: MessageBoxIcon.Warning
            );
        }
    }
}