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

        string FullFileName { get; }
        bool IsFolderAllowedToChange { get; }

        public string TranslationText{ get; set; }

        string Lyrics { get; set; }
        bool IsLyricsShown { get; set; }
        bool IsLyricsAllowedToChange { get; set; }

        string Url { get; set; }
        bool IsUrlAllowedToFollow { get; set; }
        bool IsUrlAllowedToChange { get; set; }

        bool IsTranslationCompleted { get; set; }

        private const int AUTOSAVE_FREQUENCY_IN_SECONDS = 5;

        public void StartSaveTimer(bool mandatorySave)
        {

        }

        public void TryAutoCompleteText()
        {
            int lineIndex = textField.realTextBoxLinesLength - 1;
            int sameLineIndex;
            TypesOfLine lineType = lyricsBox.IsRepeatedLineOrKeyword(lineIndex);
            while (lineType != TypesOfLine.New)
            {
                switch (lineType)
                {
                    case TypesOfLine.Repeated:
                        sameLineIndex = lyricsBox.IndexOfFirstOccurenceOfSameLine(lineIndex);
                        TranslationText += TextBoxWithText.Lines[sameLineIndex];
                        break;
                    case TypesOfLine.Keyword:
                        TranslationText += lyricsBox[lineIndex];
                        break;
                }
                TranslationText += "\r\n";
                TextBoxWithText.SelectionStart = TextBoxWithText.Text.Length;

                lineIndex++;
                lineType = lyricsBox.IsRepeatedLineOrKeyword(lineIndex);
            }
        }

        public void CreateTranslation(string fullFileName, SongInfo songInfo);
        public void OpenTranslation(string fullFileName);
        public void CloseTranslation();
        public void ChangeDirectory(DirectoryInfo newDirectory);
        public void StartSaveTimer();
    }
}
