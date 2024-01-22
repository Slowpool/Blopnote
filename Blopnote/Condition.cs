using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Blopnote
{
    public class Condition
    {
        private readonly Title title;

        private string FileName { get; set; }
        public bool FileHasName => FileName != null;

        public Condition(Title title)
        {
            this.title = title;
            FileState = FileStates.JustCreated;
        }

        private FileStates fileState;
        public FileStates FileState
        {
            set
            {
                fileState = value;
                switch (fileState)
                {
                    case FileStates.JustCreated:
                        title.SetNewName("Unnamed");
                        break;
                    case FileStates.Saved:
                        title.NoteAsSaved();
                        title.SetNewName(FileName);
                        break;
                    case FileStates.Unsaved:
                        title.NoteAsNotSaved();
                        break;
                }
            }
            get => fileState;
        }

        public bool IsUnsaved()
        {
            return fileState == FileStates.Unsaved;
        }

        public bool IsSaved()
        {
            return fileState == FileStates.Saved;
        }

        public bool IsJustCreated()
        {
            return fileState == FileStates.JustCreated;
        }
    }
}