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
            this.checkBoxUseURL = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCopy5 = new System.Windows.Forms.Button();
            this.buttonCopy4 = new System.Windows.Forms.Button();
            this.buttonCopy3 = new System.Windows.Forms.Button();
            this.buttonCopy2 = new System.Windows.Forms.Button();
            this.buttonCopy1 = new System.Windows.Forms.Button();
            this.labelURL_Request = new System.Windows.Forms.Label();
            this.radioButtonURL5 = new System.Windows.Forms.RadioButton();
            this.radioButtonURL4 = new System.Windows.Forms.RadioButton();
            this.radioButtonURL3 = new System.Windows.Forms.RadioButton();
            this.linkLabelURL5 = new System.Windows.Forms.LinkLabel();
            this.linkLabelURL4 = new System.Windows.Forms.LinkLabel();
            this.linkLabelURL3 = new System.Windows.Forms.LinkLabel();
            this.linkLabelURL2 = new System.Windows.Forms.LinkLabel();
            this.linkLabelURL1 = new System.Windows.Forms.LinkLabel();
            this.radioButtonURL2 = new System.Windows.Forms.RadioButton();
            this.radioButtonURL1 = new System.Windows.Forms.RadioButton();
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
            this.TextBoxForAuthor.Size = new System.Drawing.Size(584, 30);
            this.TextBoxForAuthor.TabIndex = 1;
            this.TextBoxForAuthor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxForAuthor_KeyPress);
            this.TextBoxForAuthor.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxAuthorAndSong_Validating);
            this.TextBoxForAuthor.Validated += new System.EventHandler(this.TextBoxAuthorAndSong_Validated);
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
            this.TextBoxForLyrics.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxForLyrics_Validating);
            this.TextBoxForLyrics.Validated += new System.EventHandler(this.TextBoxForLyrics_Validated);
            // 
            // CheckBoxUseLyrics
            // 
            this.CheckBoxUseLyrics.AutoSize = true;
            this.CheckBoxUseLyrics.CausesValidation = false;
            this.CheckBoxUseLyrics.Checked = true;
            this.CheckBoxUseLyrics.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.TextBoxForSong.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxAuthorAndSong_Validating);
            this.TextBoxForSong.Validated += new System.EventHandler(this.TextBoxAuthorAndSong_Validated);
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
            this.groupBoxSong.Size = new System.Drawing.Size(720, 102);
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
            this.groupBoxLyrics.Size = new System.Drawing.Size(720, 260);
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
            // buttonRequestForURL
            // 
            this.buttonRequestForURL.AutoSize = true;
            this.buttonRequestForURL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonRequestForURL.Location = new System.Drawing.Point(126, 32);
            this.buttonRequestForURL.Name = "buttonRequestForURL";
            this.buttonRequestForURL.Size = new System.Drawing.Size(170, 32);
            this.buttonRequestForURL.TabIndex = 11;
            this.buttonRequestForURL.Text = "Request for URL";
            this.buttonRequestForURL.UseVisualStyleBackColor = true;
            this.buttonRequestForURL.Click += new System.EventHandler(this.buttonRequestForURL_Click);
            // 
            // checkBoxUseURL
            // 
            this.checkBoxUseURL.AutoSize = true;
            this.checkBoxUseURL.Checked = true;
            this.checkBoxUseURL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseURL.Location = new System.Drawing.Point(21, 36);
            this.checkBoxUseURL.Name = "checkBoxUseURL";
            this.checkBoxUseURL.Size = new System.Drawing.Size(99, 26);
            this.checkBoxUseURL.TabIndex = 12;
            this.checkBoxUseURL.Text = "Use URL";
            this.checkBoxUseURL.UseVisualStyleBackColor = true;
            this.checkBoxUseURL.CheckedChanged += new System.EventHandler(this.checkBoxStoreURL_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonCopy5);
            this.groupBox1.Controls.Add(this.buttonCopy4);
            this.groupBox1.Controls.Add(this.buttonCopy3);
            this.groupBox1.Controls.Add(this.buttonCopy2);
            this.groupBox1.Controls.Add(this.buttonCopy1);
            this.groupBox1.Controls.Add(this.labelURL_Request);
            this.groupBox1.Controls.Add(this.radioButtonURL5);
            this.groupBox1.Controls.Add(this.radioButtonURL4);
            this.groupBox1.Controls.Add(this.radioButtonURL3);
            this.groupBox1.Controls.Add(this.linkLabelURL5);
            this.groupBox1.Controls.Add(this.linkLabelURL4);
            this.groupBox1.Controls.Add(this.linkLabelURL3);
            this.groupBox1.Controls.Add(this.linkLabelURL2);
            this.groupBox1.Controls.Add(this.linkLabelURL1);
            this.groupBox1.Controls.Add(this.radioButtonURL2);
            this.groupBox1.Controls.Add(this.radioButtonURL1);
            this.groupBox1.Controls.Add(this.checkBoxUseURL);
            this.groupBox1.Controls.Add(this.buttonRequestForURL);
            this.groupBox1.Location = new System.Drawing.Point(12, 390);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(720, 270);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "URL to song (youtube.com)";
            // 
            // buttonCopy5
            // 
            this.buttonCopy5.AutoSize = true;
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
            this.buttonCopy1.Location = new System.Drawing.Point(21, 68);
            this.buttonCopy1.Name = "buttonCopy1";
            this.buttonCopy1.Size = new System.Drawing.Size(40, 32);
            this.buttonCopy1.TabIndex = 25;
            this.buttonCopy1.Text = "✂";
            this.buttonCopy1.UseVisualStyleBackColor = true;
            this.buttonCopy1.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // labelURL_Request
            // 
            this.labelURL_Request.AutoSize = true;
            this.labelURL_Request.Location = new System.Drawing.Point(302, 37);
            this.labelURL_Request.Name = "labelURL_Request";
            this.labelURL_Request.Size = new System.Drawing.Size(80, 22);
            this.labelURL_Request.TabIndex = 24;
            this.labelURL_Request.Text = "results";
            // 
            // radioButtonURL5
            // 
            this.radioButtonURL5.AutoSize = true;
            this.radioButtonURL5.Location = new System.Drawing.Point(67, 228);
            this.radioButtonURL5.Name = "radioButtonURL5";
            this.radioButtonURL5.Size = new System.Drawing.Size(14, 13);
            this.radioButtonURL5.TabIndex = 23;
            this.radioButtonURL5.UseVisualStyleBackColor = true;
            // 
            // radioButtonURL4
            // 
            this.radioButtonURL4.AutoSize = true;
            this.radioButtonURL4.Location = new System.Drawing.Point(67, 192);
            this.radioButtonURL4.Name = "radioButtonURL4";
            this.radioButtonURL4.Size = new System.Drawing.Size(14, 13);
            this.radioButtonURL4.TabIndex = 22;
            this.radioButtonURL4.UseVisualStyleBackColor = true;
            // 
            // radioButtonURL3
            // 
            this.radioButtonURL3.AutoSize = true;
            this.radioButtonURL3.Location = new System.Drawing.Point(67, 154);
            this.radioButtonURL3.Name = "radioButtonURL3";
            this.radioButtonURL3.Size = new System.Drawing.Size(14, 13);
            this.radioButtonURL3.TabIndex = 21;
            this.radioButtonURL3.UseVisualStyleBackColor = true;
            // 
            // linkLabelURL5
            // 
            this.linkLabelURL5.AutoSize = true;
            this.linkLabelURL5.Location = new System.Drawing.Point(87, 223);
            this.linkLabelURL5.MaximumSize = new System.Drawing.Size(627, 22);
            this.linkLabelURL5.Name = "linkLabelURL5";
            this.linkLabelURL5.Size = new System.Drawing.Size(110, 22);
            this.linkLabelURL5.TabIndex = 20;
            this.linkLabelURL5.TabStop = true;
            this.linkLabelURL5.Text = "linkLabel5";
            this.linkLabelURL5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelURL_LinkClicked);
            // 
            // linkLabelURL4
            // 
            this.linkLabelURL4.AutoSize = true;
            this.linkLabelURL4.Location = new System.Drawing.Point(87, 187);
            this.linkLabelURL4.MaximumSize = new System.Drawing.Size(627, 22);
            this.linkLabelURL4.Name = "linkLabelURL4";
            this.linkLabelURL4.Size = new System.Drawing.Size(110, 22);
            this.linkLabelURL4.TabIndex = 19;
            this.linkLabelURL4.TabStop = true;
            this.linkLabelURL4.Text = "linkLabel4";
            this.linkLabelURL4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelURL_LinkClicked);
            // 
            // linkLabelURL3
            // 
            this.linkLabelURL3.AutoSize = true;
            this.linkLabelURL3.Location = new System.Drawing.Point(87, 149);
            this.linkLabelURL3.MaximumSize = new System.Drawing.Size(627, 22);
            this.linkLabelURL3.Name = "linkLabelURL3";
            this.linkLabelURL3.Size = new System.Drawing.Size(110, 22);
            this.linkLabelURL3.TabIndex = 18;
            this.linkLabelURL3.TabStop = true;
            this.linkLabelURL3.Text = "linkLabel3";
            this.linkLabelURL3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelURL_LinkClicked);
            // 
            // linkLabelURL2
            // 
            this.linkLabelURL2.AutoSize = true;
            this.linkLabelURL2.Location = new System.Drawing.Point(87, 111);
            this.linkLabelURL2.MaximumSize = new System.Drawing.Size(627, 22);
            this.linkLabelURL2.Name = "linkLabelURL2";
            this.linkLabelURL2.Size = new System.Drawing.Size(110, 22);
            this.linkLabelURL2.TabIndex = 17;
            this.linkLabelURL2.TabStop = true;
            this.linkLabelURL2.Text = "linkLabel2";
            this.linkLabelURL2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelURL_LinkClicked);
            // 
            // linkLabelURL1
            // 
            this.linkLabelURL1.AutoSize = true;
            this.linkLabelURL1.Location = new System.Drawing.Point(87, 73);
            this.linkLabelURL1.MaximumSize = new System.Drawing.Size(627, 22);
            this.linkLabelURL1.Name = "linkLabelURL1";
            this.linkLabelURL1.Size = new System.Drawing.Size(110, 22);
            this.linkLabelURL1.TabIndex = 16;
            this.linkLabelURL1.TabStop = true;
            this.linkLabelURL1.Text = "linkLabel1";
            this.linkLabelURL1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelURL_LinkClicked);
            // 
            // radioButtonURL2
            // 
            this.radioButtonURL2.AutoSize = true;
            this.radioButtonURL2.Location = new System.Drawing.Point(67, 116);
            this.radioButtonURL2.Name = "radioButtonURL2";
            this.radioButtonURL2.Size = new System.Drawing.Size(14, 13);
            this.radioButtonURL2.TabIndex = 14;
            this.radioButtonURL2.UseVisualStyleBackColor = true;
            // 
            // radioButtonURL1
            // 
            this.radioButtonURL1.AutoSize = true;
            this.radioButtonURL1.Checked = true;
            this.radioButtonURL1.Location = new System.Drawing.Point(67, 78);
            this.radioButtonURL1.Name = "radioButtonURL1";
            this.radioButtonURL1.Size = new System.Drawing.Size(14, 13);
            this.radioButtonURL1.TabIndex = 13;
            this.radioButtonURL1.TabStop = true;
            this.radioButtonURL1.UseVisualStyleBackColor = true;
            // 
            // CreateNewTranslationForm
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(744, 709);
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
        private CheckBox checkBoxUseURL;
        private Label labelURL_Request;
        private RadioButton radioButtonURL5;
        private RadioButton radioButtonURL4;
        private RadioButton radioButtonURL3;
        private LinkLabel linkLabelURL5;
        private LinkLabel linkLabelURL4;
        private LinkLabel linkLabelURL3;
        private LinkLabel linkLabelURL2;
        private LinkLabel linkLabelURL1;
        private RadioButton radioButtonURL2;
        private RadioButton radioButtonURL1;
        private Button buttonCopy1;
        private Button buttonCopy5;
        private Button buttonCopy4;
        private Button buttonCopy3;
        private Button buttonCopy2;
    }
}