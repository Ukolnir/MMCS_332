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
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.textBoxX2 = new System.Windows.Forms.TextBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.textBoxZ1 = new System.Windows.Forms.TextBox();
			this.textBoxY2 = new System.Windows.Forms.TextBox();
			this.textBoxY1 = new System.Windows.Forms.TextBox();
			this.textBoxX1 = new System.Windows.Forms.TextBox();
			this.textBoxZ2 = new System.Windows.Forms.TextBox();
			this.textBoxAngle = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            "Тетраэдр"});
			this.comboBox1.Location = new System.Drawing.Point(15, 97);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(140, 21);
			this.comboBox1.TabIndex = 3;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(16, 152);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(216, 100);
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
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(247, 572);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(630, 20);
			this.textBox3.TabIndex = 10;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(15, 293);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(114, 40);
			this.button3.TabIndex = 13;
			this.button3.Text = "масштабирование";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(135, 313);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(28, 20);
			this.textBox5.TabIndex = 14;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(15, 358);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(114, 40);
			this.button4.TabIndex = 15;
			this.button4.Text = "смещение";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(15, 422);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(113, 42);
			this.button5.TabIndex = 16;
			this.button5.Text = "отражение";
			this.button5.UseVisualStyleBackColor = true;
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(16, 490);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(113, 42);
			this.button6.TabIndex = 17;
			this.button6.Text = "поворот";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(135, 378);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(28, 20);
			this.textBox6.TabIndex = 18;
			// 
			// textBox7
			// 
			this.textBox7.Location = new System.Drawing.Point(169, 378);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(29, 20);
			this.textBox7.TabIndex = 19;
			// 
			// textBox8
			// 
			this.textBox8.Location = new System.Drawing.Point(204, 378);
			this.textBox8.Name = "textBox8";
			this.textBox8.Size = new System.Drawing.Size(28, 20);
			this.textBox8.TabIndex = 20;
			// 
			// textBoxX2
			// 
			this.textBoxX2.Location = new System.Drawing.Point(135, 512);
			this.textBoxX2.Name = "textBoxX2";
			this.textBoxX2.Size = new System.Drawing.Size(28, 20);
			this.textBoxX2.TabIndex = 21;
			this.textBoxX2.Text = "x2";
			// 
			// comboBox2
			// 
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
			this.comboBox2.Location = new System.Drawing.Point(134, 443);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(98, 21);
			this.comboBox2.TabIndex = 22;
			// 
			// textBoxZ1
			// 
			this.textBoxZ1.Location = new System.Drawing.Point(203, 486);
			this.textBoxZ1.Name = "textBoxZ1";
			this.textBoxZ1.Size = new System.Drawing.Size(28, 20);
			this.textBoxZ1.TabIndex = 23;
			this.textBoxZ1.Text = "z1";
			// 
			// textBoxY2
			// 
			this.textBoxY2.Location = new System.Drawing.Point(169, 512);
			this.textBoxY2.Name = "textBoxY2";
			this.textBoxY2.Size = new System.Drawing.Size(28, 20);
			this.textBoxY2.TabIndex = 24;
			this.textBoxY2.Text = "y2";
			// 
			// textBoxY1
			// 
			this.textBoxY1.Location = new System.Drawing.Point(169, 486);
			this.textBoxY1.Name = "textBoxY1";
			this.textBoxY1.Size = new System.Drawing.Size(28, 20);
			this.textBoxY1.TabIndex = 25;
			this.textBoxY1.Text = "y1";
			// 
			// textBoxX1
			// 
			this.textBoxX1.Location = new System.Drawing.Point(135, 486);
			this.textBoxX1.Name = "textBoxX1";
			this.textBoxX1.Size = new System.Drawing.Size(28, 20);
			this.textBoxX1.TabIndex = 26;
			this.textBoxX1.Text = "x1";
			// 
			// textBoxZ2
			// 
			this.textBoxZ2.Location = new System.Drawing.Point(204, 512);
			this.textBoxZ2.Name = "textBoxZ2";
			this.textBoxZ2.Size = new System.Drawing.Size(28, 20);
			this.textBoxZ2.TabIndex = 27;
			this.textBoxZ2.Text = "z2";
			// 
			// textBoxAngle
			// 
			this.textBoxAngle.Location = new System.Drawing.Point(169, 557);
			this.textBoxAngle.Name = "textBoxAngle";
			this.textBoxAngle.Size = new System.Drawing.Size(28, 20);
			this.textBoxAngle.TabIndex = 28;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(135, 358);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 12);
			this.label2.TabIndex = 29;
			this.label2.Text = "Введите координаты: ";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(149, 541);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 13);
			this.label3.TabIndex = 30;
			this.label3.Text = "Введите угол:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1105, 608);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxAngle);
			this.Controls.Add(this.textBoxZ2);
			this.Controls.Add(this.textBoxX1);
			this.Controls.Add(this.textBoxY1);
			this.Controls.Add(this.textBoxY2);
			this.Controls.Add(this.textBoxZ1);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.textBoxX2);
			this.Controls.Add(this.textBox8);
			this.Controls.Add(this.textBox7);
			this.Controls.Add(this.textBox6);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.TextBox textBox8;
		private System.Windows.Forms.TextBox textBoxX2;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.TextBox textBoxZ1;
		private System.Windows.Forms.TextBox textBoxY2;
		private System.Windows.Forms.TextBox textBoxY1;
		private System.Windows.Forms.TextBox textBoxX1;
		private System.Windows.Forms.TextBox textBoxZ2;
		private System.Windows.Forms.TextBox textBoxAngle;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
	}
}

