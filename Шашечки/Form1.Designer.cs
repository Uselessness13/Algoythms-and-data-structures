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
            this.whiteQueen = new System.Windows.Forms.PictureBox();
            this.blackQueen = new System.Windows.Forms.PictureBox();
            this.highlight = new System.Windows.Forms.PictureBox();
            this.blacksh = new System.Windows.Forms.PictureBox();
            this.whitesh = new System.Windows.Forms.PictureBox();
            this.startNewGame = new System.Windows.Forms.Button();
            this.playersname = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.whiteQueen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackQueen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.highlight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blacksh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.whitesh)).BeginInit();
            this.SuspendLayout();
            // 
            // history
            // 
            this.history.Location = new System.Drawing.Point(429, 30);
            this.history.Name = "history";
            this.history.ReadOnly = true;
            this.history.Size = new System.Drawing.Size(150, 355);
            this.history.TabIndex = 0;
            this.history.Text = "";
            // 
            // whiteQueen
            // 
            this.whiteQueen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("whiteQueen.BackgroundImage")));
            this.whiteQueen.Image = global::Шашечки.Properties.Resources.belaya_damka;
            this.whiteQueen.Location = new System.Drawing.Point(373, 236);
            this.whiteQueen.Name = "whiteQueen";
            this.whiteQueen.Size = new System.Drawing.Size(50, 50);
            this.whiteQueen.TabIndex = 5;
            this.whiteQueen.TabStop = false;
            this.whiteQueen.Visible = false;
            // 
            // blackQueen
            // 
            this.blackQueen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("blackQueen.BackgroundImage")));
            this.blackQueen.Image = global::Шашечки.Properties.Resources.sinyaya_damka;
            this.blackQueen.Location = new System.Drawing.Point(373, 180);
            this.blackQueen.Name = "blackQueen";
            this.blackQueen.Size = new System.Drawing.Size(50, 50);
            this.blackQueen.TabIndex = 4;
            this.blackQueen.TabStop = false;
            this.blackQueen.Visible = false;
            // 
            // highlight
            // 
            this.highlight.Image = global::Шашечки.Properties.Resources.black_highlited;
            this.highlight.Location = new System.Drawing.Point(373, 12);
            this.highlight.Name = "highlight";
            this.highlight.Size = new System.Drawing.Size(50, 50);
            this.highlight.TabIndex = 3;
            this.highlight.TabStop = false;
            this.highlight.Visible = false;
            // 
            // blacksh
            // 
            this.blacksh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("blacksh.BackgroundImage")));
            this.blacksh.Image = global::Шашечки.Properties.Resources.black_checker;
            this.blacksh.Location = new System.Drawing.Point(373, 124);
            this.blacksh.Name = "blacksh";
            this.blacksh.Size = new System.Drawing.Size(50, 50);
            this.blacksh.TabIndex = 2;
            this.blacksh.TabStop = false;
            this.blacksh.Visible = false;
            // 
            // whitesh
            // 
            this.whitesh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("whitesh.BackgroundImage")));
            this.whitesh.Image = global::Шашечки.Properties.Resources.white_checker;
            this.whitesh.Location = new System.Drawing.Point(373, 68);
            this.whitesh.Name = "whitesh";
            this.whitesh.Size = new System.Drawing.Size(50, 50);
            this.whitesh.TabIndex = 1;
            this.whitesh.TabStop = false;
            this.whitesh.Visible = false;
            // 
            // startNewGame
            // 
            this.startNewGame.Location = new System.Drawing.Point(429, 391);
            this.startNewGame.Name = "startNewGame";
            this.startNewGame.Size = new System.Drawing.Size(150, 23);
            this.startNewGame.TabIndex = 6;
            this.startNewGame.Text = "New game";
            this.startNewGame.UseVisualStyleBackColor = true;
            this.startNewGame.Click += new System.EventHandler(this.startNewGame_Click);
            // 
            // playersname
            // 
            this.playersname.Location = new System.Drawing.Point(430, 4);
            this.playersname.Name = "playersname";
            this.playersname.ReadOnly = true;
            this.playersname.Size = new System.Drawing.Size(142, 20);
            this.playersname.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 421);
            this.Controls.Add(this.playersname);
            this.Controls.Add(this.startNewGame);
            this.Controls.Add(this.whiteQueen);
            this.Controls.Add(this.blackQueen);
            this.Controls.Add(this.highlight);
            this.Controls.Add(this.blacksh);
            this.Controls.Add(this.whitesh);
            this.Controls.Add(this.history);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "ШАШЕЧКИ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.whiteQueen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackQueen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.highlight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blacksh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.whitesh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox history;
        private System.Windows.Forms.PictureBox whitesh;
        private System.Windows.Forms.PictureBox blacksh;
        private System.Windows.Forms.PictureBox highlight;
        private System.Windows.Forms.PictureBox whiteQueen;
        private System.Windows.Forms.PictureBox blackQueen;
        private System.Windows.Forms.Button startNewGame;
        private System.Windows.Forms.TextBox playersname;
    }
}

