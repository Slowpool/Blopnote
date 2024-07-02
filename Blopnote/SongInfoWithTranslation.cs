using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blopnote
{
    public class SongInfoWithTranslation
    {
        public readonly string Lyrics;
        public readonly string TranslationText;
        public readonly bool Completed;
        public readonly string Url;

        public SongInfoWithTranslation()
        { }

        public SongInfoWithTranslation(SongInfo songInfo, string translationText)
        {
            Lyrics = songInfo.Lyrics;
            Completed = songInfo.Completed;
            Url = songInfo.Url;

            TranslationText = translationText;
        }
    }
}
