using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote
{
    public class Title
    {
        private readonly Form form;
        private readonly Condition condition;

        public Title(Form form)
        {
            this.form = form;
        }

        private string TitleString
        {
            get => form.Text;
            set => form.Text = value;
        }

        private string FileName { get; set; }

        public void SetNewName(string fileName)
        {
            TitleString = fileName + " - Blopnot";
        }

        public void NoteAsSaved()
        {
            if (TitleString[0] == '*')
            {
                TitleString = TitleString.Substring(1);
            }
        }

        public void NoteAsNotSaved()
        {
            TitleString = '*' + TitleString;
        }

        public void RefreshTitle(string newFileName)
        {
            SetNewName(newFileName);
        }
    }
}