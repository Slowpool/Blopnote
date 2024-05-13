using System.Windows.Forms;
using System.Drawing;

namespace Blopnote
{
    partial class CreateNewTranslationWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Label label1;
        private TextBox TextBoxForAuthor;
        private TextBox TextBoxForLyrics;
        private CheckBox CheckBoxUseLyrics;
        private Button buttonOK;
        private TextBox TextBoxForSong;
        private Label label3;
        private Button buttonLyricsRequest;
        private Button buttonAudioFileRequest;
        private ToolTip toolTipLyricsRequest;
        private System.ComponentModel.IContainer components;
        private Label label2;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBoxForAuthor = new System.Windows.Forms.TextBox();
            this.TextBoxForLyrics = new System.Windows.Forms.TextBox();
            this.CheckBoxUseLyrics = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.TextBoxForSong = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonLyricsRequest = new System.Windows.Forms.Button();
            this.buttonAudioFileRequest = new System.Windows.Forms.Button();
            this.toolTipLyricsRequest = new System.Windows.Forms.ToolTip(this.components);
            this.buttonNextLyrics = new System.Windows.Forms.Button();
            this.buttonPreviousLyrics = new System.Windows.Forms.Button();
            this.labelRequestResult = new System.Windows.Forms.Label();
            this.toolTipAudioFileRequest = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Author pseudonym:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lyrics:";
            // 
            // TextBoxForAuthor
            // 
            this.TextBoxForAuthor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TextBoxForAuthor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.TextBoxForAuthor.Location = new System.Drawing.Point(209, 32);
            this.TextBoxForAuthor.MaxLength = 100;
            this.TextBoxForAuthor.Name = "TextBoxForAuthor";
            this.TextBoxForAuthor.Size = new System.Drawing.Size(562, 30);
            this.TextBoxForAuthor.TabIndex = 1;
            this.TextBoxForAuthor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxForAuthor_KeyPress);
            // 
            // TextBoxForLyrics
            // 
            this.TextBoxForLyrics.Location = new System.Drawing.Point(209, 246);
            this.TextBoxForLyrics.Multiline = true;
            this.TextBoxForLyrics.Name = "TextBoxForLyrics";
            this.TextBoxForLyrics.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxForLyrics.Size = new System.Drawing.Size(562, 208);
            this.TextBoxForLyrics.TabIndex = 4;
            // 
            // CheckBoxUseLyrics
            // 
            this.CheckBoxUseLyrics.AutoSize = true;
            this.CheckBoxUseLyrics.Location = new System.Drawing.Point(209, 214);
            this.CheckBoxUseLyrics.Name = "CheckBoxUseLyrics";
            this.CheckBoxUseLyrics.Size = new System.Drawing.Size(129, 26);
            this.CheckBoxUseLyrics.TabIndex = 3;
            this.CheckBoxUseLyrics.Text = "Use lyrics";
            this.CheckBoxUseLyrics.UseVisualStyleBackColor = true;
            this.CheckBoxUseLyrics.CheckedChanged += new System.EventHandler(this.CheckBoxUseLyrics_CheckedChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.AutoSize = true;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(378, 515);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(160, 32);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.OK_Click);
            // 
            // TextBoxForSong
            // 
            this.TextBoxForSong.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TextBoxForSong.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.TextBoxForSong.Location = new System.Drawing.Point(209, 80);
            this.TextBoxForSong.MaxLength = 100;
            this.TextBoxForSong.Name = "TextBoxForSong";
            this.TextBoxForSong.Size = new System.Drawing.Size(562, 30);
            this.TextBoxForSong.TabIndex = 2;
            this.TextBoxForSong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxForAuthor_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "Song:";
            // 
            // buttonLyricsRequest
            // 
            this.buttonLyricsRequest.Location = new System.Drawing.Point(209, 130);
            this.buttonLyricsRequest.Name = "buttonLyricsRequest";
            this.buttonLyricsRequest.Size = new System.Drawing.Size(231, 30);
            this.buttonLyricsRequest.TabIndex = 8;
            this.buttonLyricsRequest.Text = "Request for lyrics";
            this.toolTipLyricsRequest.SetToolTip(this.buttonLyricsRequest, "It yields no more than 5 lyrics.");
            this.buttonLyricsRequest.UseVisualStyleBackColor = true;
            this.buttonLyricsRequest.Click += new System.EventHandler(this.buttonSearchOnGenius_Click);
            // 
            // buttonAudioFileRequest
            // 
            this.buttonAudioFileRequest.Location = new System.Drawing.Point(505, 130);
            this.buttonAudioFileRequest.Name = "buttonAudioFileRequest";
            this.buttonAudioFileRequest.Size = new System.Drawing.Size(266, 30);
            this.buttonAudioFileRequest.TabIndex = 9;
            this.buttonAudioFileRequest.Text = "Request for audio file";
            this.toolTipAudioFileRequest.SetToolTip(this.buttonAudioFileRequest, "It yields no more than 5 audio files.");
            this.buttonAudioFileRequest.UseVisualStyleBackColor = true;
            // 
            // toolTipLyricsRequest
            // 
            this.toolTipLyricsRequest.AutoPopDelay = 32767;
            this.toolTipLyricsRequest.InitialDelay = 200;
            this.toolTipLyricsRequest.IsBalloon = true;
            this.toolTipLyricsRequest.ReshowDelay = 100;
            this.toolTipLyricsRequest.ToolTipTitle = "Execute song search request on Genius.com";
            // 
            // buttonNextLyrics
            // 
            this.buttonNextLyrics.Location = new System.Drawing.Point(606, 460);
            this.buttonNextLyrics.Name = "buttonNextLyrics";
            this.buttonNextLyrics.Size = new System.Drawing.Size(165, 30);
            this.buttonNextLyrics.TabIndex = 11;
            this.buttonNextLyrics.Text = "Next";
            this.buttonNextLyrics.UseVisualStyleBackColor = true;
            this.buttonNextLyrics.Click += new System.EventHandler(this.buttonNextLyrics_Click);
            // 
            // buttonPreviousLyrics
            // 
            this.buttonPreviousLyrics.Location = new System.Drawing.Point(209, 460);
            this.buttonPreviousLyrics.Name = "buttonPreviousLyrics";
            this.buttonPreviousLyrics.Size = new System.Drawing.Size(165, 30);
            this.buttonPreviousLyrics.TabIndex = 12;
            this.buttonPreviousLyrics.Text = "Previous lyrics";
            this.buttonPreviousLyrics.UseVisualStyleBackColor = true;
            this.buttonPreviousLyrics.Click += new System.EventHandler(this.buttonPreviousLyrics_Click);
            // 
            // labelRequestResult
            // 
            this.labelRequestResult.AutoSize = true;
            this.labelRequestResult.Location = new System.Drawing.Point(215, 176);
            this.labelRequestResult.Name = "labelRequestResult";
            this.labelRequestResult.Size = new System.Drawing.Size(0, 22);
            this.labelRequestResult.TabIndex = 10;
            // 
            // toolTipAudioFileRequest
            // 
            this.toolTipAudioFileRequest.AutoPopDelay = 32767;
            this.toolTipAudioFileRequest.InitialDelay = 200;
            this.toolTipAudioFileRequest.IsBalloon = true;
            this.toolTipAudioFileRequest.ReshowDelay = 100;
            this.toolTipAudioFileRequest.ToolTipTitle = "Execute audio file searching on *website*";
            // 
            // CreateNewTranslationWindow
            // 
            this.ClientSize = new System.Drawing.Size(914, 559);
            this.Controls.Add(this.buttonPreviousLyrics);
            this.Controls.Add(this.buttonNextLyrics);
            this.Controls.Add(this.labelRequestResult);
            this.Controls.Add(this.buttonAudioFileRequest);
            this.Controls.Add(this.buttonLyricsRequest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBoxForSong);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.CheckBoxUseLyrics);
            this.Controls.Add(this.TextBoxForLyrics);
            this.Controls.Add(this.TextBoxForAuthor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateNewTranslationWindow";
            this.Text = "New file creating";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileNameAndLyricsInputWindow_FormClosing);
            this.Load += new System.EventHandler(this.FileNameAndLyricsInputWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Label labelRequestResult;
        private Button buttonNextLyrics;
        private Button buttonPreviousLyrics;
        private ToolTip toolTipAudioFileRequest;
    }
}