using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote.MVP
{
    public class BlopnotePresenter
    {
        private IBlopnoteModel model;
        private IBlopnoteView view;
        public BlopnotePresenter(IBlopnoteModel model, IBlopnoteView view)
        {
            this.model = model;
            this.view = view;

            view.CreateNewTranslation += View_CreateNewTranslation;
            view.KeyDown += View_KeyDown;
            view.KeyPress += View_KeyPress;
        }

        private void View_KeyPress(object sender, KeyPressEventArgs e)
        {
#warning I stayed here
            timerAutoSave.Start();

            if (ShowLyrics.Checked && (int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = true;
                TextBoxWithText.AppendText("\r\n");
                TextBoxWithText.SelectionStart = TextBoxWithText.Text.Length;
                TryAutoCompleteText();
            }
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control | Keys.Back:
                    e.SuppressKeyPress = true;
                    view.EraseUntilDelimiter();
                    break;
                case Keys.Control | Keys.C:
                    if (view.TrySaveLineToClipboard())
                    {
                        e.SuppressKeyPress = true;
                    }
                    break;
                default:
                    break;
            }
#warning should i copy text to model?
        }



        private void View_CreateNewTranslation(object sender, EventArgs e)
        {
#warning HOW
            //if (createNewTranslationForm.ShowForDataInput() == DialogResult.OK)
            //{
            //    HandleInsertedData();
            //    PrepareComponentsToDisplayTranslation();
            //}
        }
    }
}
