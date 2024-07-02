namespace Blopnote
{
    partial class Blopnote
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TextBoxWithText = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lyricsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowLyrics = new System.Windows.Forms.ToolStripMenuItem();
            this.changeLyricsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabTranslatesOnly1LineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.followToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uselessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportDocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportXlsxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportXmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportJsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.PanelForLyricsBox = new System.Windows.Forms.Panel();
            this.VScrollBarForLyrics = new System.Windows.Forms.VScrollBar();
            this.timerAutoSave = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTipLyrics = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.PanelForLyricsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBoxWithText
            // 
            this.TextBoxWithText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxWithText.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxWithText.Location = new System.Drawing.Point(0, 27);
            this.TextBoxWithText.MaxLength = 1500000;
            this.TextBoxWithText.Name = "TextBoxWithText";
            this.TextBoxWithText.Size = new System.Drawing.Size(530, 454);
            this.TextBoxWithText.TabIndex = 0;
            this.TextBoxWithText.Text = "";
            this.TextBoxWithText.WordWrap = false;
            this.TextBoxWithText.SelectionChanged += new System.EventHandler(this.TextBoxWithText_SelectionChanged);
            this.TextBoxWithText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxWithText_KeyDown);
            this.TextBoxWithText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxWithText_KeyPress);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.lyricsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.UrlToolStripMenuItem,
            this.uselessToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(946, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.CreateToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Enabled = false;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // lyricsToolStripMenuItem
            // 
            this.lyricsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowLyrics,
            this.changeLyricsToolStripMenuItem});
            this.lyricsToolStripMenuItem.Name = "lyricsToolStripMenuItem";
            this.lyricsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.lyricsToolStripMenuItem.Text = "Lyrics";
            // 
            // ShowLyrics
            // 
            this.ShowLyrics.CheckOnClick = true;
            this.ShowLyrics.Enabled = false;
            this.ShowLyrics.Name = "ShowLyrics";
            this.ShowLyrics.Size = new System.Drawing.Size(115, 22);
            this.ShowLyrics.Text = "Show";
            this.ShowLyrics.ToolTipText = "Disabled, if there\'s no lyrics";
            this.ShowLyrics.Click += new System.EventHandler(this.ShowLyrics_Click);
            this.ShowLyrics.EnabledChanged += new System.EventHandler(this.ShowLyrics_EnabledChanged);
            // 
            // changeLyricsToolStripMenuItem
            // 
            this.changeLyricsToolStripMenuItem.Name = "changeLyricsToolStripMenuItem";
            this.changeLyricsToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.changeLyricsToolStripMenuItem.Text = "Change";
            this.changeLyricsToolStripMenuItem.Click += new System.EventHandler(this.changeLyricsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeFolderToolStripMenuItem,
            this.tabTranslatesOnly1LineToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // changeFolderToolStripMenuItem
            // 
            this.changeFolderToolStripMenuItem.Name = "changeFolderToolStripMenuItem";
            this.changeFolderToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.changeFolderToolStripMenuItem.Text = "Change folder";
            this.changeFolderToolStripMenuItem.ToolTipText = "Close file to unlock";
            // 
            // tabTranslatesOnly1LineToolStripMenuItem
            // 
            this.tabTranslatesOnly1LineToolStripMenuItem.Name = "tabTranslatesOnly1LineToolStripMenuItem";
            this.tabTranslatesOnly1LineToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.tabTranslatesOnly1LineToolStripMenuItem.Text = "Tab translates only 1 line";
            this.tabTranslatesOnly1LineToolStripMenuItem.ToolTipText = "Works only with shown lyrics";
            this.tabTranslatesOnly1LineToolStripMenuItem.Click += new System.EventHandler(this.tabTranslatesOnly1LineToolStripMenuItem_Click);
            // 
            // UrlToolStripMenuItem
            // 
            this.UrlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.followToolStripMenuItem,
            this.changeUrlToolStripMenuItem});
            this.UrlToolStripMenuItem.Name = "UrlToolStripMenuItem";
            this.UrlToolStripMenuItem.Size = new System.Drawing.Size(34, 20);
            this.UrlToolStripMenuItem.Text = "Url";
            // 
            // followToolStripMenuItem
            // 
            this.followToolStripMenuItem.Enabled = false;
            this.followToolStripMenuItem.Name = "followToolStripMenuItem";
            this.followToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.followToolStripMenuItem.Text = "Follow";
            this.followToolStripMenuItem.ToolTipText = "Disabled, if there\'s no Url";
            this.followToolStripMenuItem.Click += new System.EventHandler(this.followUrl_Click);
            // 
            // changeUrlToolStripMenuItem
            // 
            this.changeUrlToolStripMenuItem.Enabled = false;
            this.changeUrlToolStripMenuItem.Name = "changeUrlToolStripMenuItem";
            this.changeUrlToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.changeUrlToolStripMenuItem.Text = "Change";
            this.changeUrlToolStripMenuItem.Click += new System.EventHandler(this.changeUrl_Click);
            // 
            // uselessToolStripMenuItem
            // 
            this.uselessToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem});
            this.uselessToolStripMenuItem.Name = "uselessToolStripMenuItem";
            this.uselessToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.uselessToolStripMenuItem.Text = "Useless";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportDocToolStripMenuItem,
            this.ImportXlsxToolStripMenuItem,
            this.ImportXmlToolStripMenuItem,
            this.ImportJsonToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // ImportDocToolStripMenuItem
            // 
            this.ImportDocToolStripMenuItem.Name = "ImportDocToolStripMenuItem";
            this.ImportDocToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.ImportDocToolStripMenuItem.Text = "Word (.doc)";
            // 
            // ImportXlsxToolStripMenuItem
            // 
            this.ImportXlsxToolStripMenuItem.Name = "ImportXlsxToolStripMenuItem";
            this.ImportXlsxToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.ImportXlsxToolStripMenuItem.Text = "Excel (.xlsx)";
            this.ImportXlsxToolStripMenuItem.Click += new System.EventHandler(this.ImportDocToolStripMenuItem_Click);
            // 
            // ImportXmlToolStripMenuItem
            // 
            this.ImportXmlToolStripMenuItem.Name = "ImportXmlToolStripMenuItem";
            this.ImportXmlToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.ImportXmlToolStripMenuItem.Text = "XML (.xml)";
            // 
            // ImportJsonToolStripMenuItem
            // 
            this.ImportJsonToolStripMenuItem.Name = "ImportJsonToolStripMenuItem";
            this.ImportJsonToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.ImportJsonToolStripMenuItem.Text = "JSON (.json)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 484);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(946, 27);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(440, 22);
            this.status.Text = "if you can see it then somehting has broken";
            // 
            // PanelForLyricsBox
            // 
            this.PanelForLyricsBox.Controls.Add(this.VScrollBarForLyrics);
            this.PanelForLyricsBox.Location = new System.Drawing.Point(536, 27);
            this.PanelForLyricsBox.Name = "PanelForLyricsBox";
            this.PanelForLyricsBox.Size = new System.Drawing.Size(398, 454);
            this.PanelForLyricsBox.TabIndex = 4;
            // 
            // VScrollBarForLyrics
            // 
            this.VScrollBarForLyrics.Dock = System.Windows.Forms.DockStyle.Right;
            this.VScrollBarForLyrics.Location = new System.Drawing.Point(376, 0);
            this.VScrollBarForLyrics.Name = "VScrollBarForLyrics";
            this.VScrollBarForLyrics.Size = new System.Drawing.Size(22, 454);
            this.VScrollBarForLyrics.TabIndex = 5;
            // 
            // timerAutoSave
            // 
            this.timerAutoSave.Tick += new System.EventHandler(this.timerAutoSave_Tick);
            // 
            // toolTipLyrics
            // 
            this.toolTipLyrics.AutomaticDelay = 200;
            this.toolTipLyrics.AutoPopDelay = 32000;
            this.toolTipLyrics.InitialDelay = 200;
            this.toolTipLyrics.ReshowDelay = 40;
            this.toolTipLyrics.UseFading = false;
            // 
            // Blopnote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 511);
            this.Controls.Add(this.TextBoxWithText);
            this.Controls.Add(this.PanelForLyricsBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Blopnote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blopnote";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Blopnote_FormClosing);
            this.Load += new System.EventHandler(this.Blopnote_Load);
            this.Shown += new System.EventHandler(this.Blopnote_Shown);
            this.SizeChanged += new System.EventHandler(this.Blopnote_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.PanelForLyricsBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox TextBoxWithText;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lyricsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowLyrics;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Panel PanelForLyricsBox;
        private System.Windows.Forms.VScrollBar VScrollBarForLyrics;
        private System.Windows.Forms.Timer timerAutoSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolTip toolTipLyrics;
        private System.Windows.Forms.ToolStripMenuItem tabTranslatesOnly1LineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UrlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem followToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeUrlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeLyricsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uselessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportDocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportXlsxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportXmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportJsonToolStripMenuItem;
    }
}

