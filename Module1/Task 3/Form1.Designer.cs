namespace task3
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OpenFile = new System.Windows.Forms.MenuStrip();
            this.openPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.преобразованияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКартинкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.rGBHSLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // OpenFile
            // 
            this.OpenFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPicture,
            this.преобразованияToolStripMenuItem,
            this.сохранитьКартинкуToolStripMenuItem});
            this.OpenFile.Location = new System.Drawing.Point(0, 0);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(796, 24);
            this.OpenFile.TabIndex = 0;
            this.OpenFile.Text = "menuStrip1";
            // 
            // openPicture
            // 
            this.openPicture.Name = "openPicture";
            this.openPicture.Size = new System.Drawing.Size(98, 20);
            this.openPicture.Text = "Открыть файл";
            this.openPicture.Click += new System.EventHandler(this.openPicture_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(114, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(579, 262);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // преобразованияToolStripMenuItem
            // 
            this.преобразованияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rGBHSLToolStripMenuItem});
            this.преобразованияToolStripMenuItem.Name = "преобразованияToolStripMenuItem";
            this.преобразованияToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.преобразованияToolStripMenuItem.Text = "Преобразования";
            // 
            // сохранитьКартинкуToolStripMenuItem
            // 
            this.сохранитьКартинкуToolStripMenuItem.Name = "сохранитьКартинкуToolStripMenuItem";
            this.сохранитьКартинкуToolStripMenuItem.Size = new System.Drawing.Size(130, 20);
            this.сохранитьКартинкуToolStripMenuItem.Text = "Сохранить картинку";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(32, 415);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(170, 45);
            this.trackBar1.TabIndex = 1;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(287, 415);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(200, 45);
            this.trackBar2.TabIndex = 2;
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(557, 415);
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(212, 45);
            this.trackBar3.TabIndex = 3;
            // 
            // rGBHSLToolStripMenuItem
            // 
            this.rGBHSLToolStripMenuItem.Name = "rGBHSLToolStripMenuItem";
            this.rGBHSLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rGBHSLToolStripMenuItem.Text = "RGB -> HSL";
            this.rGBHSLToolStripMenuItem.Click += new System.EventHandler(this.rGBHSLToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 499);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.OpenFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.OpenFile;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.OpenFile.ResumeLayout(false);
            this.OpenFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip OpenFile;
        private System.Windows.Forms.ToolStripMenuItem openPicture;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem преобразованияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКартинкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rGBHSLToolStripMenuItem;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar3;
    }
}

