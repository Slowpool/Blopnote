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

        public int CarriageLine => TextBoxWithText.GetLineFromCharIndex(TextBoxWithText.SelectionStart);
        public int realTextBoxLinesLength => TextBoxWithText.Lines.Length == 0 ? 1 : TextBoxWithText.Lines.Length;

        public int LinesToComplete { get; set; }

#warning i don't like it but i don't know other ways. This part really bad.
        private readonly PreviewKeyDownEventHandler TabHoldingHandler;
        private readonly KeyEventHandler KeyUpHandler;
        public TextField(RichTextBox textBoxWithText, PreviewKeyDownEventHandler tabHoldingHandler, KeyEventHandler keyUpHandler)
        {
            this.TextBoxWithText = textBoxWithText;
            this.TabHoldingHandler = tabHoldingHandler;
            this.KeyUpHandler = keyUpHandler;
        }

        public void PlaceOnce(int topMargin)
        {
            // Here -1 due to strange displaying of textbox borders even when ClientSize property is used
            TextBoxWithText.Location = new Point(-1, topMargin);
        }

        public void AdjustSizeTo(Size size)
        {
            // Here +2 due to strange displaying of textbox borders even when ClientSize property is used
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
            string line = TextBoxWithText.Lines[CarriageLine];
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

        public void ObserveTabHolding()
        {
            TextBoxWithText.PreviewKeyDown += TabHoldingHandler;
        }

        public void StopObserveTabHolding()
        {
            TextBoxWithText.PreviewKeyDown -= TabHoldingHandler;
            TextBoxWithText.KeyUp -= KeyUpHandler;
        }
    }
}