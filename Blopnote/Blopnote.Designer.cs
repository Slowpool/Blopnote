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
            this.TextBoxWithText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TextBoxWithText
            // 
            this.TextBoxWithText.Location = new System.Drawing.Point(71, 64);
            this.TextBoxWithText.Multiline = true;
            this.TextBoxWithText.Name = "TextBoxWithText";
            this.TextBoxWithText.Size = new System.Drawing.Size(577, 328);
            this.TextBoxWithText.TabIndex = 0;
            // 
            // Blopnote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TextBoxWithText);
            this.Name = "Blopnote";
            this.Text = "Form1";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxWithText;
    }
}

