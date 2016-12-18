namespace Шашечки
{
    partial class ChooseURMight
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.mem = new System.Windows.Forms.RadioButton();
            this.mec = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.starter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mem
            // 
            this.mem.AutoSize = true;
            this.mem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mem.Location = new System.Drawing.Point(12, 36);
            this.mem.Name = "mem";
            this.mem.Size = new System.Drawing.Size(169, 24);
            this.mem.TabIndex = 0;
            this.mem.TabStop = true;
            this.mem.Text = "Человек - человек";
            this.mem.UseVisualStyleBackColor = true;
            // 
            // mec
            // 
            this.mec.AutoSize = true;
            this.mec.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mec.Location = new System.Drawing.Point(12, 59);
            this.mec.Name = "mec";
            this.mec.Size = new System.Drawing.Size(191, 24);
            this.mec.TabIndex = 1;
            this.mec.TabStop = true;
            this.mec.Text = "Человек - компьютер";
            this.mec.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(47, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Выберите тип игры";
            // 
            // starter
            // 
            this.starter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.starter.Location = new System.Drawing.Point(13, 90);
            this.starter.Name = "starter";
            this.starter.Size = new System.Drawing.Size(190, 34);
            this.starter.TabIndex = 3;
            this.starter.Text = "НОЧАТЬ ЕГРУ";
            this.starter.UseVisualStyleBackColor = true;
            this.starter.Click += new System.EventHandler(this.starter_Click);
            // 
            // ChooseURMight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 136);
            this.Controls.Add(this.starter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mec);
            this.Controls.Add(this.mem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ChooseURMight";
            this.Text = "Choose Ur Might";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton mem;
        private System.Windows.Forms.RadioButton mec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button starter;
    }
}