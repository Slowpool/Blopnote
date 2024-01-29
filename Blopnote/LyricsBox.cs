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
        internal bool Enabled => panel.Visible;
        
        private string[] lines { get; set; }

        private Label[] labelsWithLyrics { get; set; }

        // I think it's bad idea to use so many levels of incapsulation for properties...
        internal int Width
        {
            get => panel.Width;
            set
            {
                if (value > 0)
                {
                    panel.Width = value;
                }
                else
                {
                    throw new Exception("incorrect width");
                }
            }
        }
        private int Height
        {
            set
            {
                if (value > 0)
                {
                    panel.Height = value;
                }
                else
                {
                    throw new Exception("incorrect width");
                }
            }
        }

        internal int Left
        {
            set
            {
                panel.Left = value;
            }
        }

        const int WIDTH_PADDING = 10;
        const int HEIGHT_PADDING = 10;

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
        internal void BuildNewLyrics(string lyrics)
        {
#warning unfinished
            // Q Remove empty lines or not?
            lines = lyrics.Split(new[] { "\r\n" }, StringSplitOptions.None);
            labelsWithLyrics = new Label[lines.Length];
            ConfigureLabels();
            CalculateWidth();
        }

        private void ConfigureLabels()
        {
            int y = 0;
            // Q WHY DO I HAVE TO SUBTRACT 1 FROM FONT HEIGHT HERE FOR CORRECT DISPLAYING OF ROWS?
            int lineHeight = font.Height - 1;
            for (int i = 0; i < labelsWithLyrics.Length; i++)
            {
                labelsWithLyrics[i] = new Label();
                labelsWithLyrics[i].Font = font;
                labelsWithLyrics[i].Text = lines[i];

                labelsWithLyrics[i].Top = y;
                labelsWithLyrics[i].Left = WIDTH_PADDING;
                labelsWithLyrics[i].AutoSize = true;
                ChangeBackgroundColorIfControlWord(labelsWithLyrics[i]);
                panel.Controls.Add(labelsWithLyrics[i]);

                y += lineHeight;
            }
        }
        /// <summary>
        /// This method analyzes label and looking for keyword like [chorus] and then changes color if label contains keyword.
        /// </summary>
        /// <param name="label"></param>
        private void ChangeBackgroundColorIfControlWord(Label label)
        {
            if (label.Text == "[Chorus]")
            {
                label.BackColor = Color.Yellow;
            }
        }

        private void CalculateWidth()
        {
            int maxWidth = labelsWithLyrics.Max(label => label.Width);
            Width = WIDTH_PADDING + maxWidth + WIDTH_PADDING;
        }

        internal void NoLyrics()
        {
            Hide();
            ClearPreviousLyricsDisplayIfNeed();

            // Q I'm not sure
            lines = null;
            //labelsWithLyrics = null;
        }

        internal void Display()
        {
            panel.Visible = true;
        }

        internal void Hide()
        {
            panel.Visible = false;
        }

        internal void AdjustHeightTo(int height)
        {
            Height = height;
        }

        internal void ClearPreviousLyricsDisplayIfNeed()
        {
            if (panel.Controls.Count != 0)
            {
                panel.Controls.Clear();
                foreach (var label in labelsWithLyrics)
                {
                    label.Dispose();
                }
            }
        }
    }
}