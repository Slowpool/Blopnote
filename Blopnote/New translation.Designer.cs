using System.Windows.Forms;
using System.Drawing;

namespace Blopnote
{
    partial class CreateNewTranslationForm
    {
        private TextBox TextBoxForAuthor;
        private TextBox TextBoxForLyrics;
        private CheckBox checkBoxUseLyrics;
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
            this.checkBoxUseLyrics = new System.Windows.Forms.CheckBox();
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
            this.buttonRequestForUrl = new System.Windows.Forms.Button();
            this.checkBoxUseUrl = new System.Windows.Forms.CheckBox();
            this.groupBoxUrl = new System.Windows.Forms.GroupBox();
            this.buttonCopy5 = new System.Windows.Forms.Button();
            this.buttonCopy4 = new System.Windows.Forms.Button();
            this.buttonCopy3 = new System.Windows.Forms.Button();
            this.buttonCopy2 = new System.Windows.Forms.Button();
            this.buttonCopy1 = new System.Windows.Forms.Button();
            this.labelUrlRequest = new System.Windows.Forms.Label();
            this.radioButtonUrl5 = new System.Windows.Forms.RadioButton();
            this.radioButtonUrl4 = new System.Windows.Forms.RadioButton();
            this.radioButtonUrl3 = new System.Windows.Forms.RadioButton();
            this.linkLabelUrl5 = new System.Windows.Forms.LinkLabel();
            this.linkLabelUrl4 = new System.Windows.Forms.LinkLabel();
            this.linkLabelUrl3 = new System.Windows.Forms.LinkLabel();
            this.linkLabelUrl2 = new System.Windows.Forms.LinkLabel();
            this.linkLabelUrl1 = new System.Windows.Forms.LinkLabel();
            this.radioButtonUrl2 = new System.Windows.Forms.RadioButton();
            this.radioButtonUrl1 = new System.Windows.Forms.RadioButton();
            this.groupBoxSong.SuspendLayout();
            this.groupBoxLyrics.SuspendLayout();
            this.groupBoxLanguage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBoxUrl.SuspendLayout();
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
            this.TextBoxForAuthor.Size = new System.Drawing.Size(584, 30);
            this.TextBoxForAuthor.TabIndex = 1;
            this.TextBoxForAuthor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxForAuthor_KeyPress);
            // 
            // TextBoxForLyrics
            // 
            this.TextBoxForLyrics.CausesValidation = false;
            this.TextBoxForLyrics.Location = new System.Drawing.Point(20, 68);
            this.TextBoxForLyrics.Multiline = true;
            this.TextBoxForLyrics.Name = "TextBoxForLyrics";
            this.TextBoxForLyrics.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxForLyrics.Size = new System.Drawing.Size(656, 140);
            this.TextBoxForLyrics.TabIndex = 5;
            this.TextBoxForLyrics.WordWrap = false;
            // 
            // checkBoxUseLyrics
            // 
            this.checkBoxUseLyrics.AutoSize = true;
            this.checkBoxUseLyrics.CausesValidation = false;
            this.checkBoxUseLyrics.Checked = true;
            this.checkBoxUseLyrics.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseLyrics.Location = new System.Drawing.Point(21, 36);
            this.checkBoxUseLyrics.Name = "checkBoxUseLyrics";
            this.checkBoxUseLyrics.Size = new System.Drawing.Size(129, 26);
            this.checkBoxUseLyrics.TabIndex = 3;
            this.checkBoxUseLyrics.Text = "Use lyrics";
            this.checkBoxUseLyrics.UseVisualStyleBackColor = true;
            this.checkBoxUseLyrics.CheckedChanged += new System.EventHandler(this.CheckBoxUseLyrics_CheckedChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.AutoSize = true;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(285, 665);
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
            this.TextBoxForSong.Size = new System.Drawing.Size(584, 30);
            this.TextBoxForSong.TabIndex = 2;
            this.TextBoxForSong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxForAuthor_KeyPress);
            // 
            // buttonLyricsRequest
            // 
            this.buttonLyricsRequest.AutoSize = true;
            this.buttonLyricsRequest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonLyricsRequest.CausesValidation = false;
            this.buttonLyricsRequest.Location = new System.Drawing.Point(156, 32);
            this.buttonLyricsRequest.Name = "buttonLyricsRequest";
            this.buttonLyricsRequest.Size = new System.Drawing.Size(200, 32);
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
            this.buttonNextLyrics.Location = new System.Drawing.Point(510, 214);
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
            this.buttonPreviousLyrics.Location = new System.Drawing.Point(20, 214);
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
            this.labelLyricsRequestResult.CausesValidation = false;
            this.labelLyricsRequestResult.Location = new System.Drawing.Point(362, 37);
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
            this.label3.CausesValidation = false;
            this.label3.Location = new System.Drawing.Point(26, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "Song:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.CausesValidation = false;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Author:";
            // 
            // groupBoxSong
            // 
            this.groupBoxSong.CausesValidation = false;
            this.groupBoxSong.Controls.Add(this.label1);
            this.groupBoxSong.Controls.Add(this.TextBoxForAuthor);
            this.groupBoxSong.Controls.Add(this.TextBoxForSong);
            this.groupBoxSong.Controls.Add(this.label3);
            this.groupBoxSong.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSong.Name = "groupBoxSong";
            this.groupBoxSong.Size = new System.Drawing.Size(720, 102);
            this.groupBoxSong.TabIndex = 16;
            this.groupBoxSong.TabStop = false;
            this.groupBoxSong.Text = "Song";
            this.groupBoxSong.Validating += new System.ComponentModel.CancelEventHandler(this.groupBoxSong_Validating);
            // 
            // groupBoxLyrics
            // 
            this.groupBoxLyrics.CausesValidation = false;
            this.groupBoxLyrics.Controls.Add(this.TextBoxForLyrics);
            this.groupBoxLyrics.Controls.Add(this.checkBoxUseLyrics);
            this.groupBoxLyrics.Controls.Add(this.buttonLyricsRequest);
            this.groupBoxLyrics.Controls.Add(this.labelLyricsRequestResult);
            this.groupBoxLyrics.Controls.Add(this.buttonPreviousLyrics);
            this.groupBoxLyrics.Controls.Add(this.buttonNextLyrics);
            this.groupBoxLyrics.Location = new System.Drawing.Point(12, 120);
            this.groupBoxLyrics.Name = "groupBoxLyrics";
            this.groupBoxLyrics.Size = new System.Drawing.Size(720, 260);
            this.groupBoxLyrics.TabIndex = 16;
            this.groupBoxLyrics.TabStop = false;
            this.groupBoxLyrics.Text = "Lyrics (Genius.com)";
            this.groupBoxLyrics.Validating += new System.ComponentModel.CancelEventHandler(this.groupBoxLyrics_Validating);
            // 
            // groupBoxLanguage
            // 
            this.groupBoxLanguage.Controls.Add(this.label2);
            this.groupBoxLanguage.Controls.Add(this.comboBox2);
            this.groupBoxLanguage.Controls.Add(this.comboBox1);
            this.groupBoxLanguage.Enabled = false;
            this.groupBoxLanguage.Location = new System.Drawing.Point(794, 390);
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
            // buttonRequestForUrl
            // 
            this.buttonRequestForUrl.AutoSize = true;
            this.buttonRequestForUrl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonRequestForUrl.CausesValidation = false;
            this.buttonRequestForUrl.Location = new System.Drawing.Point(126, 32);
            this.buttonRequestForUrl.Name = "buttonRequestForUrl";
            this.buttonRequestForUrl.Size = new System.Drawing.Size(170, 32);
            this.buttonRequestForUrl.TabIndex = 11;
            this.buttonRequestForUrl.Text = "Request for Url";
            this.buttonRequestForUrl.UseVisualStyleBackColor = true;
            this.buttonRequestForUrl.Click += new System.EventHandler(this.buttonRequestForUrlClick);
            // 
            // checkBoxUseUrl
            // 
            this.checkBoxUseUrl.AutoSize = true;
            this.checkBoxUseUrl.CausesValidation = false;
            this.checkBoxUseUrl.Checked = true;
            this.checkBoxUseUrl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseUrl.Location = new System.Drawing.Point(21, 36);
            this.checkBoxUseUrl.Name = "checkBoxUseUrl";
            this.checkBoxUseUrl.Size = new System.Drawing.Size(99, 26);
            this.checkBoxUseUrl.TabIndex = 12;
            this.checkBoxUseUrl.Text = "Use Url";
            this.checkBoxUseUrl.UseVisualStyleBackColor = true;
            this.checkBoxUseUrl.CheckedChanged += new System.EventHandler(this.checkBoxStoreUrlCheckedChanged);
            // 
            // groupBoxUrl
            // 
            this.groupBoxUrl.CausesValidation = false;
            this.groupBoxUrl.Controls.Add(this.buttonCopy5);
            this.groupBoxUrl.Controls.Add(this.buttonCopy4);
            this.groupBoxUrl.Controls.Add(this.buttonCopy3);
            this.groupBoxUrl.Controls.Add(this.buttonCopy2);
            this.groupBoxUrl.Controls.Add(this.buttonCopy1);
            this.groupBoxUrl.Controls.Add(this.labelUrlRequest);
            this.groupBoxUrl.Controls.Add(this.radioButtonUrl5);
            this.groupBoxUrl.Controls.Add(this.radioButtonUrl4);
            this.groupBoxUrl.Controls.Add(this.radioButtonUrl3);
            this.groupBoxUrl.Controls.Add(this.linkLabelUrl5);
            this.groupBoxUrl.Controls.Add(this.linkLabelUrl4);
            this.groupBoxUrl.Controls.Add(this.linkLabelUrl3);
            this.groupBoxUrl.Controls.Add(this.linkLabelUrl2);
            this.groupBoxUrl.Controls.Add(this.linkLabelUrl1);
            this.groupBoxUrl.Controls.Add(this.radioButtonUrl2);
            this.groupBoxUrl.Controls.Add(this.radioButtonUrl1);
            this.groupBoxUrl.Controls.Add(this.checkBoxUseUrl);
            this.groupBoxUrl.Controls.Add(this.buttonRequestForUrl);
            this.groupBoxUrl.Location = new System.Drawing.Point(12, 390);
            this.groupBoxUrl.Name = "groupBoxUrl";
            this.groupBoxUrl.Size = new System.Drawing.Size(720, 270);
            this.groupBoxUrl.TabIndex = 18;
            this.groupBoxUrl.TabStop = false;
            this.groupBoxUrl.Text = "Url to song (youtube.com)";
            this.groupBoxUrl.Validating += new System.ComponentModel.CancelEventHandler(this.groupBoxUrlValidating);
            // 
            // buttonCopy5
            // 
            this.buttonCopy5.AutoSize = true;
            this.buttonCopy5.CausesValidation = false;
            this.buttonCopy5.Location = new System.Drawing.Point(21, 220);
            this.buttonCopy5.Name = "buttonCopy5";
            this.buttonCopy5.Size = new System.Drawing.Size(40, 32);
            this.buttonCopy5.TabIndex = 29;
            this.buttonCopy5.Text = "✂";
            this.buttonCopy5.UseVisualStyleBackColor = true;
            this.buttonCopy5.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonCopy4
            // 
            this.buttonCopy4.AutoSize = true;
            this.buttonCopy4.CausesValidation = false;
            this.buttonCopy4.Location = new System.Drawing.Point(21, 182);
            this.buttonCopy4.Name = "buttonCopy4";
            this.buttonCopy4.Size = new System.Drawing.Size(40, 32);
            this.buttonCopy4.TabIndex = 28;
            this.buttonCopy4.Text = "✂";
            this.buttonCopy4.UseVisualStyleBackColor = true;
            this.buttonCopy4.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonCopy3
            // 
            this.buttonCopy3.AutoSize = true;
            this.buttonCopy3.CausesValidation = false;
            this.buttonCopy3.Location = new System.Drawing.Point(21, 144);
            this.buttonCopy3.Name = "buttonCopy3";
            this.buttonCopy3.Size = new System.Drawing.Size(40, 32);
            this.buttonCopy3.TabIndex = 27;
            this.buttonCopy3.Text = "✂";
            this.buttonCopy3.UseVisualStyleBackColor = true;
            this.buttonCopy3.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonCopy2
            // 
            this.buttonCopy2.AutoSize = true;
            this.buttonCopy2.CausesValidation = false;
            this.buttonCopy2.Location = new System.Drawing.Point(21, 106);
            this.buttonCopy2.Name = "buttonCopy2";
            this.buttonCopy2.Size = new System.Drawing.Size(40, 32);
            this.buttonCopy2.TabIndex = 26;
            this.buttonCopy2.Text = "✂";
            this.buttonCopy2.UseVisualStyleBackColor = true;
            this.buttonCopy2.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonCopy1
            // 
            this.buttonCopy1.AutoSize = true;
            this.buttonCopy1.CausesValidation = false;
            this.buttonCopy1.Location = new System.Drawing.Point(21, 68);
            this.buttonCopy1.Name = "buttonCopy1";
            this.buttonCopy1.Size = new System.Drawing.Size(40, 32);
            this.buttonCopy1.TabIndex = 25;
            this.buttonCopy1.Text = "✂";
            this.buttonCopy1.UseVisualStyleBackColor = true;
            this.buttonCopy1.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // labelUrlRequest
            // 
            this.labelUrlRequest.AutoSize = true;
            this.labelUrlRequest.CausesValidation = false;
            this.labelUrlRequest.Location = new System.Drawing.Point(302, 37);
            this.labelUrlRequest.Name = "labelUrlRequest";
            this.labelUrlRequest.Size = new System.Drawing.Size(80, 22);
            this.labelUrlRequest.TabIndex = 24;
            this.labelUrlRequest.Text = "results";
            // 
            // radioButtonUrl5
            // 
            this.radioButtonUrl5.AutoSize = true;
            this.radioButtonUrl5.CausesValidation = false;
            this.radioButtonUrl5.Location = new System.Drawing.Point(67, 228);
            this.radioButtonUrl5.Name = "radioButtonUrl5";
            this.radioButtonUrl5.Size = new System.Drawing.Size(14, 13);
            this.radioButtonUrl5.TabIndex = 23;
            this.radioButtonUrl5.UseVisualStyleBackColor = true;
            // 
            // radioButtonUrl4
            // 
            this.radioButtonUrl4.AutoSize = true;
            this.radioButtonUrl4.CausesValidation = false;
            this.radioButtonUrl4.Location = new System.Drawing.Point(67, 192);
            this.radioButtonUrl4.Name = "radioButtonUrl4";
            this.radioButtonUrl4.Size = new System.Drawing.Size(14, 13);
            this.radioButtonUrl4.TabIndex = 22;
            this.radioButtonUrl4.UseVisualStyleBackColor = true;
            // 
            // radioButtonUrl3
            // 
            this.radioButtonUrl3.AutoSize = true;
            this.radioButtonUrl3.CausesValidation = false;
            this.radioButtonUrl3.Location = new System.Drawing.Point(67, 154);
            this.radioButtonUrl3.Name = "radioButtonUrl3";
            this.radioButtonUrl3.Size = new System.Drawing.Size(14, 13);
            this.radioButtonUrl3.TabIndex = 21;
            this.radioButtonUrl3.UseVisualStyleBackColor = true;
            // 
            // linkLabelUrl5
            // 
            this.linkLabelUrl5.AutoSize = true;
            this.linkLabelUrl5.CausesValidation = false;
            this.linkLabelUrl5.Location = new System.Drawing.Point(87, 223);
            this.linkLabelUrl5.MaximumSize = new System.Drawing.Size(627, 22);
            this.linkLabelUrl5.Name = "linkLabelUrl5";
            this.linkLabelUrl5.Size = new System.Drawing.Size(110, 22);
            this.linkLabelUrl5.TabIndex = 20;
            this.linkLabelUrl5.TabStop = true;
            this.linkLabelUrl5.Text = "linkLabel5";
            this.linkLabelUrl5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelUrlLinkClicked);
            // 
            // linkLabelUrl4
            // 
            this.linkLabelUrl4.AutoSize = true;
            this.linkLabelUrl4.CausesValidation = false;
            this.linkLabelUrl4.Location = new System.Drawing.Point(87, 187);
            this.linkLabelUrl4.MaximumSize = new System.Drawing.Size(627, 22);
            this.linkLabelUrl4.Name = "linkLabelUrl4";
            this.linkLabelUrl4.Size = new System.Drawing.Size(110, 22);
            this.linkLabelUrl4.TabIndex = 19;
            this.linkLabelUrl4.TabStop = true;
            this.linkLabelUrl4.Text = "linkLabel4";
            this.linkLabelUrl4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelUrlLinkClicked);
            // 
            // linkLabelUrl3
            // 
            this.linkLabelUrl3.AutoSize = true;
            this.linkLabelUrl3.CausesValidation = false;
            this.linkLabelUrl3.Location = new System.Drawing.Point(87, 149);
            this.linkLabelUrl3.MaximumSize = new System.Drawing.Size(627, 22);
            this.linkLabelUrl3.Name = "linkLabelUrl3";
            this.linkLabelUrl3.Size = new System.Drawing.Size(110, 22);
            this.linkLabelUrl3.TabIndex = 18;
            this.linkLabelUrl3.TabStop = true;
            this.linkLabelUrl3.Text = "linkLabel3";
            this.linkLabelUrl3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelUrlLinkClicked);
            // 
            // linkLabelUrl2
            // 
            this.linkLabelUrl2.AutoSize = true;
            this.linkLabelUrl2.CausesValidation = false;
            this.linkLabelUrl2.Location = new System.Drawing.Point(87, 111);
            this.linkLabelUrl2.MaximumSize = new System.Drawing.Size(627, 22);
            this.linkLabelUrl2.Name = "linkLabelUrl2";
            this.linkLabelUrl2.Size = new System.Drawing.Size(110, 22);
            this.linkLabelUrl2.TabIndex = 17;
            this.linkLabelUrl2.TabStop = true;
            this.linkLabelUrl2.Text = "linkLabel2";
            this.linkLabelUrl2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelUrlLinkClicked);
            // 
            // linkLabelUrl1
            // 
            this.linkLabelUrl1.AutoSize = true;
            this.linkLabelUrl1.CausesValidation = false;
            this.linkLabelUrl1.Location = new System.Drawing.Point(87, 73);
            this.linkLabelUrl1.MaximumSize = new System.Drawing.Size(627, 22);
            this.linkLabelUrl1.Name = "linkLabelUrl1";
            this.linkLabelUrl1.Size = new System.Drawing.Size(110, 22);
            this.linkLabelUrl1.TabIndex = 16;
            this.linkLabelUrl1.TabStop = true;
            this.linkLabelUrl1.Text = "linkLabel1";
            this.linkLabelUrl1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelUrlLinkClicked);
            // 
            // radioButtonUrl2
            // 
            this.radioButtonUrl2.AutoSize = true;
            this.radioButtonUrl2.CausesValidation = false;
            this.radioButtonUrl2.Location = new System.Drawing.Point(67, 116);
            this.radioButtonUrl2.Name = "radioButtonUrl2";
            this.radioButtonUrl2.Size = new System.Drawing.Size(14, 13);
            this.radioButtonUrl2.TabIndex = 14;
            this.radioButtonUrl2.UseVisualStyleBackColor = true;
            // 
            // radioButtonUrl1
            // 
            this.radioButtonUrl1.AutoSize = true;
            this.radioButtonUrl1.CausesValidation = false;
            this.radioButtonUrl1.Checked = true;
            this.radioButtonUrl1.Location = new System.Drawing.Point(67, 78);
            this.radioButtonUrl1.Name = "radioButtonUrl1";
            this.radioButtonUrl1.Size = new System.Drawing.Size(14, 13);
            this.radioButtonUrl1.TabIndex = 13;
            this.radioButtonUrl1.TabStop = true;
            this.radioButtonUrl1.UseVisualStyleBackColor = true;
            // 
            // CreateNewTranslationForm
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(744, 709);
            this.Controls.Add(this.groupBoxUrl);
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
            this.groupBoxSong.ResumeLayout(false);
            this.groupBoxSong.PerformLayout();
            this.groupBoxLyrics.ResumeLayout(false);
            this.groupBoxLyrics.PerformLayout();
            this.groupBoxLanguage.ResumeLayout(false);
            this.groupBoxLanguage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBoxUrl.ResumeLayout(false);
            this.groupBoxUrl.PerformLayout();
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
        private Button buttonRequestForUrl;
        private GroupBox groupBoxUrl;
        private CheckBox checkBoxUseUrl;
        private Label labelUrlRequest;
        private RadioButton radioButtonUrl5;
        private RadioButton radioButtonUrl4;
        private RadioButton radioButtonUrl3;
        private LinkLabel linkLabelUrl5;
        private LinkLabel linkLabelUrl4;
        private LinkLabel linkLabelUrl3;
        private LinkLabel linkLabelUrl2;
        private LinkLabel linkLabelUrl1;
        private RadioButton radioButtonUrl2;
        private RadioButton radioButtonUrl1;
        private Button buttonCopy1;
        private Button buttonCopy5;
        private Button buttonCopy4;
        private Button buttonCopy3;
        private Button buttonCopy2;
    }
}