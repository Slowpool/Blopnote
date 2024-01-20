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
        private Form form;

        public Title(Form form)
        {
            this.form = form;
            FileState = FileStates.JustCreated;
        }

        private string TitleString
        {
            get => form.Text;
            set => form.Text = value;
        }

        private string FileName { get; set; }

        private FileStates fileState;
        public FileStates FileState
        {
            set
            {
                fileState = value;
                switch (fileState)
                {
                    case FileStates.JustCreated:
                        SetNewName("Unnamed");
                        break;
                    case FileStates.Saved:
                        TitleString = TitleString.Substring(1);
                        break;
                    case FileStates.Unsaved:
                        TitleString = '*' + TitleString;
                        break;
                }
            }
            get => fileState;
        }

        public void SetNewName(string fileName)
        {
            TitleString = fileName + " - Blopnot";
        }
    }
}