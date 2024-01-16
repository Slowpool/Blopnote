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
        private TextField textField;
        public Blopnote()
        {
            InitializeComponent();
            textField = new TextField(TextBoxWithText);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Size currentSizeOfWindow = ClientSize;
            textField.AdjustTextFieldSizeTo(currentSizeOfWindow);
        }

        
    }
}