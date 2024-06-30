using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blopnote
{
    public class SongInfo
    {
        public string Lyrics { get; set; }
        public bool Completed { get; set; }
        public string Url { get; set; }

        public SongInfo(string lyrics, string Url)
        {
            Lyrics = lyrics;
            this.Url = Url;

            Completed = false;
        }
    }
}
