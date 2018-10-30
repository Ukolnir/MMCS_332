namespace Task_1
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.загрузитьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.очиститьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Location = new System.Drawing.Point(12, 40);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(1170, 757);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьФайлToolStripMenuItem,
            this.очиститьToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1225, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// загрузитьФайлToolStripMenuItem
			// 
			this.загрузитьФайлToolStripMenuItem.Name = "загрузитьФайлToolStripMenuItem";
			this.загрузитьФайлToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
			this.загрузитьФайлToolStripMenuItem.Text = "Загрузить файл";
			this.загрузитьФайлToolStripMenuItem.Click += new System.EventHandler(this.loadFileToolStripMenuItem_Click);
			// 
			// очиститьToolStripMenuItem
			// 
			this.очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
			this.очиститьToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
			this.очиститьToolStripMenuItem.Text = "Очистить";
			this.очиститьToolStripMenuItem.Click += new System.EventHandler(this.очиститьToolStripMenuItem_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 859);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Количество итераций:";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(697, 828);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(160, 44);
			this.button1.TabIndex = 4;
			this.button1.Text = "Применить";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(137, 857);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            18,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(46, 20);
			this.numericUpDown1.TabIndex = 6;
			this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(15, 817);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(552, 20);
			this.textBox1.TabIndex = 7;
			this.textBox1.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(357, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(0, 13);
			this.label2.TabIndex = 8;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1225, 907);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem загрузитьФайлToolStripMenuItem;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem очиститьToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
	}
}

