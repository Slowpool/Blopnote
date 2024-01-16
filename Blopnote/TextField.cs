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

        public void AdjustTextFieldSizeTo(Size size)
        {
            TextBoxWithText.Size = size;
        }
    }
}