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

        private readonly int bottomMargin;
        private readonly int topMargin;

        private int rightMargin;
        private int RightMargin
        {
            get => rightMargin;
            set
            {
                rightMargin = value;

            }
        }

        internal TextField(TextBox TextBoxWithText, int bottomMargin, int topMargin)
        {
            this.TextBoxWithText = TextBoxWithText;
            this.bottomMargin = bottomMargin;
            this.topMargin = topMargin;

            rightMargin = 0;
        }

        internal void Place()
        {
            // Here -1 due to strange display of textbox borders even with using of property ClientSize
            TextBoxWithText.Location = new Point(-1, topMargin);
        }

        internal void AdjustSizeTo(Size size)
        {
            // Here +2 due to strange display of textbox borders even with the property ClientSize being used
            TextBoxWithText.Size = new Size(size.Width + 2 - rightMargin, size.Height - (topMargin + bottomMargin));
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