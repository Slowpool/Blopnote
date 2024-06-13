using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blopnote
{
    internal class SongInfo
    {
        public string Lyrics { get; set; }
        public bool Completed { get; set; }
        public SongInfo(string lyrics)
        {
            Lyrics = lyrics;
            Completed = false;
        }
    }
}
