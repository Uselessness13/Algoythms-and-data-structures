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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.history = new System.Windows.Forms.RichTextBox();
            this.whiteQueen = new System.Windows.Forms.PictureBox();
            this.blackQueen = new System.Windows.Forms.PictureBox();
            this.highlight = new System.Windows.Forms.PictureBox();
            this.blacksh = new System.Windows.Forms.PictureBox();
            this.whitesh = new System.Windows.Forms.PictureBox();
            this.startNewGame = new System.Windows.Forms.Button();
            this.playersname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.blackScore = new System.Windows.Forms.Label();
            this.whiteScore = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.whiteQueen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackQueen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.highlight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blacksh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.whitesh)).BeginInit();
            this.SuspendLayout();
            // 
            // history
            // 
            this.history.Location = new System.Drawing.Point(608, 111);
            this.history.Margin = new System.Windows.Forms.Padding(4);
            this.history.Name = "history";
            this.history.ReadOnly = true;
            this.history.Size = new System.Drawing.Size(199, 370);
            this.history.TabIndex = 0;
            this.history.Text = "";
            // 
            // whiteQueen
            // 
            this.whiteQueen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("whiteQueen.BackgroundImage")));
            this.whiteQueen.Image = global::Шашечки.Properties.Resources.belaya_damka;
            this.whiteQueen.Location = new System.Drawing.Point(13, 288);
            this.whiteQueen.Margin = new System.Windows.Forms.Padding(4);
            this.whiteQueen.Name = "whiteQueen";
            this.whiteQueen.Size = new System.Drawing.Size(67, 62);
            this.whiteQueen.TabIndex = 5;
            this.whiteQueen.TabStop = false;
            this.whiteQueen.Visible = false;
            // 
            // blackQueen
            // 
            this.blackQueen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("blackQueen.BackgroundImage")));
            this.blackQueen.Image = global::Шашечки.Properties.Resources.sinyaya_damka;
            this.blackQueen.Location = new System.Drawing.Point(13, 220);
            this.blackQueen.Margin = new System.Windows.Forms.Padding(4);
            this.blackQueen.Name = "blackQueen";
            this.blackQueen.Size = new System.Drawing.Size(67, 62);
            this.blackQueen.TabIndex = 4;
            this.blackQueen.TabStop = false;
            this.blackQueen.Visible = false;
            // 
            // highlight
            // 
            this.highlight.Image = global::Шашечки.Properties.Resources.black_highlited;
            this.highlight.Location = new System.Drawing.Point(13, 13);
            this.highlight.Margin = new System.Windows.Forms.Padding(4);
            this.highlight.Name = "highlight";
            this.highlight.Size = new System.Drawing.Size(67, 62);
            this.highlight.TabIndex = 3;
            this.highlight.TabStop = false;
            this.highlight.Visible = false;
            // 
            // blacksh
            // 
            this.blacksh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("blacksh.BackgroundImage")));
            this.blacksh.Image = global::Шашечки.Properties.Resources.black_checker;
            this.blacksh.Location = new System.Drawing.Point(13, 151);
            this.blacksh.Margin = new System.Windows.Forms.Padding(4);
            this.blacksh.Name = "blacksh";
            this.blacksh.Size = new System.Drawing.Size(67, 62);
            this.blacksh.TabIndex = 2;
            this.blacksh.TabStop = false;
            this.blacksh.Visible = false;
            // 
            // whitesh
            // 
            this.whitesh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("whitesh.BackgroundImage")));
            this.whitesh.Image = global::Шашечки.Properties.Resources.white_checker;
            this.whitesh.Location = new System.Drawing.Point(13, 82);
            this.whitesh.Margin = new System.Windows.Forms.Padding(4);
            this.whitesh.Name = "whitesh";
            this.whitesh.Size = new System.Drawing.Size(67, 62);
            this.whitesh.TabIndex = 1;
            this.whitesh.TabStop = false;
            this.whitesh.Visible = false;
            // 
            // startNewGame
            // 
            this.startNewGame.Location = new System.Drawing.Point(608, 489);
            this.startNewGame.Margin = new System.Windows.Forms.Padding(4);
            this.startNewGame.Name = "startNewGame";
            this.startNewGame.Size = new System.Drawing.Size(200, 28);
            this.startNewGame.TabIndex = 6;
            this.startNewGame.Text = "New game";
            this.startNewGame.UseVisualStyleBackColor = true;
            this.startNewGame.Click += new System.EventHandler(this.startNewGame_Click);
            // 
            // playersname
            // 
            this.playersname.Location = new System.Drawing.Point(609, 13);
            this.playersname.Margin = new System.Windows.Forms.Padding(4);
            this.playersname.Name = "playersname";
            this.playersname.ReadOnly = true;
            this.playersname.Size = new System.Drawing.Size(188, 22);
            this.playersname.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(608, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "White";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(728, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Black";
            // 
            // blackScore
            // 
            this.blackScore.AutoSize = true;
            this.blackScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.blackScore.Location = new System.Drawing.Point(764, 83);
            this.blackScore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.blackScore.Name = "blackScore";
            this.blackScore.Size = new System.Drawing.Size(0, 25);
            this.blackScore.TabIndex = 11;
            // 
            // whiteScore
            // 
            this.whiteScore.AutoSize = true;
            this.whiteScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.whiteScore.Location = new System.Drawing.Point(636, 83);
            this.whiteScore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.whiteScore.Name = "whiteScore";
            this.whiteScore.Size = new System.Drawing.Size(0, 25);
            this.whiteScore.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 520);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "A";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(94, 520);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "B";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 520);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "C";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(231, 520);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "D";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(298, 520);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "E";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(370, 520);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 17);
            this.label8.TabIndex = 17;
            this.label8.Text = "F";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(443, 520);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 17);
            this.label9.TabIndex = 18;
            this.label9.Text = "G";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(512, 520);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 17);
            this.label10.TabIndex = 19;
            this.label10.Text = "H";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(573, 464);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 17);
            this.label11.TabIndex = 20;
            this.label11.Text = "1";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(573, 409);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 17);
            this.label12.TabIndex = 21;
            this.label12.Text = "2";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(573, 355);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 17);
            this.label13.TabIndex = 22;
            this.label13.Text = "3";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(573, 288);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(16, 17);
            this.label14.TabIndex = 23;
            this.label14.Text = "4";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(573, 220);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(16, 17);
            this.label15.TabIndex = 24;
            this.label15.Text = "5";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(573, 162);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(16, 17);
            this.label16.TabIndex = 25;
            this.label16.Text = "6";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(573, 102);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(16, 17);
            this.label17.TabIndex = 26;
            this.label17.Text = "7";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(573, 41);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(16, 17);
            this.label18.TabIndex = 27;
            this.label18.Text = "8";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(732, 524);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 546);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.blackScore);
            this.Controls.Add(this.whiteScore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.playersname);
            this.Controls.Add(this.startNewGame);
            this.Controls.Add(this.whiteQueen);
            this.Controls.Add(this.blackQueen);
            this.Controls.Add(this.highlight);
            this.Controls.Add(this.blacksh);
            this.Controls.Add(this.whitesh);
            this.Controls.Add(this.history);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label blackScore;
        private System.Windows.Forms.Label whiteScore;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
    }
}

