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
            this.button2 = new System.Windows.Forms.Button();
            this.listBoxMode = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(303, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(757, 617);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 158);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // radioAdd
            // 
            this.radioAdd.AutoSize = true;
            this.radioAdd.Location = new System.Drawing.Point(13, 193);
            this.radioAdd.Margin = new System.Windows.Forms.Padding(4);
            this.radioAdd.Name = "radioAdd";
            this.radioAdd.Size = new System.Drawing.Size(153, 21);
            this.radioAdd.TabIndex = 2;
            this.radioAdd.TabStop = true;
            this.radioAdd.Text = "Добавление точки";
            this.radioAdd.UseVisualStyleBackColor = true;
            // 
            // radioDelete
            // 
            this.radioDelete.AutoSize = true;
            this.radioDelete.Location = new System.Drawing.Point(13, 222);
            this.radioDelete.Margin = new System.Windows.Forms.Padding(4);
            this.radioDelete.Name = "radioDelete";
            this.radioDelete.Size = new System.Drawing.Size(136, 21);
            this.radioDelete.TabIndex = 3;
            this.radioDelete.TabStop = true;
            this.radioDelete.Text = "Удаление точки";
            this.radioDelete.UseVisualStyleBackColor = true;
            // 
            // radioMove
            // 
            this.radioMove.AutoSize = true;
            this.radioMove.Location = new System.Drawing.Point(13, 250);
            this.radioMove.Margin = new System.Windows.Forms.Padding(4);
            this.radioMove.Name = "radioMove";
            this.radioMove.Size = new System.Drawing.Size(165, 21);
            this.radioMove.TabIndex = 4;
            this.radioMove.TabStop = true;
            this.radioMove.Text = "Перемещение точки";
            this.radioMove.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 81);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(243, 52);
            this.button2.TabIndex = 6;
            this.button2.Text = "clear";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBoxMode
            // 
            this.listBoxMode.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.listBoxMode.DisplayMember = "BezeCurve";
            this.listBoxMode.ItemHeight = 16;
            this.listBoxMode.Items.AddRange(new object[] {
            "BezeCurve",
            "GrahamScan"});
            this.listBoxMode.Location = new System.Drawing.Point(16, 315);
            this.listBoxMode.Name = "listBoxMode";
            this.listBoxMode.Size = new System.Drawing.Size(120, 84);
            this.listBoxMode.TabIndex = 0;
            this.listBoxMode.Visible = false;
            this.listBoxMode.SelectedIndexChanged += new System.EventHandler(this.listBoxMode_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 647);
            this.Controls.Add(this.listBoxMode);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.radioMove);
            this.Controls.Add(this.radioDelete);
            this.Controls.Add(this.radioAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBoxMode;
    }
}

