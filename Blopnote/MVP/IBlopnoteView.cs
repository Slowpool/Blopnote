using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote.MVP
{
    public interface IBlopnoteView
    {
        event EventHandler CreateNewTranslation;
        event EventHandler OpenTranslation;
        event EventHandler CloseTranslation;
        event EventHandler ChangeFolder;
        event EventHandler ChangeLyrics;
        event EventHandler ChangeUrl;
        event KeyEventHandler KeyDown;
        event KeyPressEventHandler KeyPress;

        void Show();
        void Close();
        void EraseUntilDelimiter();
        bool TrySaveLineToClipboard();
        void PrintEnterAtTheEnd();

        string TranslationText { get; set; }
        bool TranslationTextFieldEnabled { get; set; }


        string Lyrics { get; set; }
        bool AllowToChangeLyricsVisibility { get; set; }
        bool ShowLyrics { get; set; }
        string TranslatedLyrics { get; set; }
        string Status { get; set; }



    }
}
