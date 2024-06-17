using System.Windows.Forms;
using System.Drawing;

namespace Blopnote
{
    partial class CreateNewTranslationForm
    {
        private TextBox TextBoxForAuthor;
        private TextBox TextBoxForLyrics;
        private CheckBox CheckBoxUseLyrics;
        private Button buttonOK;
        private TextBox TextBoxForSong;
        private Button buttonLyricsRequest;
        private ToolTip toolTipLyricsRequest;
        private System.ComponentModel.IContainer components;

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
            this.TextBoxForAuthor = new System.Windows.Forms.TextBox();
            this.TextBoxForLyrics = new System.Windows.Forms.TextBox();
            this.CheckBoxUseLyrics = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.TextBoxForSong = new System.Windows.Forms.TextBox();
            this.buttonLyricsRequest = new System.Windows.Forms.Button();
            this.toolTipLyricsRequest = new System.Windows.Forms.ToolTip(this.components);
            this.buttonNextLyrics = new System.Windows.Forms.Button();
            this.buttonPreviousLyrics = new System.Windows.Forms.Button();
            this.labelLyricsRequestResult = new System.Windows.Forms.Label();
            this.toolTipAudioFileRequest = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxSong = new System.Windows.Forms.GroupBox();
            this.groupBoxLyrics = new System.Windows.Forms.GroupBox();
            this.groupBoxLanguage = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonRequestForURL = new System.Windows.Forms.Button();
            this.checkBoxStoreURL = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBoxSong.SuspendLayout();
            this.groupBoxLyrics.SuspendLayout();
            this.groupBoxLanguage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBoxForAuthor
            // 
            this.TextBoxForAuthor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TextBoxForAuthor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.TextBoxForAuthor.CausesValidation = false;
            this.TextBoxForAuthor.Location = new System.Drawing.Point(92, 23);
            this.TextBoxForAuthor.MaxLength = 100;
            this.TextBoxForAuthor.Name = "TextBoxForAuthor";
            this.TextBoxForAuthor.Size = new System.Drawing.Size(686, 30);
            this.TextBoxForAuthor.TabIndex = 1;
            this.TextBoxForAuthor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxForAuthor_KeyPress);
            this.TextBoxForAuthor.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxAuthorAndSong_Validating);
            this.TextBoxForAuthor.Validated += new System.EventHandler(this.TextBoxAuthorAndSong_Validated);
            // 
            // TextBoxForLyrics
            // 
            this.TextBoxForLyrics.CausesValidation = false;
            this.TextBoxForLyrics.Location = new System.Drawing.Point(21, 99);
            this.TextBoxForLyrics.Multiline = true;
            this.TextBoxForLyrics.Name = "TextBoxForLyrics";
            this.TextBoxForLyrics.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxForLyrics.Size = new System.Drawing.Size(378, 140);
            this.TextBoxForLyrics.TabIndex = 5;
            this.TextBoxForLyrics.WordWrap = false;
            this.TextBoxForLyrics.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxForLyrics_Validating);
            this.TextBoxForLyrics.Validated += new System.EventHandler(this.TextBoxForLyrics_Validated);
            // 
            // CheckBoxUseLyrics
            // 
            this.CheckBoxUseLyrics.AutoSize = true;
            this.CheckBoxUseLyrics.CausesValidation = false;
            this.CheckBoxUseLyrics.Location = new System.Drawing.Point(21, 36);
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
            this.buttonOK.Location = new System.Drawing.Point(210, 485);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(180, 32);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.OK_Click);
            // 
            // TextBoxForSong
            // 
            this.TextBoxForSong.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TextBoxForSong.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.TextBoxForSong.CausesValidation = false;
            this.TextBoxForSong.Location = new System.Drawing.Point(92, 59);
            this.TextBoxForSong.MaxLength = 100;
            this.TextBoxForSong.Name = "TextBoxForSong";
            this.TextBoxForSong.Size = new System.Drawing.Size(686, 30);
            this.TextBoxForSong.TabIndex = 2;
            this.TextBoxForSong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxForAuthor_KeyPress);
            this.TextBoxForSong.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxAuthorAndSong_Validating);
            this.TextBoxForSong.Validated += new System.EventHandler(this.TextBoxAuthorAndSong_Validated);
            // 
            // buttonLyricsRequest
            // 
            this.buttonLyricsRequest.CausesValidation = false;
            this.buttonLyricsRequest.Location = new System.Drawing.Point(156, 32);
            this.buttonLyricsRequest.Name = "buttonLyricsRequest";
            this.buttonLyricsRequest.Size = new System.Drawing.Size(243, 30);
            this.buttonLyricsRequest.TabIndex = 4;
            this.buttonLyricsRequest.Text = "Request for lyrics";
            this.toolTipLyricsRequest.SetToolTip(this.buttonLyricsRequest, "It yields no more than 5 lyrics.");
            this.buttonLyricsRequest.UseVisualStyleBackColor = true;
            this.buttonLyricsRequest.Click += new System.EventHandler(this.buttonLyricsRequest_Click);
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
            this.buttonNextLyrics.CausesValidation = false;
            this.buttonNextLyrics.Location = new System.Drawing.Point(234, 245);
            this.buttonNextLyrics.Name = "buttonNextLyrics";
            this.buttonNextLyrics.Size = new System.Drawing.Size(165, 30);
            this.buttonNextLyrics.TabIndex = 7;
            this.buttonNextLyrics.Text = "Next";
            this.buttonNextLyrics.UseVisualStyleBackColor = true;
            this.buttonNextLyrics.Click += new System.EventHandler(this.buttonNextLyrics_Click);
            // 
            // buttonPreviousLyrics
            // 
            this.buttonPreviousLyrics.CausesValidation = false;
            this.buttonPreviousLyrics.Location = new System.Drawing.Point(21, 245);
            this.buttonPreviousLyrics.Name = "buttonPreviousLyrics";
            this.buttonPreviousLyrics.Size = new System.Drawing.Size(165, 30);
            this.buttonPreviousLyrics.TabIndex = 6;
            this.buttonPreviousLyrics.Text = "Previous lyrics";
            this.buttonPreviousLyrics.UseVisualStyleBackColor = true;
            this.buttonPreviousLyrics.Click += new System.EventHandler(this.buttonPreviousLyrics_Click);
            // 
            // labelLyricsRequestResult
            // 
            this.labelLyricsRequestResult.AutoSize = true;
            this.labelLyricsRequestResult.Location = new System.Drawing.Point(26, 65);
            this.labelLyricsRequestResult.Name = "labelLyricsRequestResult";
            this.labelLyricsRequestResult.Size = new System.Drawing.Size(80, 22);
            this.labelLyricsRequestResult.TabIndex = 10;
            this.labelLyricsRequestResult.Text = "results";
            // 
            // toolTipAudioFileRequest
            // 
            this.toolTipAudioFileRequest.AutoPopDelay = 32767;
            this.toolTipAudioFileRequest.InitialDelay = 200;
            this.toolTipAudioFileRequest.IsBalloon = true;
            this.toolTipAudioFileRequest.ReshowDelay = 100;
            this.toolTipAudioFileRequest.ToolTipTitle = "Execute audio file searching on *website*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "Song:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Author:";
            // 
            // groupBoxSong
            // 
            this.groupBoxSong.Controls.Add(this.label1);
            this.groupBoxSong.Controls.Add(this.TextBoxForAuthor);
            this.groupBoxSong.Controls.Add(this.TextBoxForSong);
            this.groupBoxSong.Controls.Add(this.label3);
            this.groupBoxSong.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSong.Name = "groupBoxSong";
            this.groupBoxSong.Size = new System.Drawing.Size(822, 102);
            this.groupBoxSong.TabIndex = 16;
            this.groupBoxSong.TabStop = false;
            this.groupBoxSong.Text = "Song";
            this.groupBoxSong.Validating += new System.ComponentModel.CancelEventHandler(this.groupBoxSong_Validating);
            this.groupBoxSong.Validated += new System.EventHandler(this.groupBoxSong_Validated);
            // 
            // groupBoxLyrics
            // 
            this.groupBoxLyrics.Controls.Add(this.TextBoxForLyrics);
            this.groupBoxLyrics.Controls.Add(this.CheckBoxUseLyrics);
            this.groupBoxLyrics.Controls.Add(this.buttonLyricsRequest);
            this.groupBoxLyrics.Controls.Add(this.labelLyricsRequestResult);
            this.groupBoxLyrics.Controls.Add(this.buttonPreviousLyrics);
            this.groupBoxLyrics.Controls.Add(this.buttonNextLyrics);
            this.groupBoxLyrics.Location = new System.Drawing.Point(12, 120);
            this.groupBoxLyrics.Name = "groupBoxLyrics";
            this.groupBoxLyrics.Size = new System.Drawing.Size(418, 281);
            this.groupBoxLyrics.TabIndex = 16;
            this.groupBoxLyrics.TabStop = false;
            this.groupBoxLyrics.Text = "Lyrics";
            // 
            // groupBoxLanguage
            // 
            this.groupBoxLanguage.Controls.Add(this.label2);
            this.groupBoxLanguage.Controls.Add(this.comboBox2);
            this.groupBoxLanguage.Controls.Add(this.comboBox1);
            this.groupBoxLanguage.Enabled = false;
            this.groupBoxLanguage.Location = new System.Drawing.Point(12, 407);
            this.groupBoxLanguage.Name = "groupBoxLanguage";
            this.groupBoxLanguage.Size = new System.Drawing.Size(632, 72);
            this.groupBoxLanguage.TabIndex = 17;
            this.groupBoxLanguage.TabStop = false;
            this.groupBoxLanguage.Text = "Languages";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "to";
            // 
            // comboBox2
            // 
            this.comboBox2.CausesValidation = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(340, 29);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(277, 30);
            this.comboBox2.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.CausesValidation = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(21, 29);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(277, 30);
            this.comboBox1.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // buttonRequestForURL
            // 
            this.buttonRequestForURL.Location = new System.Drawing.Point(146, 32);
            this.buttonRequestForURL.Name = "buttonRequestForURL";
            this.buttonRequestForURL.Size = new System.Drawing.Size(182, 30);
            this.buttonRequestForURL.TabIndex = 11;
            this.buttonRequestForURL.Text = "Request for URL";
            this.buttonRequestForURL.UseVisualStyleBackColor = true;
            this.buttonRequestForURL.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxStoreURL
            // 
            this.checkBoxStoreURL.AutoSize = true;
            this.checkBoxStoreURL.Checked = true;
            this.checkBoxStoreURL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStoreURL.Location = new System.Drawing.Point(21, 36);
            this.checkBoxStoreURL.Name = "checkBoxStoreURL";
            this.checkBoxStoreURL.Size = new System.Drawing.Size(119, 26);
            this.checkBoxStoreURL.TabIndex = 12;
            this.checkBoxStoreURL.Text = "Store URL";
            this.checkBoxStoreURL.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.radioButton5);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.linkLabel5);
            this.groupBox1.Controls.Add(this.linkLabel4);
            this.groupBox1.Controls.Add(this.linkLabel3);
            this.groupBox1.Controls.Add(this.linkLabel2);
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.checkBoxStoreURL);
            this.groupBox1.Controls.Add(this.buttonRequestForURL);
            this.groupBox1.Location = new System.Drawing.Point(436, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 281);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "URL to song (youtube.com)";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(52, 104);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(14, 13);
            this.radioButton1.TabIndex = 13;
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(52, 142);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(14, 13);
            this.radioButton2.TabIndex = 14;
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(72, 99);
            this.linkLabel1.MaximumSize = new System.Drawing.Size(352, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(110, 22);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(72, 137);
            this.linkLabel2.MaximumSize = new System.Drawing.Size(352, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(110, 22);
            this.linkLabel2.TabIndex = 17;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "linkLabel2";
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(72, 175);
            this.linkLabel3.MaximumSize = new System.Drawing.Size(352, 0);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(110, 22);
            this.linkLabel3.TabIndex = 18;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "linkLabel3";
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(72, 213);
            this.linkLabel4.MaximumSize = new System.Drawing.Size(352, 0);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(110, 22);
            this.linkLabel4.TabIndex = 19;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "linkLabel4";
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(72, 249);
            this.linkLabel5.MaximumSize = new System.Drawing.Size(352, 0);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(110, 22);
            this.linkLabel5.TabIndex = 20;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "linkLabel5";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(52, 180);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(14, 13);
            this.radioButton3.TabIndex = 21;
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(52, 218);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(14, 13);
            this.radioButton4.TabIndex = 22;
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(52, 254);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(14, 13);
            this.radioButton5.TabIndex = 23;
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 22);
            this.label4.TabIndex = 24;
            this.label4.Text = "results";
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(6, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 32);
            this.button1.TabIndex = 25;
            this.button1.Text = "✂";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.Location = new System.Drawing.Point(6, 132);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 32);
            this.button2.TabIndex = 26;
            this.button2.Text = "✂";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.Location = new System.Drawing.Point(6, 170);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(40, 32);
            this.button3.TabIndex = 27;
            this.button3.Text = "✂";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.AutoSize = true;
            this.button4.Location = new System.Drawing.Point(6, 208);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(40, 32);
            this.button4.TabIndex = 28;
            this.button4.Text = "✂";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.AutoSize = true;
            this.button5.Location = new System.Drawing.Point(6, 246);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(40, 32);
            this.button5.TabIndex = 29;
            this.button5.Text = "✂";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // CreateNewTranslationForm
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(849, 548);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxLanguage);
            this.Controls.Add(this.groupBoxLyrics);
            this.Controls.Add(this.groupBoxSong);
            this.Controls.Add(this.buttonOK);
            this.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateNewTranslationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New translation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileNameAndLyricsInputWindow_FormClosing);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.CreateNewTranslationForm_Validating);
            this.groupBoxSong.ResumeLayout(false);
            this.groupBoxSong.PerformLayout();
            this.groupBoxLyrics.ResumeLayout(false);
            this.groupBoxLyrics.PerformLayout();
            this.groupBoxLanguage.ResumeLayout(false);
            this.groupBoxLanguage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Label labelLyricsRequestResult;
        private Button buttonNextLyrics;
        private Button buttonPreviousLyrics;
        private ToolTip toolTipAudioFileRequest;
        private Label label3;
        private Label label1;
        private GroupBox groupBoxSong;
        private GroupBox groupBoxLyrics;
        private GroupBox groupBoxLanguage;
        private ComboBox comboBox1;
        private Label label2;
        private ComboBox comboBox2;
        private ErrorProvider errorProvider1;
        private Button buttonRequestForURL;
        private GroupBox groupBox1;
        private CheckBox checkBoxStoreURL;
        private Label label4;
        private RadioButton radioButton5;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private LinkLabel linkLabel5;
        private LinkLabel linkLabel4;
        private LinkLabel linkLabel3;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Button button1;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
    }
}