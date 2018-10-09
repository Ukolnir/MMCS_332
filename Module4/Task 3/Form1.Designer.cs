namespace Task_3
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
			this.label1 = new System.Windows.Forms.Label();
			this.radioAdd = new System.Windows.Forms.RadioButton();
			this.radioDelete = new System.Windows.Forms.RadioButton();
			this.radioMove = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Location = new System.Drawing.Point(227, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(568, 502);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			// 
			// radioAdd
			// 
			this.radioAdd.AutoSize = true;
			this.radioAdd.Location = new System.Drawing.Point(15, 45);
			this.radioAdd.Name = "radioAdd";
			this.radioAdd.Size = new System.Drawing.Size(119, 17);
			this.radioAdd.TabIndex = 2;
			this.radioAdd.TabStop = true;
			this.radioAdd.Text = "Добавление точки";
			this.radioAdd.UseVisualStyleBackColor = true;
			// 
			// radioDelete
			// 
			this.radioDelete.AutoSize = true;
			this.radioDelete.Location = new System.Drawing.Point(15, 68);
			this.radioDelete.Name = "radioDelete";
			this.radioDelete.Size = new System.Drawing.Size(106, 17);
			this.radioDelete.TabIndex = 3;
			this.radioDelete.TabStop = true;
			this.radioDelete.Text = "Удаление точки";
			this.radioDelete.UseVisualStyleBackColor = true;
			// 
			// radioMove
			// 
			this.radioMove.AutoSize = true;
			this.radioMove.Location = new System.Drawing.Point(15, 91);
			this.radioMove.Name = "radioMove";
			this.radioMove.Size = new System.Drawing.Size(129, 17);
			this.radioMove.TabIndex = 4;
			this.radioMove.TabStop = true;
			this.radioMove.Text = "Перемещение точки";
			this.radioMove.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(807, 526);
			this.Controls.Add(this.radioMove);
			this.Controls.Add(this.radioDelete);
			this.Controls.Add(this.radioAdd);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radioAdd;
		private System.Windows.Forms.RadioButton radioDelete;
		private System.Windows.Forms.RadioButton radioMove;
	}
}

