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
            this.buttonBuild1 = new System.Windows.Forms.Button();
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
            this.textBoxShiftX = new System.Windows.Forms.TextBox();
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
            this.buttonBuild2 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.labelDebug = new System.Windows.Forms.Label();
            this.labelDebug2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBoxBuildZ.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(367, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(689, 532);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // comboBoxBuildAxis
            // 
            this.comboBoxBuildAxis.FormattingEnabled = true;
            this.comboBoxBuildAxis.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.comboBoxBuildAxis.Location = new System.Drawing.Point(20, 49);
            this.comboBoxBuildAxis.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBuildAxis.Name = "comboBoxBuildAxis";
            this.comboBoxBuildAxis.Size = new System.Drawing.Size(132, 24);
            this.comboBoxBuildAxis.TabIndex = 10;
            this.comboBoxBuildAxis.Text = "Y";
            // 
            // buttonBuild1
            // 
            this.buttonBuild1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBuild1.Location = new System.Drawing.Point(147, 129);
            this.buttonBuild1.Margin = new System.Windows.Forms.Padding(4);
            this.buttonBuild1.Name = "buttonBuild1";
            this.buttonBuild1.Size = new System.Drawing.Size(85, 61);
            this.buttonBuild1.TabIndex = 11;
            this.buttonBuild1.Text = "Построить 1";
            this.buttonBuild1.UseVisualStyleBackColor = true;
            this.buttonBuild1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClear.Location = new System.Drawing.Point(212, 15);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(133, 36);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSave.Location = new System.Drawing.Point(8, 15);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(133, 36);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Ось вращения";
            // 
            // textBoxBuildCount
            // 
            this.textBoxBuildCount.Location = new System.Drawing.Point(20, 47);
            this.textBoxBuildCount.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxBuildCount.Name = "textBoxBuildCount";
            this.textBoxBuildCount.Size = new System.Drawing.Size(79, 22);
            this.textBoxBuildCount.TabIndex = 16;
            this.textBoxBuildCount.Text = "3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(140, 89);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 17);
            this.label7.TabIndex = 67;
            this.label7.Text = "Введите угол:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(123, 20);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(175, 17);
            this.label8.TabIndex = 66;
            this.label8.Text = "Введите координаты: ";
            // 
            // textBoxAngle
            // 
            this.textBoxAngle.Location = new System.Drawing.Point(299, 87);
            this.textBoxAngle.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxAngle.Name = "textBoxAngle";
            this.textBoxAngle.Size = new System.Drawing.Size(41, 22);
            this.textBoxAngle.TabIndex = 65;
            this.textBoxAngle.Text = "45";
            // 
            // textBoxZ2
            // 
            this.textBoxZ2.Location = new System.Drawing.Point(300, 58);
            this.textBoxZ2.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxZ2.Name = "textBoxZ2";
            this.textBoxZ2.Size = new System.Drawing.Size(41, 22);
            this.textBoxZ2.TabIndex = 64;
            this.textBoxZ2.Text = "10";
            // 
            // textBoxX1
            // 
            this.textBoxX1.Location = new System.Drawing.Point(183, 26);
            this.textBoxX1.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(44, 22);
            this.textBoxX1.TabIndex = 63;
            this.textBoxX1.Text = "0";
            // 
            // textBoxY1
            // 
            this.textBoxY1.Location = new System.Drawing.Point(241, 26);
            this.textBoxY1.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxY1.Name = "textBoxY1";
            this.textBoxY1.Size = new System.Drawing.Size(44, 22);
            this.textBoxY1.TabIndex = 62;
            this.textBoxY1.Text = "0";
            // 
            // textBoxY2
            // 
            this.textBoxY2.Location = new System.Drawing.Point(241, 58);
            this.textBoxY2.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxY2.Name = "textBoxY2";
            this.textBoxY2.Size = new System.Drawing.Size(45, 22);
            this.textBoxY2.TabIndex = 61;
            this.textBoxY2.Text = "10";
            // 
            // textBoxZ1
            // 
            this.textBoxZ1.Location = new System.Drawing.Point(300, 26);
            this.textBoxZ1.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxZ1.Name = "textBoxZ1";
            this.textBoxZ1.Size = new System.Drawing.Size(41, 22);
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
            this.comboBoxReflection.Location = new System.Drawing.Point(181, 47);
            this.comboBoxReflection.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxReflection.Name = "comboBoxReflection";
            this.comboBoxReflection.Size = new System.Drawing.Size(159, 24);
            this.comboBoxReflection.TabIndex = 59;
            this.comboBoxReflection.Text = "X";
            // 
            // textBoxX2
            // 
            this.textBoxX2.Location = new System.Drawing.Point(183, 58);
            this.textBoxX2.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.Size = new System.Drawing.Size(44, 22);
            this.textBoxX2.TabIndex = 58;
            this.textBoxX2.Text = "10";
            // 
            // textBoxShiftZ
            // 
            this.textBoxShiftZ.Location = new System.Drawing.Point(300, 54);
            this.textBoxShiftZ.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxShiftZ.Name = "textBoxShiftZ";
            this.textBoxShiftZ.Size = new System.Drawing.Size(41, 22);
            this.textBoxShiftZ.TabIndex = 57;
            this.textBoxShiftZ.Text = "10";
            // 
            // textBoxShiftY
            // 
            this.textBoxShiftY.Location = new System.Drawing.Point(241, 54);
            this.textBoxShiftY.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxShiftY.Name = "textBoxShiftY";
            this.textBoxShiftY.Size = new System.Drawing.Size(41, 22);
            this.textBoxShiftY.TabIndex = 56;
            this.textBoxShiftY.Text = "10";
            // 
            // textBoxShiftX
            // 
            this.textBoxShiftX.Location = new System.Drawing.Point(183, 54);
            this.textBoxShiftX.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxShiftX.Name = "textBoxShiftX";
            this.textBoxShiftX.Size = new System.Drawing.Size(41, 22);
            this.textBoxShiftX.TabIndex = 55;
            this.textBoxShiftX.Text = "10";
            // 
            // buttonRotate
            // 
            this.buttonRotate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRotate.Location = new System.Drawing.Point(8, 26);
            this.buttonRotate.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRotate.Name = "buttonRotate";
            this.buttonRotate.Size = new System.Drawing.Size(165, 46);
            this.buttonRotate.TabIndex = 54;
            this.buttonRotate.Text = "Поворот";
            this.buttonRotate.UseVisualStyleBackColor = true;
            this.buttonRotate.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonReflection
            // 
            this.buttonReflection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonReflection.Location = new System.Drawing.Point(8, 36);
            this.buttonReflection.Margin = new System.Windows.Forms.Padding(4);
            this.buttonReflection.Name = "buttonReflection";
            this.buttonReflection.Size = new System.Drawing.Size(165, 46);
            this.buttonReflection.TabIndex = 53;
            this.buttonReflection.Text = "Отражение";
            this.buttonReflection.UseVisualStyleBackColor = true;
            this.buttonReflection.Click += new System.EventHandler(this.buttonReflection_Click);
            // 
            // buttonShift
            // 
            this.buttonShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonShift.Location = new System.Drawing.Point(8, 43);
            this.buttonShift.Margin = new System.Windows.Forms.Padding(4);
            this.buttonShift.Name = "buttonShift";
            this.buttonShift.Size = new System.Drawing.Size(165, 46);
            this.buttonShift.TabIndex = 52;
            this.buttonShift.Text = "Смещение";
            this.buttonShift.UseVisualStyleBackColor = true;
            this.buttonShift.Click += new System.EventHandler(this.buttonShift_Click);
            // 
            // textBoxScale
            // 
            this.textBoxScale.Location = new System.Drawing.Point(249, 37);
            this.textBoxScale.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxScale.Name = "textBoxScale";
            this.textBoxScale.Size = new System.Drawing.Size(73, 22);
            this.textBoxScale.TabIndex = 51;
            this.textBoxScale.Text = "2";
            // 
            // buttonScale
            // 
            this.buttonScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonScale.Location = new System.Drawing.Point(8, 27);
            this.buttonScale.Margin = new System.Windows.Forms.Padding(4);
            this.buttonScale.Name = "buttonScale";
            this.buttonScale.Size = new System.Drawing.Size(224, 46);
            this.buttonScale.TabIndex = 50;
            this.buttonScale.Text = "Масштабирование";
            this.buttonScale.UseVisualStyleBackColor = true;
            this.buttonScale.Click += new System.EventHandler(this.buttonScale_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonScale);
            this.groupBox1.Controls.Add(this.textBoxScale);
            this.groupBox1.Location = new System.Drawing.Point(8, 260);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(343, 81);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Масштабирование";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonShift);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxShiftY);
            this.groupBox2.Controls.Add(this.textBoxShiftX);
            this.groupBox2.Controls.Add(this.textBoxShiftZ);
            this.groupBox2.Location = new System.Drawing.Point(3, 348);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(360, 107);
            this.groupBox2.TabIndex = 69;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Смещение";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonReflection);
            this.groupBox3.Controls.Add(this.comboBoxReflection);
            this.groupBox3.Location = new System.Drawing.Point(12, 458);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(351, 89);
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
            this.groupBox4.Location = new System.Drawing.Point(12, 554);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(347, 116);
            this.groupBox4.TabIndex = 71;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Поворот";
            // 
            // textBoxBuildZ
            // 
            this.textBoxBuildZ.Location = new System.Drawing.Point(9, 46);
            this.textBoxBuildZ.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxBuildZ.Name = "textBoxBuildZ";
            this.textBoxBuildZ.Size = new System.Drawing.Size(107, 22);
            this.textBoxBuildZ.TabIndex = 72;
            this.textBoxBuildZ.Text = "0";
            // 
            // groupBoxBuildZ
            // 
            this.groupBoxBuildZ.Controls.Add(this.textBoxBuildZ);
            this.groupBoxBuildZ.Location = new System.Drawing.Point(184, 36);
            this.groupBoxBuildZ.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxBuildZ.Name = "groupBoxBuildZ";
            this.groupBoxBuildZ.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxBuildZ.Size = new System.Drawing.Size(147, 90);
            this.groupBoxBuildZ.TabIndex = 73;
            this.groupBoxBuildZ.TabStop = false;
            this.groupBoxBuildZ.Text = "Значение координаты Z";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonBuild2);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Controls.Add(this.comboBoxBuildAxis);
            this.groupBox5.Controls.Add(this.groupBoxBuildZ);
            this.groupBox5.Controls.Add(this.buttonBuild1);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(8, 54);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(339, 198);
            this.groupBox5.TabIndex = 74;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Построение";
            // 
            // buttonBuild2
            // 
            this.buttonBuild2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBuild2.Location = new System.Drawing.Point(246, 129);
            this.buttonBuild2.Margin = new System.Windows.Forms.Padding(4);
            this.buttonBuild2.Name = "buttonBuild2";
            this.buttonBuild2.Size = new System.Drawing.Size(85, 61);
            this.buttonBuild2.TabIndex = 76;
            this.buttonBuild2.Text = "Построить 2";
            this.buttonBuild2.UseVisualStyleBackColor = true;
            this.buttonBuild2.Click += new System.EventHandler(this.buttonBuild2_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBoxBuildCount);
            this.groupBox6.Location = new System.Drawing.Point(20, 82);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(120, 82);
            this.groupBox6.TabIndex = 75;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Количество разбиений";
            // 
            // labelDebug
            // 
            this.labelDebug.AutoSize = true;
            this.labelDebug.Location = new System.Drawing.Point(16, 687);
            this.labelDebug.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDebug.Name = "labelDebug";
            this.labelDebug.Size = new System.Drawing.Size(80, 17);
            this.labelDebug.TabIndex = 75;
            this.labelDebug.Text = "labelDebug";
            // 
            // labelDebug2
            // 
            this.labelDebug2.AutoSize = true;
            this.labelDebug2.Location = new System.Drawing.Point(16, 714);
            this.labelDebug2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDebug2.Name = "labelDebug2";
            this.labelDebug2.Size = new System.Drawing.Size(46, 17);
            this.labelDebug2.TabIndex = 76;
            this.labelDebug2.Text = "label2";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(1077, 129);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(514, 384);
            this.pictureBox2.TabIndex = 77;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseClick);
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1604, 803);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.labelDebug2);
            this.Controls.Add(this.labelDebug);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBoxBuildAxis;
        private System.Windows.Forms.Button buttonBuild1;
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
        private System.Windows.Forms.TextBox textBoxShiftX;
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
        private System.Windows.Forms.Label labelDebug2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonBuild2;
    }
}

