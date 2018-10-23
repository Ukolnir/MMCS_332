namespace Task
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.textBox9 = new System.Windows.Forms.TextBox();
			this.textBox10 = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Location = new System.Drawing.Point(247, 31);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(806, 491);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(12, 59);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(137, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "Выбор элемента";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(913, 537);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(140, 59);
			this.button1.TabIndex = 2;
			this.button1.Text = "Очистить";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "...",
            "Гексаэдр",
            "Тетраэдр",
            "Октаэдр",
            "Точка",
            "Отрезок",
            "Полигон"});
			this.comboBox1.Location = new System.Drawing.Point(15, 97);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(140, 21);
			this.comboBox1.TabIndex = 3;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(15, 324);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(139, 46);
			this.button2.TabIndex = 4;
			this.button2.Text = "Нарисовать";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(247, 539);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(631, 20);
			this.textBox1.TabIndex = 5;
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(11, 264);
			this.trackBar1.Maximum = 350;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(163, 45);
			this.trackBar1.TabIndex = 6;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(16, 238);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(85, 20);
			this.textBox2.TabIndex = 7;
			this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(13, 219);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Координата z: ";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(247, 572);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(630, 20);
			this.textBox3.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(13, 143);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(131, 16);
			this.label4.TabIndex = 11;
			this.label4.Text = "Сдвиг проекции:";
			this.label4.Click += new System.EventHandler(this.label4_Click);
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(16, 162);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(85, 20);
			this.textBox4.TabIndex = 12;
			this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(15, 400);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(114, 23);
			this.button3.TabIndex = 13;
			this.button3.Text = "масштабирование";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(135, 403);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(28, 20);
			this.textBox5.TabIndex = 14;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(15, 439);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(114, 23);
			this.button4.TabIndex = 15;
			this.button4.Text = "смещение";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(16, 480);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(113, 23);
			this.button5.TabIndex = 16;
			this.button5.Text = "отражение";
			this.button5.UseVisualStyleBackColor = true;
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(16, 519);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(113, 23);
			this.button6.TabIndex = 17;
			this.button6.Text = "поворот";
			this.button6.UseVisualStyleBackColor = true;
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(135, 442);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(28, 20);
			this.textBox6.TabIndex = 18;
			// 
			// textBox7
			// 
			this.textBox7.Location = new System.Drawing.Point(169, 442);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(29, 20);
			this.textBox7.TabIndex = 19;
			// 
			// textBox8
			// 
			this.textBox8.Location = new System.Drawing.Point(204, 442);
			this.textBox8.Name = "textBox8";
			this.textBox8.Size = new System.Drawing.Size(28, 20);
			this.textBox8.TabIndex = 20;
			// 
			// textBox9
			// 
			this.textBox9.Location = new System.Drawing.Point(135, 522);
			this.textBox9.Name = "textBox9";
			this.textBox9.Size = new System.Drawing.Size(28, 20);
			this.textBox9.TabIndex = 21;
			// 
			// textBox10
			// 
			this.textBox10.Location = new System.Drawing.Point(135, 483);
			this.textBox10.Name = "textBox10";
			this.textBox10.Size = new System.Drawing.Size(28, 20);
			this.textBox10.TabIndex = 22;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1105, 608);
			this.Controls.Add(this.textBox10);
			this.Controls.Add(this.textBox9);
			this.Controls.Add(this.textBox8);
			this.Controls.Add(this.textBox7);
			this.Controls.Add(this.textBox6);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.trackBar1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.TextBox textBox8;
		private System.Windows.Forms.TextBox textBox9;
		private System.Windows.Forms.TextBox textBox10;
	}
}

