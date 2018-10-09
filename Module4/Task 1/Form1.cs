using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_1
{
	public partial class Form1 : Form
	{
		Bitmap bmp;
		Graphics g;

		List<string> param;
		int cnt;


		public Form1()
		{
			InitializeComponent();
			pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			bmp = (Bitmap)pictureBox1.Image;
			Clear();
			//g = Graphics.FromImage(bmp);
			pictureBox1.Image = bmp;
		}

		private void Clear()
		{
			g = Graphics.FromImage(pictureBox1.Image);
			g.Clear(pictureBox1.BackColor);
			pictureBox1.Image = pictureBox1.Image;
		}

		private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Text Files(*.txt)|*.txt|All files (*.*)|*.*"; //формат загружаемого файла
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				param = new List<string>();
				using (System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog.FileName, Encoding.Default))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						param.Add(line);
					}
				}
			}
		}

		public List<char> parse_start(string s) {
			List<char> res = new List<char>();
			for (int i = 0; i < s.Length; ++i)
				res.Add(s[i]);
			return res;
		}

		private void button1_Click(object sender, EventArgs e)
		{

			cnt = Convert.ToInt32(textBox1.Text);
			List<char> starts = parse_start(param[1]);




		}
	}
}
