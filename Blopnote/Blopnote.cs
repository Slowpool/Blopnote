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
        private Title title { get; set; }
        private TextField textField { set; get; }

        public Blopnote()
        {
            InitializeComponent();
            textField = new TextField(TextBoxWithText);
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
            // somewhere at the end
            //FileSaved = true;
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // somewhere at the end
            //FileSaved = true;
        }

        private void TextBoxWithText_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}