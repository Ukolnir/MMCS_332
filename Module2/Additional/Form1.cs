using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Additional
{
    public partial class Form1 : Form
    {
        Color c;
        OpenFileDialog open_dialog;

        public Form1()
        {
            InitializeComponent();

            colorDialog1.FullOpen = true;
            // установка начального цвета для colorDialog
            colorDialog1.Color = Color.Red;
            c = Color.Red;

            label2.BackColor = c;
        }

        void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            
            // установка цвета формы
            c = colorDialog1.Color;
            label2.BackColor = c;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;**.PNG)|*.BMP;*.JPG;**.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            DialogResult dr = open_dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Bitmap b = new Bitmap(open_dialog.FileName);
                pictureBox1.Image = new Bitmap(b, pictureBox1.Size);
            }
        }
    }
}
