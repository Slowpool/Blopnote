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
            this.TextBoxWithText = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lyricsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowLyrics = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.PanelForLyricsBox = new System.Windows.Forms.Panel();
            this.VScrollBarForLyrics = new System.Windows.Forms.VScrollBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.PanelForLyricsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBoxWithText
            // 
            this.TextBoxWithText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TextBoxWithText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.TextBoxWithText.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxWithText.Location = new System.Drawing.Point(0, 27);
            this.TextBoxWithText.MaxLength = 1500000;
            this.TextBoxWithText.Multiline = true;
            this.TextBoxWithText.Name = "TextBoxWithText";
            this.TextBoxWithText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxWithText.Size = new System.Drawing.Size(488, 303);
            this.TextBoxWithText.TabIndex = 0;
            this.TextBoxWithText.WordWrap = false;
            this.TextBoxWithText.TextChanged += new System.EventHandler(this.TextBoxWithText_TextChanged);
            this.TextBoxWithText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxWithText_KeyDown);
            this.TextBoxWithText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxWithText_KeyPress);
            this.TextBoxWithText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxWithText_KeyUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.lyricsToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(946, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.openToolStripMenuItem.Text = "Create";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.CreateToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.saveToolStripMenuItem.Text = "Open";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
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
            this.ShowLyrics});
            this.lyricsToolStripMenuItem.Name = "lyricsToolStripMenuItem";
            this.lyricsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.lyricsToolStripMenuItem.Text = "Lyrics";
            // 
            // ShowLyrics
            // 
            this.ShowLyrics.CheckOnClick = true;
            this.ShowLyrics.Enabled = false;
            this.ShowLyrics.Name = "ShowLyrics";
            this.ShowLyrics.Size = new System.Drawing.Size(103, 22);
            this.ShowLyrics.Text = "Show";
            this.ShowLyrics.Click += new System.EventHandler(this.ShowLyricsToolStripMenuItem_Click);
            this.ShowLyrics.EnabledChanged += new System.EventHandler(this.ShowLyrics_EnabledChanged);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeFolderToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // changeFolderToolStripMenuItem
            // 
            this.changeFolderToolStripMenuItem.Name = "changeFolderToolStripMenuItem";
            this.changeFolderToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.changeFolderToolStripMenuItem.Text = "Change folder";
            this.changeFolderToolStripMenuItem.Click += new System.EventHandler(this.changeFolderPathToolStripMenuItem_Click);
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
            this.PanelForLyricsBox.Size = new System.Drawing.Size(342, 225);
            this.PanelForLyricsBox.TabIndex = 4;
            // 
            // VScrollBarForLyrics
            // 
            this.VScrollBarForLyrics.Dock = System.Windows.Forms.DockStyle.Right;
            this.VScrollBarForLyrics.Location = new System.Drawing.Point(320, 0);
            this.VScrollBarForLyrics.Name = "VScrollBarForLyrics";
            this.VScrollBarForLyrics.Size = new System.Drawing.Size(22, 225);
            this.VScrollBarForLyrics.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Blopnote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 511);
            this.Controls.Add(this.PanelForLyricsBox);
            this.Controls.Add(this.TextBoxWithText);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Blopnote";
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

        private System.Windows.Forms.TextBox TextBoxWithText;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lyricsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowLyrics;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel PanelForLyricsBox;
        private System.Windows.Forms.VScrollBar VScrollBarForLyrics;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

