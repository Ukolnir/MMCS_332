using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_3
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			g = Graphics.FromImage(pictureBox1.Image);
			bmp = (Bitmap)pictureBox1.Image;
		}

		private static List<Point> points = new List<Point>();
		private static Graphics g;
		private static Bitmap bmp;
        private bool haveFictivePoint = false;
        private Point fictivePoint = new Point();
        private bool nowMoving = false;
        private int indOfMovingPoint = -1;

        private int diff_for_accuracy = 7;

        private System.IO.StreamWriter writer = new System.IO.StreamWriter("indices.txt");

        private void clear()
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            bmp = (Bitmap)pictureBox1.Image;

            points.Clear();
        }

        private void clearWithoutPoints()
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            bmp = (Bitmap)pictureBox1.Image;

            foreach (var p in points)
            {
                drawPoint(p.X, p.Y, Color.Black);
            }
        }

        private void drawPoint(int x, int y, Color c)
		{
			bmp.SetPixel(x, y, c);

			bmp.SetPixel(x + 1, y, c);
			bmp.SetPixel(x - 1, y, c);
			bmp.SetPixel(x, y + 1, c);
			bmp.SetPixel(x, y - 1, c);

			bmp.SetPixel(x + 1, y + 1, c);
			bmp.SetPixel(x - 1, y + 1, c);
			bmp.SetPixel(x + 1, y - 1, c);
			bmp.SetPixel(x - 1, y - 1, c);

            pictureBox1.Image = bmp;
        }

		private bool addPoint(int x, int y, Color c, int index = -1)
		{
			if (x > 0 && x < pictureBox1.Width && y > 0 && y < pictureBox1.Height)
			{
                Point p = new Point(x, y);
                if (index == -1)
                    points.Add(p);
                else
                    points.Insert(index, p);

				drawPoint(x, y, c);

				label1.Text = "Point added";

				return true;
			}
			else
			{
				label1.Text = "Point abroad borders";

				return false;
			}
		}

		private bool deletePoint(int x, int y)
		{
			if (x > 0 && x < pictureBox1.Width && y > 0 && y < pictureBox1.Height)
			{
                int indToDel = 0;
				int diff = diff_for_accuracy;
				if (points.Exists(point => ((point.X > x - diff) && (point.X < x + diff)) &&
					((point.Y > y - diff) && (point.Y < y + diff))))
				{
					indToDel = points.FindIndex(point => ((point.X > x - diff) && (point.X < x + diff)) &&
						((point.Y > y - diff) && (point.Y < y + diff)));

                    drawPoint(points[indToDel].X, points[indToDel].Y, pictureBox1.BackColor);

                    points.RemoveAt(indToDel);

					label1.Text = "Point deleted";

					return true;
				}
				else
				{
					label1.Text = "Point not found";

					return false;
				}
			}
			else
			{
				label1.Text = "Point abroad borders";

				return false;
			}
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			int x = e.Location.X;
			int y = e.Location.Y;
			if (radioAdd.Checked)
			{
				addPoint(x, y, Color.Black);
            }
			if (radioDelete.Checked)
			{
				deletePoint(x, y);

                if (haveFictivePoint)
                {
                    deletePoint(fictivePoint.X, fictivePoint.Y);
                    haveFictivePoint = false;
                }
			}
            clearWithoutPoints();
            drawObject();
        }

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			int x = e.Location.X;
			int y = e.Location.Y;

			if (radioMove.Checked)
			{
                nowMoving = true;
                //deletePoint(x, y);
                int diff = diff_for_accuracy;
                
                if (nowMoving && indOfMovingPoint == -1)
                {
                    indOfMovingPoint = points.FindIndex(point => ((point.X > x - diff) && (point.X < x + diff)) &&
                        ((point.Y > y - diff) && (point.Y < y + diff)));
                    writer.WriteLine(indOfMovingPoint);
                    writer.Flush();
                }

                /*
                if (haveFictivePoint)
                {
                    deletePoint(fictivePoint.X, fictivePoint.Y);
                    haveFictivePoint = false;
                }
                */
                clearWithoutPoints();
                drawObject();
            }
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			int x = e.Location.X;
			int y = e.Location.Y;

			if (radioMove.Checked)
			{
                int ind = System.Math.Min(indOfMovingPoint, points.Count());
                Point p = points[indOfMovingPoint];
                deletePoint(p.X, p.Y);
				if (addPoint(x, y, Color.Black, ind))
				{
					label1.Text = "Point moved";
                    clearWithoutPoints();
                    drawObject();
                }
                nowMoving = false;
                indOfMovingPoint = -1;
			}
		}

        private PointF q(PointF p0, PointF p1, float t)
        {
            return new PointF(p0.X * (1 - t) + p1.X * t, p0.Y * (1 - t) + p1.Y * t);
        }

        private PointF r(PointF p0, PointF p1, PointF p2, float t)
        {
            return new PointF(q(p0, p1, t).X * (1 - t) + q(p1, p2, t).X * t,
                q(p0, p1, t).Y * (1 - t) + q(p1, p2, t).Y * t);
        }

        private PointF b(PointF p0, PointF p1, PointF p2, PointF p3, float t)
        {
            return new PointF(r(p0, p1, p2, t).X * (1 - t) + r(p1, p2, p3, t).X * t, 
                r(p0, p1, p2, t).Y * (1 - t) + r(p1, p2, p3, t).Y * t);
        }

        private void drawLine(PointF p1, PointF p2, Color c)
        {
            g.DrawLine(new Pen(c), new Point((int)p1.X, (int)p1.Y), new Point((int)p2.X, (int)p2.Y));
            pictureBox1.Image = pictureBox1.Image;
        }

        private Point pointBetweenPoints(Point p1, Point p2)
        {
            return new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        }

        private void addPointsForDrawingCurve()
        {
            
            int s = points.Count();

            if (s % 2 != 0 && s > 4)
            {
                if (!haveFictivePoint)
                {
                    Point p1 = points[s - 2];
                    Point p2 = points[s - 1];
                    Point pb = pointBetweenPoints(p1, p2);

                    points[s - 1] = pb;
                    drawPoint(pb.X, pb.Y, Color.Green);
                    points.Add(p2);

                    haveFictivePoint = true;
                    fictivePoint = pb;
                }
                else
                {
                    deletePoint(fictivePoint.X, fictivePoint.Y);
                    haveFictivePoint = false;
                }
            }
        }

        private void drawCurveBy4Points(Point p0, Point p1, Point p2, Point p3)
        {
            PointF prevP = b(p0, p1, p2, p3, (float)0);
            for (int i = 1; i <= 100; ++i)
            {
                PointF p = b(p0, p1, p2, p3, (float)i / 100);

                drawLine(prevP, p, Color.Red);
                prevP = p;
            }
        }

        private void drawCurve()
        {
            addPointsForDrawingCurve();
            if (points.Count() == 4)
            {
                Point p0 = points[0];
                Point p1 = points[1];
                Point p2 = points[2];
                Point p3 = points[3];

                drawCurveBy4Points(p0, p1, p2, p3);
            }
            if (points.Count() > 4)
            {
                //addPointsForDrawingCurve();
                int sz = points.Count();

                if (sz % 2 == 0)
                {
                    Point p0 = points[0];
                    Point p1 = points[1];
                    Point p2 = points[2];
                    Point p3 = pointBetweenPoints(points[2], points[3]);

                    drawCurveBy4Points(p0, p1, p2, p3);

                    for (int i = 3; i < sz - 4; i += 2)
                    {
                        p0 = pointBetweenPoints(points[i - 1], points[i]);
                        p1 = points[i];
                        p2 = points[i + 1];
                        p3 = pointBetweenPoints(points[i + 1], points[i + 2]);

                        drawCurveBy4Points(p0, p1, p2, p3);
                    }

                    p3 = points[sz - 1];
                    p2 = points[sz - 2];
                    p1 = points[sz - 3];
                    p0 = pointBetweenPoints(points[sz - 3], points[sz - 4]);
                    drawCurveBy4Points(p0, p1, p2, p3);
                }
            }
        }

        /*private List<Point> sortByPolar(ref List<Point> pointsWithoutMin)
        {

        }*/

        private void drawConvex()
        {
            if (points.Count() > 2)
            {
                Point pMin = points[0];
                int indMin = 0;
                for (int i = 1; i < points.Count(); ++i)
                {
                    if ((points[i].Y > pMin.Y) || (points[i].Y == pMin.Y && points[i].X < pMin.X))
                    {
                        pMin = points[i];
                        indMin = i;
                    }
                }

                List<Point> pointsWithoutMin = points.ToList();
                pointsWithoutMin.RemoveAt(indMin);

                //List<Point> sorted = sortByPolar(ref pointsWithoutMin);
            }
        }

        private void drawObject()
        {
            //if (listBoxMode.SelectedItem.ToString() == "BezeCurve")
            {
                drawCurve();   
            }
            //if (listBoxMode.SelectedItem.ToString() == "GrahamScan")
            {
            //    drawConvex();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void listBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = listBoxMode.SelectedItem.ToString();
        }
    }
}
