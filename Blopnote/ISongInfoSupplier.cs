using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blopnote
{
    public interface ISongInfoSupplier
    {
        string[] GetTranslationByGoogle(string sourceLyrics);
        List<string> FindSimilarSongs(string songName);
        string[,] GetYoutubeReferences(string songName);
        string GetLyrics(string LyricsOfSongUrl);

    }
}
