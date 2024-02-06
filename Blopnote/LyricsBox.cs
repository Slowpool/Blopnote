using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Blopnote
{
    internal class LyricsBox
    {
        internal readonly Panel panel;
        private readonly Font font;
        private readonly VScrollBar scrollBar;
        internal bool Enabled => panel.Visible;
        
        private List<string> lines { get; set; }
        internal static string[] KeyWords = "intro интро verse pre-chorus chorus bridge autro предприпев припев переход бридж куплет аутро".Split();
        private Label[] labelsWithLyrics { get; set; }

        // I think it's bad idea to use so many levels of incapsulation for properties...
        internal int Width
        {
            get => panel.Width;
            set
            {
                panel.Width = value;
            }
        }
        private int Height
        {
            get => panel.Height;
            set
            {
                panel.Height = value;
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

        const string EXCESS_PHRASE = "You might also like";
        const int LINES_AMOUNT_AFTER_EXCESS_PHRASE = 7;

        internal LyricsBox(Panel panel, Font font, VScrollBar scrollBar)
        {
            this.panel = panel;
            this.font = font;
            this.scrollBar = scrollBar;
            scrollBar.ValueChanged += ScrollBar_ValueChanged;
        }

        internal string this[int rowIndex]
        {
            get => lines[rowIndex];
        }

        private void ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            // Q I should lift up some labels, hide the topmost and show the lowest

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
            lines = lyrics.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
            CutExcessPhrase();
            AddDistanceBeforeKeyWords();
            labelsWithLyrics = new Label[lines.Count];
            ConfigureLabels();
            CalculateWidth();
            ScaleScrollBar();
        }

        private void AddDistanceBeforeKeyWords()
        {
            int countOfInsertedLines = 0;
            int actualIndex;
            int linesAmount = lines.Count;
            for (int i = 1; i < linesAmount - 2; i++)
            {
                actualIndex = i + countOfInsertedLines;
                #warning a little wrong because here i should check on keyword instead of just square parents
                if (lines[actualIndex].StartsWith("[") && lines[actualIndex].EndsWith("]"))
                {
                    if (lines[actualIndex - 1] != "")
                    {
                        lines.Insert(actualIndex, "");
                        countOfInsertedLines++;
                    }
                }
            }
        }

        /// <summary>
        /// If lyrics contains accidentaly cut phrase "You might also like..." then this method return the lyrics without that phrase.
        /// </summary>
        /// <param name="lyrics"></param>
        /// <returns></returns>
        private void CutExcessPhrase()
        {
            if (lines.Contains(EXCESS_PHRASE))
            {
                int ExcessPhraseIndex = lines.IndexOf(EXCESS_PHRASE);
                for(int i = 0; i < LINES_AMOUNT_AFTER_EXCESS_PHRASE; i++)
                {
                    lines.RemoveAt(ExcessPhraseIndex);
                }
            }
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
                ChangeBackColorIfKeywordWord(labelsWithLyrics[i]);
                panel.Controls.Add(labelsWithLyrics[i]);

                y += lineHeight;
            }
        }
        /// <summary>
        /// This method analyzes label and looking for keyword like [chorus] and then changes color if label contains keyword.
        /// </summary>
        /// <param name="label"></param>
        private void ChangeBackColorIfKeywordWord(Label label)
        {
            label.BackColor = GetNeedyColor(label.Text);
        }

        internal static Color GetNeedyColor(string text)
        {
            if (text.StartsWith("[") && text.EndsWith("]"))
            {
                return ContainedKeyword(text);
            }

            return Color.Transparent;
        }

        private static Color ContainedKeyword(string text)
        {
            string lowerCaseText = text.ToLower();
            foreach (string keyword in KeyWords)
            {
                if (lowerCaseText.Contains(keyword))
                {
                    switch (keyword)
                    {
                        case "intro":
                        case "интро":
                            return Color.Green;
                        case "verse":
                        case "куплет":
                            return Color.Pink;
                        case "pre-chorus":
                        case "предприпев":
                            return Color.Orange;
                        case "chorus":
                        case "припев":
                            return Color.Red;
                        case "bridge":
                        case "переход":
                        case "бридж":
                            return Color.Yellow;
                        case "autro":
                            return Color.Blue;
                    }
                }
            }
            return Color.LightGray;
        }

        private void CalculateWidth()
        {
            int maxWidth = labelsWithLyrics.Max(label => label.Width);
            Width = WIDTH_PADDING + maxWidth + WIDTH_PADDING + scrollBar.Width;
        }

        private void ScaleScrollBar()
        {
            // Q it works little incorrect when application works in window mode I think
            scrollBar.Maximum = labelsWithLyrics.Length;
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
            if (panel.Controls.Count != 1)
            {
                foreach (var label in labelsWithLyrics)
                {
                    label.Dispose();
                }
            }
        }

        internal void AdjustScrollBar()
        {
#warning it doesn't work
            scrollBar.Maximum = GetAmountOfNotVisibleLabels(labelsWithLyrics.ToList());
        }

        private bool LabelIsNotVisible(Label label)
        {
            return label.Top >= Height;
        }

        private int GetAmountOfNotVisibleLabels(List<Label> labels)
        {
            if (labels.Count == 0)
            {
                return 0;
            }

            Label midLabel = labels[labels.Count / 2];
            if (LabelIsNotVisible(midLabel))
            {
                return 1 + labels.Count / 2 + GetAmountOfNotVisibleLabels(labels.TakeWhile(label => label != midLabel).ToList());
            }

            else
            {
                return GetAmountOfNotVisibleLabels(labels.SkipWhile(label => label != midLabel)
                                                         .Skip(1)
                                                         .ToList());
            }
        }

        internal bool IsKeywordAtLine(int rowIndex)
        {
            if (rowIndex >= lines.Count)
            {
                return false;
            }
            else
            {
                return lines[rowIndex].StartsWith("]") && lines[rowIndex].EndsWith("]");
            }
        }

        private bool IsKeyword(string word)
        {
            return KeyWords.Contains(word);
        }
    }
}