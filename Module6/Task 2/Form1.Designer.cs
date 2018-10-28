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
            this.comboBoxBuildAxis = new System.Windows.Forms.ComboBox();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxBuildCount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxAngle = new System.Windows.Forms.TextBox();
            this.textBoxZ2 = new System.Windows.Forms.TextBox();
            this.textBoxX1 = new System.Windows.Forms.TextBox();
            this.textBoxY1 = new System.Windows.Forms.TextBox();
            this.textBoxY2 = new System.Windows.Forms.TextBox();
            this.textBoxZ1 = new System.Windows.Forms.TextBox();
            this.comboBoxReflection = new System.Windows.Forms.ComboBox();
            this.textBoxX2 = new System.Windows.Forms.TextBox();
            this.textBoxShiftZ = new System.Windows.Forms.TextBox();
            this.textBoxShiftY = new System.Windows.Forms.TextBox();
            this.textBoxSfhiftX = new System.Windows.Forms.TextBox();
            this.buttonRotate = new System.Windows.Forms.Button();
            this.buttonReflection = new System.Windows.Forms.Button();
            this.buttonShift = new System.Windows.Forms.Button();
            this.textBoxScale = new System.Windows.Forms.TextBox();
            this.buttonScale = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxBuildZ = new System.Windows.Forms.TextBox();
            this.groupBoxBuildZ = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.labelDebug = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBoxBuildZ.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(275, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(663, 561);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // comboBoxBuildAxis
            // 
            this.comboBoxBuildAxis.FormattingEnabled = true;
            this.comboBoxBuildAxis.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.comboBoxBuildAxis.Location = new System.Drawing.Point(15, 40);
            this.comboBoxBuildAxis.Name = "comboBoxBuildAxis";
            this.comboBoxBuildAxis.Size = new System.Drawing.Size(100, 21);
            this.comboBoxBuildAxis.TabIndex = 10;
            // 
            // buttonBuild
            // 
            this.buttonBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBuild.Location = new System.Drawing.Point(123, 117);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(120, 29);
            this.buttonBuild.TabIndex = 11;
            this.buttonBuild.Text = "Построить";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClear.Location = new System.Drawing.Point(159, 12);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(100, 29);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSave.Location = new System.Drawing.Point(6, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 29);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "Ось вращения";
            // 
            // textBoxBuildCount
            // 
            this.textBoxBuildCount.Location = new System.Drawing.Point(15, 38);
            this.textBoxBuildCount.Name = "textBoxBuildCount";
            this.textBoxBuildCount.Size = new System.Drawing.Size(60, 20);
            this.textBoxBuildCount.TabIndex = 16;
            this.textBoxBuildCount.Text = "3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(105, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 16);
            this.label7.TabIndex = 67;
            this.label7.Text = "Введите угол:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(92, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(172, 16);
            this.label8.TabIndex = 66;
            this.label8.Text = "Введите координаты: ";
            // 
            // textBoxAngle
            // 
            this.textBoxAngle.Location = new System.Drawing.Point(224, 71);
            this.textBoxAngle.Name = "textBoxAngle";
            this.textBoxAngle.Size = new System.Drawing.Size(32, 20);
            this.textBoxAngle.TabIndex = 65;
            this.textBoxAngle.Text = "45";
            // 
            // textBoxZ2
            // 
            this.textBoxZ2.Location = new System.Drawing.Point(225, 47);
            this.textBoxZ2.Name = "textBoxZ2";
            this.textBoxZ2.Size = new System.Drawing.Size(32, 20);
            this.textBoxZ2.TabIndex = 64;
            this.textBoxZ2.Text = "10";
            // 
            // textBoxX1
            // 
            this.textBoxX1.Location = new System.Drawing.Point(137, 21);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(34, 20);
            this.textBoxX1.TabIndex = 63;
            this.textBoxX1.Text = "0";
            // 
            // textBoxY1
            // 
            this.textBoxY1.Location = new System.Drawing.Point(181, 21);
            this.textBoxY1.Name = "textBoxY1";
            this.textBoxY1.Size = new System.Drawing.Size(34, 20);
            this.textBoxY1.TabIndex = 62;
            this.textBoxY1.Text = "0";
            // 
            // textBoxY2
            // 
            this.textBoxY2.Location = new System.Drawing.Point(181, 47);
            this.textBoxY2.Name = "textBoxY2";
            this.textBoxY2.Size = new System.Drawing.Size(35, 20);
            this.textBoxY2.TabIndex = 61;
            this.textBoxY2.Text = "10";
            // 
            // textBoxZ1
            // 
            this.textBoxZ1.Location = new System.Drawing.Point(225, 21);
            this.textBoxZ1.Name = "textBoxZ1";
            this.textBoxZ1.Size = new System.Drawing.Size(32, 20);
            this.textBoxZ1.TabIndex = 60;
            this.textBoxZ1.Text = "0";
            // 
            // comboBoxReflection
            // 
            this.comboBoxReflection.FormattingEnabled = true;
            this.comboBoxReflection.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.comboBoxReflection.Location = new System.Drawing.Point(136, 38);
            this.comboBoxReflection.Name = "comboBoxReflection";
            this.comboBoxReflection.Size = new System.Drawing.Size(120, 21);
            this.comboBoxReflection.TabIndex = 59;
            // 
            // textBoxX2
            // 
            this.textBoxX2.Location = new System.Drawing.Point(137, 47);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.Size = new System.Drawing.Size(34, 20);
            this.textBoxX2.TabIndex = 58;
            this.textBoxX2.Text = "10";
            // 
            // textBoxShiftZ
            // 
            this.textBoxShiftZ.Location = new System.Drawing.Point(225, 44);
            this.textBoxShiftZ.Name = "textBoxShiftZ";
            this.textBoxShiftZ.Size = new System.Drawing.Size(32, 20);
            this.textBoxShiftZ.TabIndex = 57;
            // 
            // textBoxShiftY
            // 
            this.textBoxShiftY.Location = new System.Drawing.Point(181, 44);
            this.textBoxShiftY.Name = "textBoxShiftY";
            this.textBoxShiftY.Size = new System.Drawing.Size(32, 20);
            this.textBoxShiftY.TabIndex = 56;
            // 
            // textBoxSfhiftX
            // 
            this.textBoxSfhiftX.Location = new System.Drawing.Point(137, 44);
            this.textBoxSfhiftX.Name = "textBoxSfhiftX";
            this.textBoxSfhiftX.Size = new System.Drawing.Size(32, 20);
            this.textBoxSfhiftX.TabIndex = 55;
            // 
            // buttonRotate
            // 
            this.buttonRotate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRotate.Location = new System.Drawing.Point(6, 21);
            this.buttonRotate.Name = "buttonRotate";
            this.buttonRotate.Size = new System.Drawing.Size(124, 37);
            this.buttonRotate.TabIndex = 54;
            this.buttonRotate.Text = "Поворот";
            this.buttonRotate.UseVisualStyleBackColor = true;
            // 
            // buttonReflection
            // 
            this.buttonReflection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonReflection.Location = new System.Drawing.Point(6, 29);
            this.buttonReflection.Name = "buttonReflection";
            this.buttonReflection.Size = new System.Drawing.Size(124, 37);
            this.buttonReflection.TabIndex = 53;
            this.buttonReflection.Text = "Отражение";
            this.buttonReflection.UseVisualStyleBackColor = true;
            // 
            // buttonShift
            // 
            this.buttonShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonShift.Location = new System.Drawing.Point(6, 35);
            this.buttonShift.Name = "buttonShift";
            this.buttonShift.Size = new System.Drawing.Size(124, 37);
            this.buttonShift.TabIndex = 52;
            this.buttonShift.Text = "Смещение";
            this.buttonShift.UseVisualStyleBackColor = true;
            // 
            // textBoxScale
            // 
            this.textBoxScale.Location = new System.Drawing.Point(187, 30);
            this.textBoxScale.Name = "textBoxScale";
            this.textBoxScale.Size = new System.Drawing.Size(56, 20);
            this.textBoxScale.TabIndex = 51;
            // 
            // buttonScale
            // 
            this.buttonScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonScale.Location = new System.Drawing.Point(6, 22);
            this.buttonScale.Name = "buttonScale";
            this.buttonScale.Size = new System.Drawing.Size(168, 37);
            this.buttonScale.TabIndex = 50;
            this.buttonScale.Text = "Масштабирование";
            this.buttonScale.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonScale);
            this.groupBox1.Controls.Add(this.textBoxScale);
            this.groupBox1.Location = new System.Drawing.Point(6, 211);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 66);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Масштабирование";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonShift);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxShiftY);
            this.groupBox2.Controls.Add(this.textBoxSfhiftX);
            this.groupBox2.Controls.Add(this.textBoxShiftZ);
            this.groupBox2.Location = new System.Drawing.Point(2, 283);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 87);
            this.groupBox2.TabIndex = 69;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Смещение";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonReflection);
            this.groupBox3.Controls.Add(this.comboBoxReflection);
            this.groupBox3.Location = new System.Drawing.Point(9, 372);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(263, 72);
            this.groupBox3.TabIndex = 70;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Отражение";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonRotate);
            this.groupBox4.Controls.Add(this.textBoxY2);
            this.groupBox4.Controls.Add(this.textBoxX2);
            this.groupBox4.Controls.Add(this.textBoxZ1);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.textBoxY1);
            this.groupBox4.Controls.Add(this.textBoxAngle);
            this.groupBox4.Controls.Add(this.textBoxX1);
            this.groupBox4.Controls.Add(this.textBoxZ2);
            this.groupBox4.Location = new System.Drawing.Point(9, 450);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 94);
            this.groupBox4.TabIndex = 71;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Поворот";
            // 
            // textBoxBuildZ
            // 
            this.textBoxBuildZ.Location = new System.Drawing.Point(7, 37);
            this.textBoxBuildZ.Name = "textBoxBuildZ";
            this.textBoxBuildZ.Size = new System.Drawing.Size(81, 20);
            this.textBoxBuildZ.TabIndex = 72;
            this.textBoxBuildZ.Text = "0";
            // 
            // groupBoxBuildZ
            // 
            this.groupBoxBuildZ.Controls.Add(this.textBoxBuildZ);
            this.groupBoxBuildZ.Location = new System.Drawing.Point(138, 29);
            this.groupBoxBuildZ.Name = "groupBoxBuildZ";
            this.groupBoxBuildZ.Size = new System.Drawing.Size(110, 73);
            this.groupBoxBuildZ.TabIndex = 73;
            this.groupBoxBuildZ.TabStop = false;
            this.groupBoxBuildZ.Text = "Значение координаты Z";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Controls.Add(this.comboBoxBuildAxis);
            this.groupBox5.Controls.Add(this.groupBoxBuildZ);
            this.groupBox5.Controls.Add(this.buttonBuild);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(6, 44);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(254, 161);
            this.groupBox5.TabIndex = 74;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Построение";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBoxBuildCount);
            this.groupBox6.Location = new System.Drawing.Point(15, 67);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(90, 67);
            this.groupBox6.TabIndex = 75;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Количество разбиений";
            // 
            // labelDebug
            // 
            this.labelDebug.AutoSize = true;
            this.labelDebug.Location = new System.Drawing.Point(12, 558);
            this.labelDebug.Name = "labelDebug";
            this.labelDebug.Size = new System.Drawing.Size(71, 15);
            this.labelDebug.TabIndex = 75;
            this.labelDebug.Text = "labelDebug";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 585);
            this.Controls.Add(this.labelDebug);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBoxBuildZ.ResumeLayout(false);
            this.groupBoxBuildZ.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBoxBuildAxis;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxBuildCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxAngle;
        private System.Windows.Forms.TextBox textBoxZ2;
        private System.Windows.Forms.TextBox textBoxX1;
        private System.Windows.Forms.TextBox textBoxY1;
        private System.Windows.Forms.TextBox textBoxY2;
        private System.Windows.Forms.TextBox textBoxZ1;
        private System.Windows.Forms.ComboBox comboBoxReflection;
        private System.Windows.Forms.TextBox textBoxX2;
        private System.Windows.Forms.TextBox textBoxShiftZ;
        private System.Windows.Forms.TextBox textBoxShiftY;
        private System.Windows.Forms.TextBox textBoxSfhiftX;
        private System.Windows.Forms.Button buttonRotate;
        private System.Windows.Forms.Button buttonReflection;
        private System.Windows.Forms.Button buttonShift;
        private System.Windows.Forms.TextBox textBoxScale;
        private System.Windows.Forms.Button buttonScale;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxBuildZ;
        private System.Windows.Forms.GroupBox groupBoxBuildZ;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label labelDebug;
    }
}

