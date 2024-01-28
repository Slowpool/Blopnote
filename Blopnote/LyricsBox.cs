using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blopnote
{
    internal class LyricsBox
    {
        internal Panel panel;

        private string lyrics { get; set; }

        internal LyricsBox()
        {
            panel = new Panel();
        }

        /// <summary>
        /// This method create panel with labels. One line of text = one lable.
        /// Text has a keywords, like [Pre-Chorus], [Chorus], [Бридж], [Переход],
        /// which are not inserted by user. Also they have a some background color, e.g. green for chorus.
        /// </summary>
        /// <param name="lyrics"></param>
        public void BuildNewLyrics(string lyrics)
        {
            this.lyrics = lyrics;

        }

        internal void NoLyrics()
        {
            panel.Visible = false;
            lyrics = null;
        }

        internal void Display()
        {
            panel.Visible = true;
        }


    }
}