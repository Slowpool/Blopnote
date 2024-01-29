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
        internal string Text => TextBoxWithText.Text;

        internal TextField(TextBox TextBoxWithText)
        {
            this.TextBoxWithText = TextBoxWithText;
        }

        internal void PlaceOnce(int topMargin)
        {
            // Here -1 due to strange display of textbox borders even with using of property ClientSize
            TextBoxWithText.Location = new Point(-1, topMargin);
        }

        internal void AdjustSizeTo(Size size)
        {
            // Here +2 due to strange display of textbox borders even with the property ClientSize being used
            TextBoxWithText.Size = new Size(size.Width + 2, size.Height);
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