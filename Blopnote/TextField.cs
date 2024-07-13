using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Blopnote
{
    public class TextField
    {
        public event EventHandler SongIsWritten;

        private readonly RichTextBox TextBoxWithText;
        public string Text
        {
            get => TextBoxWithText.Text;
            set
            {
                TextBoxWithText.Text = value;
            }
        }

        public int LineWithCarriage => TextBoxWithText.GetLineFromCharIndex(TextBoxWithText.SelectionStart);
        public int realTextBoxLinesLength => TextBoxWithText.Lines.Length == 0 ? 1 : TextBoxWithText.Lines.Length;

        public int LinesToComplete { get; set; }

        public TextField(RichTextBox TextBoxWithText)
        {
            this.TextBoxWithText = TextBoxWithText;
        }

        public void PlaceOnce(int topMargin)
        {
            // Here -1 due to strange display of textbox borders even with using of property ClientSize
            TextBoxWithText.Location = new Point(-1, topMargin);
        }

        public void AdjustSizeTo(Size size)
        {
            // Here +2 due to strange display of textbox borders even with the property ClientSize being used
            TextBoxWithText.Size = new Size(size.Width + 2, size.Height);
        }

        public void Disable()
        {
            TextBoxWithText.Enabled = false;
        }

        public void Enable()
        {
            TextBoxWithText.Enabled = true;
        }

        public void Clear()
        {
            TextBoxWithText.Clear();
        }

        public void CopyCurrentLineToClipBoard()
        {
            string line = TextBoxWithText.Lines[LineWithCarriage];
            try
            {
                Clipboard.SetText(line);
            }
            catch
            {
                // Great. Somebody tried to copy 0 characters.
            }
        }

        public void Focus()
        {
            TextBoxWithText.Focus();
            TextBoxWithText.SelectionStart = TextBoxWithText.Text.Length;
        }

        public void SongCompletionChecker(object sender, EventArgs e)
        {
            Debug.WriteLine("checking for completion...");
            if (TextBoxWithText.Lines.Length > LinesToComplete)
            {
                SongIsWritten(this, null);
            }
        }

        public void ObserveCompletion()
        {
            Debug.WriteLine("Observing: True");
            TextBoxWithText.TextChanged += SongCompletionChecker;
        }

        public void StopObserving()
        {
            Debug.WriteLine("Observing: false");
            TextBoxWithText.TextChanged -= SongCompletionChecker;
        }

        public void TranslationByGoogleLoaded(object sender)
        {
            TextBoxWithText.PreviewKeyDown += ((LyricsBox)sender).PreviewKeyDown;
        }
    }
}