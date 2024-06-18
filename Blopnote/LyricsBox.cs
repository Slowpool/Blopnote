using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.CompilerServices;

using OpenQA.Selenium;
using SeleniumKeys = OpenQA.Selenium.Keys;
using static Blopnote.Browser;
using System.Diagnostics;

namespace Blopnote
{
    internal class LyricsBox
    {
        internal readonly Panel panel;
        private readonly Font font;
        private readonly VScrollBar scrollBar;
        private readonly ToolTip toolTipLyrics;
        private List<string> Lines { get; set; }
        internal int LinesQuantity => Lines.Count;
        private string Lyrics => Lines.Aggregate(string.Empty, (total, line) => total + line + "\r\n");
        internal static string[] KeyWords = "intro интро verse pre-chorus chorus bridge autro предприпев припев переход бридж куплет аутро".Split();
        private Label[] LabelsWithLyrics { get; set; }

        private Label HighlightedLabel { get; set; }
        private int IndexHighlightedLabel { get; set; }

        internal event EventHandler TranslationByGoogleLoaded;
        private string[] TranslationByGoogle { get; set; }
        private bool TranslateOnly1Line { get; set; }

        internal bool Enabled => panel.Visible;

        // I think it's bad idea to use so many levels of incapsulation for properties...
        internal int LyricsBoxWidth
        {
            get => panel.Width;
            set
            {
                panel.Width = value;
            }
        }
        private int LyricsBoxHeight
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

        private int LinesCapacity => (panel.Height - VERTICAL_PADDING) / lineHeight;
        private int AmountOfInvisibleLabels => Lines.Count <= LinesCapacity ? 0 : Lines.Count - LinesCapacity;

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

        private readonly Color HIGHLIGHTED_LABEL = Color.LightSteelBlue;
        private const int HORIZONTAL_PADDING = 10;
        private const int VERTICAL_PADDING = 10;
        private const int MAX_WIDTH = 800;
        // manually copied lyrics from genius could contain such redundant infromation
        private const string EXCESS_PHRASE = "You might also like";
        private const int LINES_AFTER_EXCESS_PHRASE = 7;

        private readonly int lineHeight;

        internal LyricsBox(Panel panel, Font font, VScrollBar scrollBar, ToolTip toolTipLyrics)
        {
            this.panel = panel;
            this.font = font;
            this.scrollBar = scrollBar;
            this.toolTipLyrics = toolTipLyrics;

            FillLanguages();

            panel.MouseWheel += PanelForLyricsBox_MouseWheel;
            scrollBar.ValueChanged += ScrollBar_ValueChanged;
            // Q WHY DO I HAVE TO SUBTRACT 1 FROM FONT HEIGHT HERE FOR CORRECT DISPLAYING OF ROWS?
            lineHeight = font.Height - 1;
        }

        private void FillLanguages()
        {
#warning implement it
            //var list = Language
        }

        private void PanelForLyricsBox_MouseWheel(object sender, MouseEventArgs e)
        {
            int deltaValue = e.Delta / 120;
            scrollBarValue -= deltaValue;
        }

        internal string this[int lineIndex]
        {
            get => Lines[lineIndex];
        }

        private void ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            PlaceLabels();
        }

        /// <summary>
        /// This method displays panel with labels. One line of text = one lable.
        /// In Genius.com text has a keywords like [Pre-Chorus], [Chorus], [Бридж], [Переход],
        /// that the user shouldn't enter. Also they have a some background color,
        /// e.g. green for [Chorus] and etc. It's supposed that these keywords will help user
        /// better navigate the text
        /// </summary>
        /// <param name="lyrics"></param>
        internal string FilterAndStore(string lyrics)
        {
            Lines = lyrics.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
            CutExcessPhrase();
            HighlightKeywords();
            TrimLines();
            LabelsWithLyrics = new Label[Lines.Count];
            ConfigureLabels();
            TranslationByGoogle = GetTranslationByGoogle(Lyrics);
            TranslationByGoogleLoaded(this, null);
            CalculateWidth();
            AdjustScrollBar();
            return Lyrics;
        }

