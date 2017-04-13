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
            this.plblack = new System.Windows.Forms.RadioButton();
            this.plwhite = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // mem
            // 
            this.mem.AutoSize = true;
            this.mem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mem.Location = new System.Drawing.Point(16, 44);
            this.mem.Margin = new System.Windows.Forms.Padding(4);
            this.mem.Name = "mem";
            this.mem.Size = new System.Drawing.Size(204, 29);
            this.mem.TabIndex = 0;
            this.mem.TabStop = true;
            this.mem.Text = "Человек - человек";
            this.mem.UseVisualStyleBackColor = true;
            this.mem.CheckedChanged += new System.EventHandler(this.mem_CheckedChanged);
            // 
            // mec
            // 
            this.mec.AutoSize = true;
            this.mec.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mec.Location = new System.Drawing.Point(16, 73);
            this.mec.Margin = new System.Windows.Forms.Padding(4);
            this.mec.Name = "mec";
            this.mec.Size = new System.Drawing.Size(236, 29);
            this.mec.TabIndex = 1;
            this.mec.TabStop = true;
            this.mec.Text = "Человек - компьютер";
            this.mec.UseVisualStyleBackColor = true;
            this.mec.CheckedChanged += new System.EventHandler(this.mec_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(63, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Выберите тип игры";
            // 
            // starter
            // 
            this.starter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.starter.Location = new System.Drawing.Point(21, 191);
            this.starter.Margin = new System.Windows.Forms.Padding(4);
            this.starter.Name = "starter";
            this.starter.Size = new System.Drawing.Size(253, 42);
            this.starter.TabIndex = 3;
            this.starter.Text = "НОЙЧАТЬ ЙЭГРУ";
            this.starter.UseVisualStyleBackColor = true;
            this.starter.Click += new System.EventHandler(this.starter_Click);
            // 
            // plblack
            // 
            this.plblack.AutoSize = true;
            this.plblack.Enabled = false;
            this.plblack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.plblack.Location = new System.Drawing.Point(44, 134);
            this.plblack.Margin = new System.Windows.Forms.Padding(4);
            this.plblack.Name = "plblack";
            this.plblack.Size = new System.Drawing.Size(213, 29);
            this.plblack.TabIndex = 5;
            this.plblack.TabStop = true;
            this.plblack.Text = "йэградь за чёрныхг";
            this.plblack.UseVisualStyleBackColor = true;
            // 
            // plwhite
            // 
            this.plwhite.AutoSize = true;
            this.plwhite.Enabled = false;
            this.plwhite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.plwhite.Location = new System.Drawing.Point(44, 105);
            this.plwhite.Margin = new System.Windows.Forms.Padding(4);
            this.plwhite.Name = "plwhite";
            this.plwhite.Size = new System.Drawing.Size(203, 29);
            this.plwhite.TabIndex = 4;
            this.plwhite.TabStop = true;
            this.plwhite.Text = "йэградь за белыхг";
            this.plwhite.UseVisualStyleBackColor = true;
            // 
            // ChooseURMight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 246);
            this.Controls.Add(this.plblack);
            this.Controls.Add(this.plwhite);
            this.Controls.Add(this.starter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mec);
            this.Controls.Add(this.mem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.RadioButton plblack;
        private System.Windows.Forms.RadioButton plwhite;
    }
}