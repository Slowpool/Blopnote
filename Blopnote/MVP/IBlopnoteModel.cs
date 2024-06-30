using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote.MVP
{
    public interface IBlopnoteModel
    {
        void CreateTranslation(string fullFileName, SongInfo songInfo);
        void OpenTranslation(string fullFileName);
        void CloseTranslation();
        void ChangeDirectory(DirectoryInfo newDirectory);
        string FullFileName { get; }
        bool IsFolderAllowedToChange { get; }

        string TranslationText { get; set; }

        string Lyrics { get; set;  }
        bool IsLyricsShown { get; set; }
        bool IsLyricsAllowedToChange { get; set; }

        string Url { get; set; }
        bool IsUrlAllowedToFollow { get; set; }
        bool IsUrlAllowedToChange { get; set; }

        bool IsTranslationCompleted { get; set; }

    }
}