        private void TrimLines()
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                if (Lines[i].StartsWith(" ") || Lines[i].EndsWith(" "))
                {
                    Lines[i] = Lines[i].Trim();
                }
            }
            while (string.IsNullOrWhiteSpace(Lines[Lines.Count - 1]))
            {
                Lines.RemoveAt(Lines.Count - 1);
            }
        }

        private void HighlightKeywords()
        {
            SelectKeywordsAsIndividualLines(0);
            int realIndex;
            int added = 0;
            int numberOfLines = Lines.Count;
            // last line can't be keyword and first line is already correct
            // so they don't need to be checked
            // and even if last line is keyword it's ok
            // but if the last line looks like [chorus]lyrics lyrics[chorus][bridge]
            // then it's kind of strage and i'll ignore that

            for (int i = 1; i < numberOfLines; i++)
            {
                realIndex = i + added;
                if (ContainsKeyword(Lines[realIndex]))
                {
                    added += SelectKeywordsAsIndividualLines(realIndex);
                }
            }
            added = 0;
            numberOfLines = Lines.Count;
            for (int i = 1; i < numberOfLines - 1; i++)
            {
                realIndex = i + added;
                if (ContainsKeyword(Lines[realIndex]))
                {
                    added += EnsureIntendBefore(realIndex);
                }
            }
        }

        private int SelectKeywordsAsIndividualLines(int lineIndex)
        {
#warning still bad code even after remaking
            #region interesting note
            // it looks like issue from leetcode like:
            // given a string of words, spaces and other characters and substrings
            // that contain some phrases in brackets. example:
            //
            //   haha[nice] that was a [mistake][to trust the bread] after all he did for you
            //
            // you have to select words and keywords in individual lines like:
            //
            //   haha
            //   [nice]
            //    that was a
            //   [mistake]
            //   [to trust the bread]
            //    after all he did for you
            #endregion

            // "[keyword]" or "text"
            if (IsKeyword(Lines[lineIndex]) || !ContainsKeyword(Lines[lineIndex]))
            {
                return 0;
            }
            // [key]any_text[word]
            if (Lines[lineIndex].StartsWith("["))
            {
                string[] twoParts = Lines[lineIndex].Split(new[] { ']' }, 2);
                string keyword = twoParts[0] + ']';
                string restOfLine = twoParts[1];
                Lines.Insert(lineIndex, keyword);
                lineIndex++;
                Lines[lineIndex] = restOfLine;
            }
            else
            {
                string[] twoParts = Lines[lineIndex].Split(new[] { '[' }, 2);
                //string keyword = lines[lineIndex].Substring(0, lines[lineIndex].IndexOf(']') + 1);
                string text = twoParts[0];
                string restOfLine = '[' + twoParts[1];
                Lines.Insert(lineIndex, text);
                Lines[++lineIndex] = restOfLine;
            }
            return 1 + SelectKeywordsAsIndividualLines(lineIndex);
        }

        private int EnsureIntendBefore(int lineIndex)
        {
            if (Lines[lineIndex - 1] != string.Empty)
            {
                Lines.Insert(lineIndex, string.Empty);
                return 1;
            }
            return 0;
        }

        private int CheckBothSidesOfLine(int lineIndex)
        {
            int added = 0;

            if (Lines[lineIndex + added + 1] != string.Empty)
            {
                Lines.Insert(lineIndex, string.Empty);
                added++;
            }
            return added;
        }

        /// <summary>
        /// If lyrics contains accidentaly cut phrase "You might also like..." then this method return the lyrics without that phrase.
        /// </summary>
        /// <param name="lyrics"></param>
        /// <returns></returns>
        private void CutExcessPhrase()
        {
            if (Lines.Contains(EXCESS_PHRASE))
            {
                int ExcessPhraseIndex = Lines.IndexOf(EXCESS_PHRASE);
                for (int i = 0; i < LINES_AFTER_EXCESS_PHRASE; i++)
                {
                    Lines.RemoveAt(ExcessPhraseIndex);
                }
            }
        }

        private void ConfigureLabels()
        {
            for (int i = 0; i < LabelsWithLyrics.Length; i++)
            {
                LabelsWithLyrics[i] = new Label();
                LabelsWithLyrics[i].Font = font;
                LabelsWithLyrics[i].Text = Lines[i];

                LabelsWithLyrics[i].Left = HORIZONTAL_PADDING;
                LabelsWithLyrics[i].AutoSize = true;
                ChangeBackColorIfContainsKeyword(LabelsWithLyrics[i]);
                panel.Controls.Add(LabelsWithLyrics[i]);
            }
        }

        internal void PlaceLabels()
        {
#warning i have three ideas here and each is awkward: 
            // 1. at first disable all labels, next enable needed
            // 2. a) disable before needed
            // b) enable needed
            // c) disable after needed
            // 3. reproduce step 2 using one cycle
            int y = 0;
            for (int i = 0; i < LabelsWithLyrics.Length; i++)
            {
                if (i < scrollBar.Value)
                {
                    LabelsWithLyrics[i].Visible = false;
                }
                else if (i < scrollBar.Value + LinesCapacity)
                {
                    LabelsWithLyrics[i].Visible = true;
                    LabelsWithLyrics[i].Top = y;
                    y += lineHeight;
                }
                else
                {
                    LabelsWithLyrics[i].Visible = false;
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
            // attempt to calculate max width including width of labels with translation
            // but there's string[] TranslatedLyrics and it's impossible to calculate width of strings
            // because characters have different width so it'll spaghetti-code
            // UPD: got it. i don't need in several labels, I need in only one (I decided to take first one
            // because it's already configurated in appropriate way.

            string temp = LabelsWithLyrics[0].Text;

            var fakeLabel = LabelsWithLyrics[0];
            var TranslatedLinesWidth = TranslationByGoogle.Select(line =>
            {
                fakeLabel.Text = line;
                return fakeLabel.Width;
            });

            int maxWidthOfLines = LabelsWithLyrics.Select(label => label.Width)
                                                  .Concat(TranslatedLinesWidth)
                                                  .Max();
            fakeLabel.Text = temp;

            LyricsBoxWidth = HORIZONTAL_PADDING + maxWidthOfLines + HORIZONTAL_PADDING + scrollBar.Width;
            if (LyricsBoxWidth > MAX_WIDTH)
            {
                LyricsBoxWidth = MAX_WIDTH;
            }
        }

        internal void NoLyrics()
        {
            Hide();
            EnsureCleared();

            Lines = null;
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
            LyricsBoxHeight = height;
        }

        internal void EnsureCleared()
        {
            if (panel.Controls.Count != 1) // 1 is scrollbar
            {
                foreach (var label in LabelsWithLyrics)
                {
                    label.Dispose();
                }
            }
        }

        internal void AdjustScrollBar()
        {
            // Q: emmm what is 9 here?
            // A: length of scrollBar
            // Conclusions: use const value
            scrollBar.Maximum = AmountOfInvisibleLabels + 9;
        }

        internal bool ContainsKeyword(string line)
        {
            int openingBracketIndex = line.IndexOf('[');
            int closingBracketIndex = line.IndexOf(']');
            return openingBracketIndex != -1
                && closingBracketIndex != -1
                && openingBracketIndex < closingBracketIndex;
        }

        private bool IsKeyword(string word)
        {
            return word.StartsWith("[")
                && word.EndsWith("]")
                // checking for [some key word]. word also could be like [some]text[key]text[word]
                // that'll return true cause '[' at the beginning and ']' at the end
                && word.IndexOf(']') == word.Length - 1;
        }

        internal TypesOfLine IsRepeatedLineOrKeyword(int lineIndex)
        {
            if (lineIndex >= Lines.Count)
            {
                return TypesOfLine.New;
            }

            if (Lines[lineIndex] == string.Empty)
            {
                return TypesOfLine.Empty;
            }

            if (IsKeyword(Lines[lineIndex]))
            {
                return TypesOfLine.Keyword;
            }

            for (int i = 0; i < lineIndex; i++)
            {
                string s1 = Lines[i];
                string s2 = Lines[lineIndex];
                if (s1 == s2)
                {
                    return TypesOfLine.Repeated;
                }
            }

            return TypesOfLine.New;
        }

        internal int IndexOfFirstOccurenceOfSameLine(int lineIndex)
        {
            string line = Lines[lineIndex];
            return Lines.IndexOf(line);
        }

        internal void HighlightAt(int lineIndex)
        {
            if (Lines == null)
            {
#warning is it possible?
                return;
            }

            if (lineIndex >= Lines.Count)
            {
                ReleaseHighlightedLabel();
                return;
            }

            Label currentLabel = LabelsWithLyrics[lineIndex];

            if (currentLabel == HighlightedLabel)
            {
                return;
            }

            ReleaseHighlightedLabel();
            if (!IsKeyword(currentLabel.Text))
            {
                currentLabel.BackColor = HIGHLIGHTED_LABEL;
                HighlightedLabel = currentLabel;
                IndexHighlightedLabel = lineIndex;
            }
        }

        private void ReleaseHighlightedLabel()
        {
            // In case when previous label is keyword, than it'll be null
            try
            {
                HighlightedLabel.BackColor = Color.Transparent;
                HighlightedLabel = null;
            }
            catch
            { }
        }

        internal void ResetScrollBar(object sender, EventArgs e)
        {
            scrollBar.Value = 0;
        }

        internal void PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.Tab)
            {
                ((TextBox)sender).KeyUp += KeyUp;
                if (TranslateOnly1Line)
                {
                    try
                    {
                        HighlightedLabel.Text = TranslationByGoogle[IndexHighlightedLabel];
                    }
                    catch
                    { }
                }
                else
                {
                    for (int i = 0; i < LabelsWithLyrics.Length; i++)
                    {
                        if (!IsKeyword(LabelsWithLyrics[i].Text))
                        {
                            LabelsWithLyrics[i].Text = TranslationByGoogle[i];
                        }
                    }
                }
            }
        }

        private void KeyUp(object sender, KeyEventArgs e)
        {
            if (TranslateOnly1Line)
            {
                try
                {
                    HighlightedLabel.Text = Lines[IndexHighlightedLabel];
                }
                catch
                { }
            }
            else
            {
                for (int i = 0; i < LabelsWithLyrics.Length; i++)
                {
                    LabelsWithLyrics[i].Text = Lines[i];
                }
            }
            ((TextBox)sender).KeyUp -= KeyUp;
        }

        internal void SwitchTabMode(object sender, EventArgs e)
        {
            TranslateOnly1Line = !TranslateOnly1Line;
        }
    }
}