using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Blopnote
{
    internal class SizeRegulator
    {
        private readonly LyricsBox lyricsBox;
        private readonly TextField textField;

        internal SizeRegulator(LyricsBox lyricsBox, TextField textField)
        {
            this.lyricsBox = lyricsBox;
            this.textField = textField;
        }

        internal void RegulateTo(Size size)
        {
            if (lyricsBox.Enabled)
            {
                // Here lyricsBox height is constant value anyway.
                Size SizeForTextField = new Size(size.Width - lyricsBox.Width, size.Height);
                
                lyricsBox.AdjustHeightTo(size.Height);
                textField.AdjustSizeTo(SizeForTextField);
            }
            else
            {
                textField.AdjustSizeTo(size);
            }
        }
    }
}
