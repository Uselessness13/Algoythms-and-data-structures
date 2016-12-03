namespace Шашечки
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.history = new System.Windows.Forms.RichTextBox();
            this.whitesh = new System.Windows.Forms.PictureBox();
            this.blacksh = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.whitesh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blacksh)).BeginInit();
            this.SuspendLayout();
            // 
            // history
            // 
            this.history.Location = new System.Drawing.Point(429, 12);
            this.history.Name = "history";
            this.history.ReadOnly = true;
            this.history.Size = new System.Drawing.Size(150, 400);
            this.history.TabIndex = 0;
            this.history.Text = "";
            // 
            // whitesh
            // 
            this.whitesh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("whitesh.BackgroundImage")));
            this.whitesh.Image = global::Шашечки.Properties.Resources.white_checker;
            this.whitesh.Location = new System.Drawing.Point(300, 249);
            this.whitesh.Name = "whitesh";
            this.whitesh.Size = new System.Drawing.Size(50, 50);
            this.whitesh.TabIndex = 1;
            this.whitesh.TabStop = false;
            this.whitesh.Visible = false;
            // 
            // blacksh
            // 
            this.blacksh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("blacksh.BackgroundImage")));
            this.blacksh.Image = global::Шашечки.Properties.Resources.black_checker;
            this.blacksh.Location = new System.Drawing.Point(300, 305);
            this.blacksh.Name = "blacksh";
            this.blacksh.Size = new System.Drawing.Size(50, 50);
            this.blacksh.TabIndex = 2;
            this.blacksh.TabStop = false;
            this.blacksh.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 421);
            this.Controls.Add(this.blacksh);
            this.Controls.Add(this.whitesh);
            this.Controls.Add(this.history);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "ШАШЕЧКИ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.whitesh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blacksh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox history;
        private System.Windows.Forms.PictureBox whitesh;
        private System.Windows.Forms.PictureBox blacksh;
    }
}

