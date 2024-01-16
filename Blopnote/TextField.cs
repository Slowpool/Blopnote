using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Blopnote
{
    public class TextField
    {
        private readonly TextBox TextBoxWithText;

        public TextField(TextBox TextBoxWithText)
        {
            this.TextBoxWithText = TextBoxWithText;
        }

        public void PlaceToCorrectPosition(int topSpace)
        {
            // Here -1 due to strange display of textbox borders even with the property ClientSize being used
            TextBoxWithText.Location = new Point(-1, topSpace);
        }

        public void AdjustTextFieldSizeTo(Size size)
        {
            // Here +2 due to strange display of textbox borders even with the property ClientSize being used
            TextBoxWithText.Size = new Size(size.Width + 2, size.Height);
        }
    }
}