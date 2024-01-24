using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Blopnote
{
    internal class FileNameAndLyricsInputWindow : Form
    {
        private Label label1;
        private TextBox TextBoxForFileName;
        private TextBox TextBoxForLyrics;
        private CheckBox CheckBoxUseLyrics;
        private Button button1;
        private Label label2;

        internal string FileName { get; set; }
        internal string Lyrics { get; set; }

        #warning how can i handle lyrics here
        internal bool IsDataInserted => FileName != null;

        internal FileNameAndLyricsInputWindow()
        {
            InitializeComponent();
        }

        [STAThread]
        internal void ShowForDataInput()
        {
            ResetAllComponents();

            DialogResult userAnswer = this.ShowDialog();

            if (userAnswer == DialogResult.OK)
            {
                FileName = TextBoxForFileName.Text;
                Lyrics = TextBoxForLyrics.Text;
            }
            
            #warning
        }

        private void ResetAllComponents()
        {
            FileName = null;
            Lyrics = null;
            TextBoxForFileName.Clear();
            TextBoxForLyrics.Clear();
            CheckBoxUseLyrics.Checked = true;
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBoxForFileName = new System.Windows.Forms.TextBox();
            this.TextBoxForLyrics = new System.Windows.Forms.TextBox();
            this.CheckBoxUseLyrics = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter file name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enter lyrics:";
            // 
            // TextBoxForFileName
            // 
            this.TextBoxForFileName.Location = new System.Drawing.Point(223, 37);
            this.TextBoxForFileName.MaxLength = 255;
            this.TextBoxForFileName.Name = "TextBoxForFileName";
            this.TextBoxForFileName.Size = new System.Drawing.Size(324, 30);
            this.TextBoxForFileName.TabIndex = 2;
            // 
            // TextBoxForLyrics
            // 
            this.TextBoxForLyrics.Location = new System.Drawing.Point(223, 131);
            this.TextBoxForLyrics.Multiline = true;
            this.TextBoxForLyrics.Name = "TextBoxForLyrics";
            this.TextBoxForLyrics.Size = new System.Drawing.Size(324, 311);
            this.TextBoxForLyrics.TabIndex = 3;
            // 
            // CheckBoxUseLyrics
            // 
            this.CheckBoxUseLyrics.AutoSize = true;
            this.CheckBoxUseLyrics.Location = new System.Drawing.Point(223, 85);
            this.CheckBoxUseLyrics.Name = "CheckBoxUseLyrics";
            this.CheckBoxUseLyrics.Size = new System.Drawing.Size(129, 26);
            this.CheckBoxUseLyrics.TabIndex = 4;
            this.CheckBoxUseLyrics.Text = "Use lyrics";
            this.CheckBoxUseLyrics.UseVisualStyleBackColor = true;
            this.CheckBoxUseLyrics.CheckedChanged += new System.EventHandler(this.CheckBoxUseLyrics_CheckedChanged);
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(240, 466);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 32);
            this.button1.TabIndex = 5;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OK_Click);
            // 
            // FileNameAndLyricsInputWindow
            // 
            this.ClientSize = new System.Drawing.Size(645, 523);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CheckBoxUseLyrics);
            this.Controls.Add(this.TextBoxForLyrics);
            this.Controls.Add(this.TextBoxForFileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FileNameAndLyricsInputWindow";
            this.Text = "New file creating";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileNameAndLyricsInputWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void CheckBoxUseLyrics_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxUseLyrics.Checked)
            {
                TextBoxForLyrics.Enabled = true;
            }
            else
            {
                TextBoxForLyrics.Enabled = false;
                TextBoxForLyrics.Clear();
            }
        }

        private void FileNameAndLyricsInputWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void OK_Click(object sender, EventArgs e)
        {

        }
    }
}