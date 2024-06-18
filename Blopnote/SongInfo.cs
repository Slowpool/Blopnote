using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blopnote
{
    internal class SongInfo
    {
        //internal static readonly SongInfo Empty = new SongInfo();
        //private SongInfo()
        //{ }

        public string Lyrics { get; set; }
        public bool Completed { get; set; }
        public string URL { get; set; }

        public SongInfo(string lyrics, string URL)
        {
            Lyrics = lyrics;
            this.URL = URL;

            Completed = false;
        }
    }
}
