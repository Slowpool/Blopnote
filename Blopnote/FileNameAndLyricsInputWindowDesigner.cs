using System;
using System.Windows.Forms;
using System.Drawing;

namespace Blopnote
{
    internal partial class FileNameAndLyricsInputWindow: Form
    {
        private Label label1;
        private TextBox TextBoxForAuthor;
        private TextBox TextBoxForLyrics;
        private CheckBox CheckBoxUseLyrics;
        private Button ButtonOK;
        private TextBox TextBoxForSong;
        private Label label3;
        private Label label2;

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBoxForAuthor = new System.Windows.Forms.TextBox();
            this.TextBoxForLyrics = new System.Windows.Forms.TextBox();
            this.CheckBoxUseLyrics = new System.Windows.Forms.CheckBox();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.TextBoxForSong = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Author pseudonym:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lyrics:";
            // 
            // TextBoxForAuthor
            // 
            this.TextBoxForAuthor.Location = new System.Drawing.Point(258, 34);
            this.TextBoxForAuthor.MaxLength = 120;
            this.TextBoxForAuthor.Name = "TextBoxForAuthor";
            this.TextBoxForAuthor.Size = new System.Drawing.Size(432, 30);
            this.TextBoxForAuthor.TabIndex = 1;
            // 
            // TextBoxForLyrics
            // 
            this.TextBoxForLyrics.Location = new System.Drawing.Point(258, 187);
            this.TextBoxForLyrics.Multiline = true;
            this.TextBoxForLyrics.Name = "TextBoxForLyrics";
            this.TextBoxForLyrics.Size = new System.Drawing.Size(432, 277);
            this.TextBoxForLyrics.TabIndex = 4;
            // 
            // CheckBoxUseLyrics
            // 
            this.CheckBoxUseLyrics.AutoSize = true;
            this.CheckBoxUseLyrics.Location = new System.Drawing.Point(258, 131);
            this.CheckBoxUseLyrics.Name = "CheckBoxUseLyrics";
            this.CheckBoxUseLyrics.Size = new System.Drawing.Size(129, 26);
            this.CheckBoxUseLyrics.TabIndex = 3;
            this.CheckBoxUseLyrics.Text = "Use lyrics";
            this.CheckBoxUseLyrics.UseVisualStyleBackColor = true;
            this.CheckBoxUseLyrics.CheckedChanged += new System.EventHandler(this.CheckBoxUseLyrics_CheckedChanged);
            // 
            // ButtonOK
            // 
            this.ButtonOK.AutoSize = true;
            this.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonOK.Location = new System.Drawing.Point(310, 515);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(160, 32);
            this.ButtonOK.TabIndex = 5;
            this.ButtonOK.Text = "OK";
            this.ButtonOK.UseVisualStyleBackColor = true;
            this.ButtonOK.Click += new System.EventHandler(this.OK_Click);
            // 
            // TextBoxForSong
            // 
            this.TextBoxForSong.Location = new System.Drawing.Point(258, 82);
            this.TextBoxForSong.MaxLength = 120;
            this.TextBoxForSong.Name = "TextBoxForSong";
            this.TextBoxForSong.Size = new System.Drawing.Size(432, 30);
            this.TextBoxForSong.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "Song:";
            // 
            // FileNameAndLyricsInputWindow
            // 
            this.ClientSize = new System.Drawing.Size(780, 559);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBoxForSong);
            this.Controls.Add(this.ButtonOK);
            this.Controls.Add(this.CheckBoxUseLyrics);
            this.Controls.Add(this.TextBoxForLyrics);
            this.Controls.Add(this.TextBoxForAuthor);
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
    }
}