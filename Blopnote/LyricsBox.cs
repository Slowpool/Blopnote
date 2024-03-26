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

        private Label PreviousHighlightedLabel { get; set; }

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

        private int LinesCapacity => (panel.Height - HEIGHT_PADDING) / lineHeight;
        private int AmountOfInvisibleLabels => lines.Count - LinesCapacity;

        private int scrollBarValue
        {
            get => scrollBar.Value;
            set
            {
                if (value >= scrollBar.Minimum && value <= scrollBar.Maximum - 9)
                {
                    scrollBar.Value = value;
                }
            }
        }

        const int WIDTH_PADDING = 10;
        const int HEIGHT_PADDING = 10;

        const string EXCESS_PHRASE = "You might also like";
        const int LINES_AMOUNT_AFTER_EXCESS_PHRASE = 7;

        private readonly int lineHeight;

        internal LyricsBox(Panel panel, Font font, VScrollBar scrollBar)
        {
            this.panel = panel;
            this.font = font;
            this.scrollBar = scrollBar;

            panel.MouseWheel += PanelForLyricsBox_MouseWheel;
            scrollBar.ValueChanged += ScrollBar_ValueChanged;
            // Q WHY DO I HAVE TO SUBTRACT 1 FROM FONT HEIGHT HERE FOR CORRECT DISPLAYING OF ROWS?
            lineHeight = font.Height - 1;
        }

        private void PanelForLyricsBox_MouseWheel(object sender, MouseEventArgs e)
        {
            int deltaValue = e.Delta / 120;
            scrollBarValue -= deltaValue;
        }

        internal string this[int lineIndex]
        {
            get => lines[lineIndex];
        }

        private void ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            // Q I should lift up some labels, hide the topmost and show the lowest
            PlaceLabels();
        }

        /// <summary>
        /// This method creates panel with labels. One line of text = one lable.
        /// Text has a keywords, like [Pre-Chorus], [Chorus], [Бридж], [Переход],
        /// which are not inserted by user. Also they have a some background color, e.g. green for chorus.
        /// </summary>
        /// <param name="lyrics"></param>
        internal string BuildNewLyricsAndGetEditedVersion(string lyrics)
        {
#warning unfinished
            lines = lyrics.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
            CutExcessPhrase();
            AddDistanceBeforeKeyWords();
            labelsWithLyrics = new Label[lines.Count];
            ConfigureLabels();
            CalculateWidth();
            AdjustScrollBar();
            return lines.Aggregate("", (total, line) => total + line + "\r\n");
        }

        private void AddDistanceBeforeKeyWords()
        {
            int added = 0;
            int indexConsideringOfAdded;
            int amountOfLines = lines.Count;
            for (int i = 0; i < amountOfLines; i++)
            {
                indexConsideringOfAdded = i + added;
                if (IsKeyword(lines[indexConsideringOfAdded]))
                {
                    if (indexConsideringOfAdded == 0 || lines[indexConsideringOfAdded - 1] != "")
                    {
                        lines.Insert(indexConsideringOfAdded, "");
                        added++;
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
            for (int i = 0; i < labelsWithLyrics.Length; i++)
            {
                labelsWithLyrics[i] = new Label();
                labelsWithLyrics[i].Font = font;
                labelsWithLyrics[i].Text = lines[i];

                labelsWithLyrics[i].Left = WIDTH_PADDING;
                labelsWithLyrics[i].AutoSize = true;
                ChangeBackColorIfContainsKeyword(labelsWithLyrics[i]);
                panel.Controls.Add(labelsWithLyrics[i]);
            }
            PlaceLabels();
        }

        internal void PlaceLabels()
        {
#warning i have three ideas here and both are awkward: 
            // 1. at first disable all labels, next enable needed
            // 2. a) disable before needed
            // b) enable needed
            // c) disable after needed
            // 3. reproduce step 2 using one cycle
            int y = 0;
            for (int i = 0; i < labelsWithLyrics.Length; i++)
            {
                if (i < scrollBar.Value)
                {
                    labelsWithLyrics[i].Visible = false;
                }
                else if (i < scrollBar.Value + LinesCapacity)
                {
                    labelsWithLyrics[i].Visible = true;
                    labelsWithLyrics[i].Top = y;
                    y += lineHeight;
                }
                else
                {
                    labelsWithLyrics[i].Visible = false;
                }
            }
        }

        /// <summary>
        /// This method analyzes label and looking for keyword like [chorus] and then changes color if label contains keyword.
        /// </summary>
        /// <param name="label"></param>
        private void ChangeBackColorIfContainsKeyword(Label label)
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
#warning terrible dream of developer
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
            int maxWidthOfLines = labelsWithLyrics.Max(label => label.Width);
            Width = WIDTH_PADDING + maxWidthOfLines + WIDTH_PADDING + scrollBar.Width;
        }

        internal void NoLyrics()
        {
            Hide();
            ClearPreviousLyricsDisplayIfNeed();

            lines = null;
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
            scrollBar.Maximum = AmountOfInvisibleLabels + 9;
        }

        internal bool IsKeywordAtLine(int lineIndex)
        {
            if (lineIndex >= lines.Count)
            {
                return false;
            }
            else
            {
                return IsKeyword(lines[lineIndex]);
            }
        }

        private bool IsKeyword(string word)
        {
            return word.StartsWith("[") && word.EndsWith("]");
        }

        internal TypesOfLine IsRepeatedLineOrKeyword(int lineIndex)
        {
            if (lineIndex >= lines.Count)
            {
                return TypesOfLine.New;
            }
            
            if (lines[lineIndex] == string.Empty)
            {
                return TypesOfLine.Empty;
            }

            if (IsKeyword(lines[lineIndex]))
            {
                return TypesOfLine.Keyword;
            }

            for(int i = 0; i < lineIndex; i++)
            {
                string s1 = string.Intern(lines[i]);
                string s2 = string.Intern(lines[lineIndex]);
#warning dude remove it
                //char[] s1Characters = s1.ToCharArray();
                //char[] s2Characters = s2.ToCharArray();
                if (s1 == s2)
                {
                    return TypesOfLine.Repeated;
                }
                //int number = s1Characters.Length;
                //int number2 = s2Characters.Length;
                //int age = number + number2;
            }

            return TypesOfLine.New;
        }

        internal int IndexOfFirstOccurenceOfSameLine(int lineIndex)
        {
            string line = lines[lineIndex];
            return lines.IndexOf(line);
        }

        internal void HighlightAt(int lineIndex)
        {
            if (lines == null || lineIndex >= lines.Count)
            {
                ReleaseHighlightedLabel();
                return;
            }

            Label currentLabel = labelsWithLyrics[lineIndex];

            if (currentLabel == PreviousHighlightedLabel)
            {
                return;
            }

            ReleaseHighlightedLabel();
            if (!IsKeyword(currentLabel.Text))
            {
                currentLabel.BackColor = Color.LightSteelBlue;
                PreviousHighlightedLabel = currentLabel;
            }
        }

        private void ReleaseHighlightedLabel()
        {
            // In case when previous label is keyword, than it'll be null
            try
            {
                PreviousHighlightedLabel.BackColor = Color.Transparent;
            }
            catch { }
            PreviousHighlightedLabel = null;
        }
    }
}