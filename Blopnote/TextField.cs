using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Blopnote
{
    internal class TextField
    {
        private readonly TextBox TextBoxWithText;

        internal TextField(TextBox TextBoxWithText)
        {
            this.TextBoxWithText = TextBoxWithText;
        }

        internal void PlaceToCorrectPosition(int topSpace)
        {
            // Here -1 due to strange display of textbox borders even with the property ClientSize being used
            TextBoxWithText.Location = new Point(-1, topSpace);
        }

        internal void AdjustTextFieldSizeTo(Size size)
        {
            // Here +2 due to strange display of textbox borders even with the property ClientSize being used
            TextBoxWithText.Size = new Size(size.Width + 2, size.Height);
            TextBoxWithText.AutoCompleteMode = AutoCompleteMode.Suggest;
            TextBoxWithText.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        internal string GetText()
        {
            return TextBoxWithText.Text;
        }

        internal void Disable()
        {
            TextBoxWithText.Enabled = false;
        }

        internal void Enable()
        {
            TextBoxWithText.Enabled = true;
        }

        internal void Clear()
        {
            TextBoxWithText.Clear();
        }
    }
}