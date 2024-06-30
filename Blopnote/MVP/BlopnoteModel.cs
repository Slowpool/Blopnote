using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote.MVP
{
    public class BlopnoteModel : IBlopnoteModel
    {
        private readonly TextField textField;
        private readonly LyricsBox lyricsBox;
        private readonly FileState fileState;
        private readonly FileProcessor fileProcessor;
        
        public BlopnoteModel(TextField textField, LyricsBox lyricsBox, FileState fileState, FileProcessor fileProcessor)
        {
            this.textField = textField;
            this.lyricsBox = lyricsBox;
            this.fileState = fileState;
            this.fileProcessor = fileProcessor;

        }

        void CreateTranslation(string fullFileName, SongInfo songInfo)
        {

        }
        void OpenTranslation(string fullFileName);
        void CloseTranslation();
        void ChangeLyrics(string newLyrics);
        void ChangeUrl(string newUrl);
        void ChangeDirectory(DirectoryInfo newDirectory);
        void KeyDown(object sender, KeyEventArgs e);
        void KeyPress(object sender, KeyPressEventArgs e);
    }
}
