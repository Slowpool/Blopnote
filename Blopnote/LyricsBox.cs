using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Blopnote
{
    internal class LyricsBox
    {
        internal readonly Panel panel;
        private readonly Font font;

        private string[] Lines { get; set; }

        private Label[] labelsWithLyrics { get; set; }

        internal LyricsBox(Panel panel, Font font)
        {
            this.panel = panel;
            this.font = font;
        }

        /// <summary>
        /// This method create panel with labels. One line of text = one lable.
        /// Text has a keywords, like [Pre-Chorus], [Chorus], [Бридж], [Переход],
        /// which are not inserted by user. Also they have a some background color, e.g. green for chorus.
        /// </summary>
        /// <param name="lyrics"></param>
        public void BuildNewLyrics(string lyrics)
        {
            // Q Remove empty lines or not?
            Lines = lyrics.Split(new[] { "\r\n" }, StringSplitOptions.None);
            labelsWithLyrics = new Label[Lines.Length];
            for(int i = 0; i < labelsWithLyrics.Length; i++)
            {
                labelsWithLyrics[i] = new Label();
                labelsWithLyrics[i].Font = font;
                labelsWithLyrics[i].Text = Lines[i];
            }
#warning unfinished
        }

        internal void NoLyrics()
        {
            panel.Visible = false;
            Lines = null;
        }

        internal void Display()
        {
            panel.Visible = true;
        }


    }
}