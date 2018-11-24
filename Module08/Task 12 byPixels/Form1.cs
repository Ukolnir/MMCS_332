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
        Graphics g, g2, g3, g4;
        Bitmap image4, texture;

        List<PointPol> points = new List<PointPol>();
        //List<Polygon> pols_rotate = new List<Polygon>();
        Polyhedron pols_rotate = new Polyhedron();
		//List<Polygon> fig = new List<Polygon>();
        Polyhedron fig = new Polyhedron();
        bool fig_drawed = false;
        double ind = 1;
        Color light_color = Color.Green;
        double light_location_x, light_location_y, light_location_z;

		public Form1()
        {
            InitializeComponent();

            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double.TryParse(textBoxLightLocationX.Text, out light_location_x);
            double.TryParse(textBoxLightLocationY.Text, out light_location_y);
            double.TryParse(textBoxLightLocationZ.Text, out light_location_z);

            fig.updLightLocations(light_location_x, light_location_y, light_location_z);

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            //g.ScaleTransform(1, -1);
            //g.TranslateTransform(pictureBox1.Width / 2, -pictureBox1.Height / 2);
            g.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);

            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            g2 = Graphics.FromImage(pictureBox2.Image);
            g2.ScaleTransform(1, -1);
            g2.TranslateTransform(pictureBox2.Width / 2, -pictureBox2.Height / 2);
            //g2.TranslateTransform(pictureBox2.Width / 2, pictureBox2.Height / 2);

            pictureBox3.Image = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            g3 = Graphics.FromImage(pictureBox3.Image);
            //g3.ScaleTransform(1, -1);
            //g3.TranslateTransform(pictureBox3.Width / 2, -pictureBox3.Height / 2);
            g3.TranslateTransform(pictureBox3.Width / 2, pictureBox3.Height / 2);

            pictureBox4.Image = new Bitmap(pictureBox4.Width, pictureBox4.Height);
            g4 = Graphics.FromImage(pictureBox4.Image);
            g4.TranslateTransform(pictureBox4.Width / 2, pictureBox4.Height / 2);
            image4 = new Bitmap(pictureBox4.Image);

            write_axes1();
			write_axes2();
            write_axes3(phi_a, psi_a, ind);
            write_axes4(phi_a, psi_a, ind);

            foreach (Control c in Controls)
            {
                if (c.Name != "buttonLoadTexture")
                    c.Visible = false;
            }

        }

        public void write_axes1()
        {
            PointPol p0 = new PointPol(0, 0, 0);
            PointPol p1 = new PointPol(pictureBox1.Width/2, 0, 0);
            PointPol p2 = new PointPol(0, pictureBox1.Width/2, 0);
            PointPol p3 = new PointPol(0, 0, pictureBox1.Width/2);

            Point o = p0.To2D();
            Point x = p1.To2D();
            Point y = p2.To2D();
            Point z = p3.To2D();

            Font font = new Font("Arial", 8);
            SolidBrush brush = new SolidBrush(Color.Black);
            
            g.DrawString("X", font, brush, x);
            g.DrawString("Y", font, brush, y);
            g.DrawString("Z", font, brush, z);

            Pen my_pen = new Pen(Color.Blue);
            g.DrawLine(my_pen, o, x);
            my_pen.Color = Color.Red;
            g.DrawLine(my_pen, o, y);
            my_pen.Color = Color.Green;
            g.DrawLine(my_pen, o, z);

            pictureBox1.Image = pictureBox1.Image;
        }

		public void write_axes2()
		{
			g2.DrawLine(new Pen(Color.Red), -pictureBox2.Width / 2, 0, pictureBox2.Width / 2, 0);
			g2.DrawLine(new Pen(Color.Red), 0, -pictureBox2.Height / 2, 0, pictureBox2.Height / 2);
			pictureBox2.Image = pictureBox2.Image;
		}

        public void write_axes3(double phi_a, double psi_a, double ind)
        {
            PointPol p0 = new PointPol(0, 0, 0);
            PointPol p1 = new PointPol(pictureBox3.Width / 2*ind, 0, 0);
            PointPol p2 = new PointPol(0, pictureBox3.Width / 2*ind, 0);
            PointPol p3 = new PointPol(0, 0, pictureBox3.Width / 2*ind);

            Point o = p0.To2D(phi_a, psi_a);
            Point x = p1.To2D(phi_a, psi_a);
            Point y = p2.To2D(phi_a, psi_a);
            Point z = p3.To2D(phi_a, psi_a);

            Font font = new Font("Arial", 8);
            SolidBrush brush = new SolidBrush(Color.Black);

            g3.DrawString("X", font, brush, x);
            g3.DrawString("Y", font, brush, y);
            g3.DrawString("Z", font, brush, z);

            Pen my_pen = new Pen(Color.Blue);
            g3.DrawLine(my_pen, o, x);
            my_pen.Color = Color.Red;
            g3.DrawLine(my_pen, o, y);
            my_pen.Color = Color.Green;
            g3.DrawLine(my_pen, o, z);
                
            PointPol newp = new PointPol(Double.Parse(textBoxViewVectorX.Text)*50,
                Double.Parse(textBoxViewVectorY.Text)*50, 
                Double.Parse(textBoxViewVectorZ.Text)*50);
            Point newpp = newp.To2D(phi_a, psi_a);
            g3.DrawEllipse(new Pen(Color.Red), newpp.X - 1, newpp.Y - 1, 2, 2);

            pictureBox3.Image = pictureBox3.Image;
        }

        public void write_axes4(double phi_a, double psi_a, double ind)
        {
            PointPol p0 = new PointPol(0, 0, 0);
            PointPol p1 = new PointPol(pictureBox4.Width / 2 * ind, 0, 0);
            PointPol p2 = new PointPol(0, pictureBox4.Width / 2 * ind, 0);
            PointPol p3 = new PointPol(0, 0, pictureBox4.Width / 2 * ind);

            Point o = p0.To2D(phi_a, psi_a);
            Point x = p1.To2D(phi_a, psi_a);
            Point y = p2.To2D(phi_a, psi_a);
            Point z = p3.To2D(phi_a, psi_a);

            Font font = new Font("Arial", 8);
            SolidBrush brush = new SolidBrush(Color.Black);

            g4.DrawString("X", font, brush, x);
            g4.DrawString("Y", font, brush, y);
            g4.DrawString("Z", font, brush, z);

            Pen my_pen = new Pen(Color.Blue);
            g4.DrawLine(my_pen, o, x);
            my_pen.Color = Color.Red;
            g4.DrawLine(my_pen, o, y);
            my_pen.Color = Color.Green;
            g4.DrawLine(my_pen, o, z);

            PointPol newp = new PointPol(Double.Parse(textBoxViewVectorX.Text) * 50,
                Double.Parse(textBoxViewVectorY.Text) * 50,
                Double.Parse(textBoxViewVectorZ.Text) * 50);
            Point newpp = newp.To2D(phi_a, psi_a);
            g4.DrawEllipse(new Pen(Color.Red), newpp.X - 1, newpp.Y - 1, 2, 2);

            pictureBox4.Image = pictureBox4.Image;
        }

        public void ClearPic1()
        {
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;

            write_axes1();
        }

		public void ClearPic2()
		{
			g2.Clear(pictureBox2.BackColor);
			pictureBox2.Image = pictureBox2.Image;

			write_axes2();
		}

        public void ClearPic3(double phi_a, double psi_a)
        {
            g3.Clear(pictureBox3.BackColor);
            pictureBox3.Image = pictureBox3.Image;

            write_axes3(phi_a, psi_a, ind);
        }

        public void ClearPic4(double phi_a, double psi_a)
        {
            g4.Clear(pictureBox4.BackColor);
            image4 = new Bitmap(pictureBox4.Width, pictureBox4.Height);
            pictureBox4.Image = image4;

            write_axes4(phi_a, psi_a, ind);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;

            ClearPic1();
            ClearPic2();
            ClearPic3(phi_a, psi_a);
            ClearPic4(phi_a, psi_a);
            points.Clear();

        }

        public double[,] matrix_multiplication(double[,] m1, double[,] m2)
        {
            double[,] res = new double[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); ++i)
                for (int j = 0; j < m2.GetLength(1); ++j)
                    for (int k = 0; k < m2.GetLength(0); k++)
                        res[i, j] += m1[i, k] * m2[k, j];
            return res;
        }

        private bool isVisible(Polygon pol, PointPol view_vector)
        {
            PointPol vecNorm = pol.normVecOfPlane();
            if (comboBoxBuildAxis.SelectedItem.ToString() == "X")
                return (scalarProduct(vecNorm, view_vector) < 0);
            return (scalarProduct(vecNorm, view_vector) > 0);
        }

        public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g7 = Graphics.FromImage(result))
            {
                g7.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }

        public static Bitmap RotateImage(Image image, PointF offset, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            //create a new empty bitmap to hold rotated image 
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap 
            Graphics gr = Graphics.FromImage(rotatedBmp);

            //Put the rotation point in the center of the image 
            gr.TranslateTransform(offset.X, offset.Y);

            //rotate the image 
            gr.RotateTransform(angle);

            //move the image back 
            gr.TranslateTransform(-offset.X, -offset.Y);

            //draw passed in image onto graphics object 
            gr.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }

        private Rectangle BoundsOfBitmap(Bitmap bmp)
        {
            GraphicsUnit units = GraphicsUnit.Point;
            RectangleF bmpRectangleF = bmp.GetBounds(ref units);
            Rectangle bmpRectangle = Rectangle.Round(bmpRectangleF);
            return bmpRectangle;
        }

        private void drawPolygon(Polygon pol, Color c, 
            double phi_a, double psi_a, PointPol view_vector)
        {
            Point[] ps = new Point[pol.points.Count()];
            int i = 0;
            foreach (var item in pol.points)
            {
                ps[i] = item.To2D(phi_a, psi_a);
                ++i;
            }
            g.DrawPolygon(new Pen(c), ps);

            if (pol.points.Count() > 2)
            {
                if (isVisible(pol, view_vector))
                {
                    
                    g3.FillPolygon(new SolidBrush(c), ps);
                }
            }
        }

        private bool vectorsEqual(Vector e1, Vector e2)
        {
            return (pointsEqual(e1.P1, e2.P1) && pointsEqual(e1.P2, e2.P2)) ||
                (pointsEqual(e1.P1, e2.P2) && pointsEqual(e1.P2, e2.P1));
        }

        private void buildPolsRotate(string axis, int cnt, ref PointPol rotate_around)
        {
            pols_rotate.clearPolygons();
            List<PointPol> l1 = new List<PointPol>(points);
            double a = 0, b = 0, c = 0;

            Polygon pol = new Polygon(l1);
            //pol.find_center(ref a, ref b, ref c);
            pol.closest_to_zero(axis, ref a, ref b, ref c);

            Vector dir = new Vector(new PointPol(0, 0, 0), new PointPol(1, 0, 0));
            if (axis == "X")
            {
                dir = new Vector(new PointPol(0, 0, 0), new PointPol(1, 0, 0));
                for (int i = 0; i < l1.Count(); ++i)
                    l1[i] = l1[i].shift(0, -b, 0);
                rotate_around.X = a; rotate_around.Y = 0; rotate_around.Z = c;
            }
            if (axis == "Y")
            {
                dir = new Vector(new PointPol(0, 0, 0), new PointPol(0, 1, 0));
                for (int i = 0; i < l1.Count(); ++i)
                    l1[i] = l1[i].shift(-a, 0, 0);
                rotate_around.X = 0; rotate_around.Y = b; rotate_around.Z = c;
            }
            if (axis == "Z")
            {
                dir = new Vector(new PointPol(0, 0, 0), new PointPol(0, 0, 1));
                for (int i = 0; i < l1.Count(); ++i)
                    l1[i] = l1[i].shift(0, -b, 0);
                rotate_around.X = a; rotate_around.Y = 0; rotate_around.Z = c;
            }

            double angle = 360.0 / cnt;
                    
            for (int i = 0; i < cnt; ++i)
            {
                List<PointPol> l2 = new List<PointPol>(l1);

                for (int j = 0; j < l2.Count(); ++j)
                {
                    l2[j] = l2[j].rotate(dir, angle, 0, 0, 0);
                }

                pols_rotate.addPolygon(new Polygon(l1));

                l1 = l2;
            }
        }

        private bool pointsEqual(PointPol p1, PointPol p2)
        {
            return (Math.Abs(p1.X - p2.X) < 0.01) &&
                (Math.Abs(p1.Y - p2.Y) < 0.01) &&
                (Math.Abs(p1.Z - p2.Z) < 0.01);
        }

        private bool polygonsEqual(Polygon pol1, Polygon pol2)
        {
            if (pol1.points.Count() != pol2.points.Count())
                return false;
            if (pol1.edges.Count() != pol2.edges.Count())
                return false;

            for (int i = 0; i < pol1.points.Count(); ++i)
            {
                if (!pointsEqual(pol1.points[i], pol2.points[i]))
                    return false;
            }
            for (int i = 0; i < pol1.edges.Count(); ++i)
            {
                if ((pol1.edges[i].Item1 != pol2.edges[i].Item1 &&
                    pol1.edges[i].Item2 != pol2.edges[i].Item2) ||
                    (pol1.edges[i].Item1 != pol2.edges[i].Item2 &&
                    pol1.edges[i].Item2 != pol2.edges[i].Item1))
                {
                    return false;
                }
            }
            return true;
        }

        private Polygon littlePartOfFig(Vector e1, Vector e2)
        {
            
            List<PointPol> l1 = new List<PointPol>();

            l1.Add(e1.P1);
            l1.Add(e1.P2);

            if (!vectorsEqual(e1, e2))
            {
                if (!pointsEqual(e1.P2, e2.P2))
                    l1.Add(e2.P2);
                if (!pointsEqual(e1.P1, e2.P1))
                    l1.Add(e2.P1);
            }

            Polygon pol = new Polygon(l1);
            return pol;

        }

        private void buildPartOfFig(Polygon p1, Polygon p2, Random r)
        {
            for (int i = 0; i < p1.edges.Count(); ++i)
            {
                Polygon pol = littlePartOfFig(
                    new Vector(p1.points[p1.edges[i].Item1],
                    p1.points[p1.edges[i].Item2]),
                    new Vector(p2.points[p2.edges[i].Item1],
                    p2.points[p2.edges[i].Item2])
                    );
                
                pol.color = Color.FromArgb(100, 100, 100);
                int ind = fig.polygons.FindIndex(polinl => polygonsEqual(polinl, pol));
                if (ind == -1 && pol.edges.Count() > 2)
                    fig.addPolygon(pol);
            }
        }

        private void buildFig(PointPol rotate_around)
        {
            fig.clearPolygons();
            Random r = new Random();
            if (pols_rotate.polygons.Count() > 1)
            {
                for (int i = 1; i < pols_rotate.polygons.Count(); ++i)
                {
                    buildPartOfFig(pols_rotate.polygons[i-1], pols_rotate.polygons[i], r);
                }
                buildPartOfFig(pols_rotate.polygons.Last(), pols_rotate.polygons.First(), r);
            }
        }

        private void drawPols(List<Polygon> pols, double phi_a, double psi_a, PointPol view_vector)
        {
            Random r = new Random();
            foreach (var item in pols)
            {
                drawPolygon(item, item.color, phi_a, psi_a, view_vector);
            }
            pictureBox1.Image = pictureBox1.Image;
            pictureBox3.Image = pictureBox3.Image;
        }
        
        public Color colorByDiffuse(Color c, double diffuse)
        {
            double r = c.R;
            double g = c.G;
            double b = c.B;
            if (diffuse == 1)
                return c;
            if (diffuse > 1)
            {
                r += (255 - r) * (diffuse - 1);
                g += (255 - g) * (diffuse - 1);
                b += (255 - b) * (diffuse - 1);
                return Color.FromArgb((int)r, (int)g, (int)b);
            }
            else
            {
                r = r * diffuse;
                g = g * diffuse;
                b = b * diffuse;
                return Color.FromArgb((int)r, (int)g, (int)b);
            }
            
        }

        public double angleByVectors(Point p1, Point p2)
        {
            int sc_p = p1.X * p2.X + p1.Y * p2.Y;
            double dist1 = Math.Sqrt(p1.X * p1.X + p1.Y * p1.Y);
            double dist2 = Math.Sqrt(p2.X * p2.X + p2.Y * p2.Y);
            double arcos = Math.Acos(sc_p / dist1 / dist2);
            return arcos / Math.PI * 180;
        }

        public void texPointsOnOneBorder(Point p1, Point p2, Point ptex1, Point ptex2, 
            int wdiv2, int hdiv2, Bitmap tex, int tex_w, int tex_h,
            ref Dictionary<int, List<Tuple<int, Point>>> pol_borders)
        {
            int x1 = p1.X;
            int y1 = p1.Y;
            int x2 = p2.X;
            int y2 = p2.Y;
            int diffx = Math.Abs(x2 - x1);
            int diffy = Math.Abs(y2 - y1);
            int diff = Math.Max(diffx, diffy);
            double stepx = (double)(x2 - x1) / diff;
            double stepy = (double)(y2 - y1) / diff;
            double steptexx = (double)(ptex2.X - ptex1.X) / diff;
            double steptexy = (double)(ptex2.Y - ptex1.Y) / diff;

            List<int> xs = new List<int>();
            List<int> ys = new List<int>();
            for (int i = 0; i < diff; ++i)
            {
                int currx = (int)Math.Round(x1 + stepx * i);
                int curry = (int)Math.Round(y1 + stepy * i);
                int currtexx = (int)Math.Round(ptex1.X + steptexx * i);
                int currtexy = (int)Math.Round(ptex1.Y + steptexy * i);
                //int currb = (int)Math.Round(c1.B + stepb * i);

                int indxinxs = xs.FindIndex(xinl => (xinl == currx));// || (p.Item1.Y == curry));// && (p.Item1.Y == curry));
                int indyinys = ys.FindIndex(yinl => (yinl == curry));
                if ((indxinxs == -1) || (indyinys == -1))
                {
                    bool y_repeat = pol_borders.Keys.Contains(curry);
                    if (y_repeat)
                    {
                        pol_borders[curry].Add(Tuple.Create(currx, new Point(currtexx, currtexy)));
                    }
                    else
                    {
                        pol_borders.Add(curry, new List<Tuple<int, Point>>());
                        pol_borders[curry].Add(Tuple.Create(currx, new Point(currtexx, currtexy)));
                    }
                }
                if (indxinxs == -1)
                    xs.Add(currx);
                if (indyinys == -1)
                    ys.Add(curry);

                if ((currx > -wdiv2) && (currx < wdiv2) && (curry > -hdiv2) && (curry < hdiv2))
                {
                    if ((currtexx >= 0) && (currtexx < tex_w) && (currtexy >= 0) && (currtexy < tex_h))
                    {
                        Color currcolor = tex.GetPixel(currtexx, currtexy);
                        image4.SetPixel(currx + wdiv2, curry + hdiv2, currcolor);
                    }
                }
            }
        }
        
        public void drawLineBetweenPoints(Point p1, Point p2, double diffuse1, double diffuse2,
            PointF ptex1, PointF ptex2,
            int wdiv2, int hdiv2, ref Dictionary<int, List<Tuple<int, Tuple<double, PointF>>>> pol_borders)
        {
            int x1 = p1.X;
            int y1 = p1.Y;
            int x2 = p2.X;
            int y2 = p2.Y;
            int diffx = Math.Abs(x2 - x1);
            int diffy = Math.Abs(y2 - y1);
            int diff = Math.Max(diffx, diffy);
            double stepx = (double)(x2 - x1) / diff;
            double stepy = (double)(y2 - y1) / diff;
            double stepdiffuse = (diffuse2 - diffuse1) / diff;
            float steptexx = (ptex2.X - ptex1.X) / diff;
            float steptexy = (ptex2.Y - ptex1.Y) / diff;

            List<int> xs = new List<int>();
            List<int> ys = new List<int>();
            for (int i = 0; i < diff; ++i)
            {
                int currx = (int)Math.Round(x1 + stepx * i);
                int curry = (int)Math.Round(y1 + stepy * i);
                double currdiffuse = diffuse1 + stepdiffuse * i;
                float currtexx = ptex1.X + steptexx * i;
                float currtexy = ptex1.Y + steptexy * i;
                //int currb = (int)Math.Round(c1.B + stepb * i);

                int indxinxs = xs.FindIndex(xinl => (xinl == currx));// || (p.Item1.Y == curry));// && (p.Item1.Y == curry));
                int indyinys = ys.FindIndex(yinl => (yinl == curry));
                if ((indxinxs == -1) || (indyinys == -1))
                {
                    bool y_repeat = pol_borders.Keys.Contains(curry);
                    if (y_repeat)
                    {
                        pol_borders[curry].Add(Tuple.Create(currx, 
                            Tuple.Create(currdiffuse, 
                            new PointF(currtexx, currtexy))));
                    }
                    else
                    {
                        pol_borders.Add(curry, new List<Tuple<int, 
                            Tuple<double, PointF>>>());
                        pol_borders[curry].Add(Tuple.Create(currx,
                            Tuple.Create(currdiffuse,
                            new PointF(currtexx, currtexy))));
                    }
                }
                if (indxinxs == -1)
                    xs.Add(currx);
                if (indyinys == -1)
                    ys.Add(curry);

                if ((currx > -wdiv2) && (currx < wdiv2) && (curry > -hdiv2) && (curry < hdiv2))
                    image4.SetPixel(currx + wdiv2, curry + hdiv2, colorByDiffuse(light_color, currdiffuse));
            }
        }
              
        public void drawTextureBetweenPoints(Point p1, Point p2, Tuple<double, PointF> par1, Tuple<double, PointF> par2,
            int wdiv2, int hdiv2, int xmin, int xmax, int ymin, int ymax, Bitmap tex, Rectangle bounds)
        {
            int x1 = p1.X;
            int y1 = p1.Y;
            int x2 = p2.X;
            int y2 = p2.Y;
            int diffx = Math.Abs(x2 - x1);
            int diffy = Math.Abs(y2 - y1);
            int diff = Math.Max(diffx, diffy);
            double stepx = (double)(x2 - x1) / diff;
            double stepy = (double)(y2 - y1) / diff;
            double stepdiffuse = (par2.Item1 - par1.Item1) / diff;
            float steptexx = (par2.Item2.X - par1.Item2.X) / diff;
            float steptexy = (par2.Item2.Y - par1.Item2.Y) / diff;

            List<int> xs = new List<int>();
            List<int> ys = new List<int>();
            for (int i = 0; i < diff; ++i)
            {
                int currx = (int)Math.Round(x1 + stepx * i);
                int curry = (int)Math.Round(y1 + stepy * i);
                double currdiffuse = par1.Item1 + stepdiffuse * i;
                float currtexx = par1.Item2.X + steptexx * i;
                float currtexy = par1.Item2.Y + steptexy * i;
                int texx = (int)Math.Round(currtexx * bounds.Width);
                int texy = (int)Math.Round(currtexy * bounds.Height);
                

                int indxinxs = xs.FindIndex(xinl => (xinl == currx));
                int indyinys = ys.FindIndex(yinl => (yinl == curry));
            
                if (indxinxs == -1)
                    xs.Add(currx);
                if (indyinys == -1)
                    ys.Add(curry);

                if ((currx > -wdiv2) && (currx < wdiv2) && (curry > -hdiv2) && (curry < hdiv2))
                {
                    Color currcolor = Color.Black;
                    /*
                    if ((currx - xmin > 0) && ((currx - xmin) < (xmax - xmin)) && (curry - ymin > 0) && ((curry - ymin) < (ymax - ymin)))
                    {
                        currcolor = tex.GetPixel((currx - xmin), (curry - ymin));  
                    }*/
                    if ((texx >= 0) && (texx < bounds.Width) && (texy >= 0) && (texy < bounds.Height))
                    {
                        currcolor = tex.GetPixel(texx, texy);
                    }          
                    image4.SetPixel(currx + wdiv2, curry + hdiv2, colorByDiffuse(currcolor, currdiffuse));
                }
            }
        }
        
        public void drawPolyhedron(Polyhedron polyhed, Color c, double phi_a, double psi_a, 
            PointPol view_vector)
        {
            int wdiv2 = pictureBox4.Width / 2;
            int hdiv2 = pictureBox4.Height / 2;

            image4 = new Bitmap(pictureBox4.Image);

            if (polyhed.polygons.Count() > 3)
            {
                List<Point> points2d = new List<Point>();
                foreach (var p in polyhed.points)
                {
                    points2d.Add(p.To2D(phi_a, psi_a));
                }
                List<Point> sorted_points = polyhed.points.OrderByDescending(
                    p => p.Y).Select(p => p.To2D(phi_a, psi_a)).ToList();

                foreach (var pol in polyhed.polygons)
                {
                    if (isVisible(pol, view_vector))
                    { 
                        Dictionary<int, PointF> vertex_to_texpoint = new Dictionary<int, PointF>();
                        if ((pol.points.Count() == 3) || (pol.points.Count() == 4))
                        {
                            vertex_to_texpoint.Add(0, new PointF(0, 0));
                            vertex_to_texpoint.Add(1, new PointF(1, 0));
                            vertex_to_texpoint.Add(2, new PointF(1, 1));
                            if (pol.points.Count() == 4)
                                vertex_to_texpoint.Add(3, new PointF(0, 1));
                        }
                        else
                            return;
                        //y, [<x, diffuse>]
                        Dictionary<int, List<Tuple<int, Tuple<double, PointF>>>> pol_borders = new Dictionary<int, List<Tuple<int, Tuple<double, PointF>>>>();
                        
                        List<int> indices_in_points = new List<int>();

                        foreach (var p in pol.points)
                        {
                            int ind = polyhed.points.FindIndex(pinpol => pointsEqual(pinpol, p));
                            //if (ind != 1)
                                indices_in_points.Add(ind);
                        }
                        //draw vertices
                        foreach(var indinp in indices_in_points)
                        {
                            int x = points2d[indinp].X;
                            int y = points2d[indinp].Y;
                            Color cdiff = colorByDiffuse(c, polyhed.point_to_diffuse[indinp]);
                            if ((x > -wdiv2) && (x < wdiv2) && (y > -hdiv2) && (y < hdiv2))
                                image4.SetPixel(x+wdiv2, y+hdiv2, cdiff);
                        }



                        //find indices of points in edges
                        List<Tuple<int, int>> ind_edges_in_points = new List<Tuple<int, int>>();
                        foreach (var e in pol.edges)
                        {
                            int ind1 = polyhed.points.FindIndex(pinpol => pointsEqual(pinpol, pol.points[e.Item1]));
                            int ind2 = polyhed.points.FindIndex(pinpol => pointsEqual(pinpol, pol.points[e.Item2]));
                            ind_edges_in_points.Add(Tuple.Create(ind1, ind2));
                        }

                        //draw borders
                        int i = 0;
                        foreach (var edge in ind_edges_in_points)
                        {
                            Point p1 = points2d[edge.Item1];
                            Point p2 = points2d[edge.Item2];
                            double diff1 = polyhed.point_to_diffuse[edge.Item1];
                            double diff2 = polyhed.point_to_diffuse[edge.Item2];
                            int ind1 = pol.edges[i].Item1;
                            int ind2 = pol.edges[i].Item2;
                            drawLineBetweenPoints(p1, p2, diff1, diff2, 
                                vertex_to_texpoint[ind1], vertex_to_texpoint[ind2], wdiv2, hdiv2, ref pol_borders);
                            ++i;
                        }

                        //find 
                        int ymin = pol_borders.Keys.Min();
                        int ymax = pol_borders.Keys.Max();
                        int xmin = pol_borders.Values.Min(t1 => t1.Min(t2 => t2.Item1));
                        int xmax = pol_borders.Values.Max(t1 => t1.Max(t2 => t2.Item1));
                        Bitmap curr_tex = new Bitmap(texture);
                        Rectangle bounds = BoundsOfBitmap(curr_tex);

                        //draw inner part
                        if ((ymax - ymin > 0) && (xmax - xmin > 0))
                        {
                            curr_tex = ResizeBitmap(curr_tex, (xmax - xmin), (ymax - ymin));
                            bounds = BoundsOfBitmap(curr_tex);

                            foreach (var y in pol_borders.Keys)
                            {
                                //xmin = pol_borders[y].Min(t => t.Item1);
                                //xmax = pol_borders[y].Max(t => t.Item1);
                                if (xmax - xmin > 0)
                                {
                                    //curr_tex = ResizeBitmap(texture, (xmax - xmin), (ymax - ymin));
                                    //bounds = BoundsOfBitmap(curr_tex);
                                    Tuple<int, Tuple<double, PointF>> tmin = pol_borders[y].First();
                                    Tuple<int, Tuple<double, PointF>> tmax = pol_borders[y].First();
                                    foreach (var t in pol_borders[y])
                                    {
                                        if (t.Item1 < tmin.Item1)
                                            tmin = t;
                                        if (t.Item1 > tmax.Item1)
                                            tmax = t;
                                    }
                                    
                                    //Dictionary<int, List<Tuple<int, Tuple<double, Point>>>> to_del = new Dictionary<int, List<Tuple<int, Tuple<double, Point>>>>();
                                    drawTextureBetweenPoints(new Point(tmin.Item1, y), new Point(tmax.Item1, y),
                                        tmin.Item2, tmax.Item2, wdiv2, hdiv2, xmin, xmax, ymin, ymax, curr_tex, bounds);
                                }
                            }
                        }
                    } 
                }
                pictureBox4.Image = image4;
                //pictureBox4.Image = pictureBox4.Image;
                //image4 = (Bitmap)pictureBox4.Image;
            }
        }
        
        public void find_center(List<Polygon> pols, ref double x, ref double y, ref double z)
		{
			x = 0; y = 0; z = 0;

			double a = 0, b = 0, c = 0;

			foreach (var p in pols)
			{
				p.find_center(ref a, ref b, ref c);
				x += a;
				y += b;
				z += c;
			}

			x /= pols.Count();
			y /= pols.Count();
			z /= pols.Count();
		}

        private void drawPointsOnPic2()
        {
            Color c = Color.Green;
            for (int i = 1; i < points.Count(); ++i)
            {
                g2.DrawLine(new Pen(c), (float)points[i - 1].X, (float)points[i - 1].Y, (float)points[i].X, (float)points[i].Y);
                //g2.DrawEllipse(new Pen(Color.Black), (float)points[i - 1].X - 1, (float)points[i - 1].X - 1, 2, 2);
            }
            g2.DrawLine(new Pen(c), (float)points.Last().X, (float)points.Last().Y, (float)points.First().X, (float)points.First().Y);
            g2.DrawEllipse(new Pen(Color.Black), (float)points.First().X - 1, (float)points.First().Y - 1, 2, 2);
        }

        private string save()
        {
            string result = "";
            foreach (var pol in fig.polygons)
            {
                foreach (var p in pol.points)
                {
                    
                    result += p.X.ToString() + ";" + p.Y.ToString() + ";" + p.Z.ToString();
                    if (p != pol.points.Last())
                    {
                        result += " ";
                    }
                }

                if (pol != fig.polygons.Last())
                    result += Environment.NewLine;
            }
            //textBox1.Text = result;
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);

            ClearPic1();
            ClearPic3(phi_a, psi_a);
            ClearPic4(phi_a, psi_a);

            string ax = comboBoxBuildAxis.SelectedItem.ToString();
            int cnt = Int32.Parse(textBoxBuildCount.Text);
        

            PointPol rotate_around = new PointPol(0, 0, 0);
            buildPolsRotate(ax, cnt, ref rotate_around);
            drawPols(pols_rotate.polygons, phi_a, psi_a, new PointPol(view_x, view_y, view_z));

            buildFig(rotate_around);
            buildFig(rotate_around);
            //drawPols(fig);
            fig_drawed = false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X - pictureBox1.Width / 2;
            int y = pictureBox1.Height / 2 - e.Y;
            int z = Int32.Parse(textBoxBuildZ.Text);
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);

            points.Add(new PointPol(x, y, z));

            if (points.Count() > 1)
            {
                ClearPic1();
                ClearPic3(phi_a, psi_a);
                Polygon pol = new Polygon(points);
                drawPolygon(pol, Color.Black, phi_a, psi_a, 
                    new PointPol(view_x, view_y, view_z));
                pictureBox1.Image = pictureBox1.Image;
                pictureBox3.Image = pictureBox3.Image;
            }

            labelDebug.Text = "x = " + x.ToString() +
                " | y = " + y.ToString() + " | z = " + z.ToString();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X - pictureBox1.Width / 2;
            int y = pictureBox1.Height / 2 - e.Y;
            labelDebug2.Text = "x = " + x.ToString() + " | y = " + y.ToString();
        }

        private void reDrawPols(double phi_a, double psi_a, PointPol view_vector)
        {
            if (fig_drawed)
            {
                ClearPic1();
                ClearPic3(phi_a, psi_a);
                ClearPic4(phi_a, psi_a);
                drawPols(fig.polygons, phi_a, psi_a, view_vector);
                drawPolyhedron(fig, light_color, phi_a, psi_a, view_vector);
            }
            else
            {
                ClearPic1();
                ClearPic3(phi_a, psi_a);
                ClearPic4(phi_a, psi_a);
                drawPols(pols_rotate.polygons, phi_a, psi_a, view_vector);
            }
        }

        

        private void buttonScale_Click(object sender, EventArgs e)
        {
            double ind = Double.Parse(textBoxScale.Text);
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);

            pols_rotate.scale(ind);
            fig.scale(ind);

            reDrawPols(phi_a, psi_a, new PointPol(view_x, view_y, view_z));
        }      

        private void buttonShift_Click(object sender, EventArgs e)
        {
            int x = Int32.Parse(textBoxShiftX.Text);
            int y = Int32.Parse(textBoxShiftY.Text);
            int z = Int32.Parse(textBoxShiftZ.Text);
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);

            pols_rotate.shift(x, y, z);
            fig.shift(x, y, z);

            reDrawPols(phi_a, psi_a, new PointPol(view_x, view_y, view_z));
        }

        private void buttonReflection_Click(object sender, EventArgs e)
        {
            string axis = comboBoxReflection.SelectedItem.ToString();
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);

            pols_rotate.reflection(axis);
            fig.reflection(axis);

            reDrawPols(phi_a, psi_a, new PointPol(view_x, view_y, view_z));
        }

        private void buttonRotate_Click(object sender, EventArgs e)
        {
            int x1 = Int32.Parse(textBoxX1.Text);
            int y1 = Int32.Parse(textBoxY1.Text);
            int z1 = Int32.Parse(textBoxZ1.Text);
            int x2 = Int32.Parse(textBoxX2.Text);
            int y2 = Int32.Parse(textBoxY2.Text);
            int z2 = Int32.Parse(textBoxZ2.Text);
            double angle = Double.Parse(textBoxAngle.Text);
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);

            pols_rotate.rotate(x1, y1, z1, x2, y2, z2, angle);
            fig.rotate(x1, y1, z1, x2, y2, z2, angle);

            reDrawPols(phi_a, psi_a, new PointPol(view_x, view_y, view_z));
        }

        private void buttonBuild2_Click(object sender, EventArgs e)
        {
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);

            ClearPic1();
            ClearPic3(phi_a, psi_a);
            ClearPic4(phi_a, psi_a);

            string ax = comboBoxBuildAxis.SelectedItem.ToString();
            int cnt = Int32.Parse(textBoxBuildCount.Text);

            PointPol rotate_around = new PointPol(0, 0, 0);
            buildPolsRotate(ax, cnt, ref rotate_around);
            //drawPols(pols_rotate);

            buildFig(rotate_around);
            drawPols(fig.polygons, phi_a, psi_a, new PointPol(view_x, view_y, view_z));
            drawPolyhedron(fig, light_color, phi_a, psi_a, new PointPol(view_x, view_y, view_z));
            fig_drawed = true;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X - pictureBox2.Width / 2;
            int y = pictureBox2.Height / 2 - e.Y;
            //int y = e.Y - pictureBox2.Height / 2;
            labelDebug2.Text = "x = " + x.ToString() + " | y = " + y.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Filter = "Text Files(*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog1.FileName, false, System.Text.Encoding.Default))
                {
                    string text = save();
                    sw.WriteLine(text);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);

            reDrawPols(phi_a, psi_a, new PointPol(view_x, view_y, view_z));
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X - pictureBox2.Width/2;
            int y = pictureBox2.Height/2 - e.Y;
            //int y = e.Y - pictureBox2.Height / 2;
            int z = 0;
            x = x - (x % 7);
            y = y - (y % 7);

            points.Add(new PointPol(x, y, z));
            ClearPic2();
            drawPointsOnPic2();
            pictureBox2.Image = pictureBox2.Image;
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);

            if (points.Count() > 1)
            {
                ClearPic1();
                ClearPic3(phi_a, psi_a);
                Polygon pol = new Polygon(points);
                drawPolygon(pol, Color.Black, phi_a, psi_a, 
                    new PointPol(view_x, view_y, view_z));
                pictureBox1.Image = pictureBox1.Image;
                pictureBox3.Image = pictureBox3.Image;
            }

            labelDebug.Text = "x = " + x.ToString() +
                " | y = " + y.ToString() + " | z = " + z.ToString();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string axis = comboBoxRotationAxis.SelectedItem.ToString();
            double time = double.Parse(textBoxRotationTime.Text);

            int x1 = 0;
            int y1 = 0;
            int z1 = 0;

            int x2 = axis == "X" ? 1 : 0;
            int y2 = axis == "Y" ? 1 : 0;
            int z2 = axis == "Z" ? 1 : 0;

            double angle = 2;
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);
            int sleep = int.Parse(textBoxSleep.Text);

            DateTime date1 = DateTime.Now;
            while (true)
            {
                DateTime date2 = DateTime.Now;
                if (date2.Subtract(date1).TotalSeconds >= time)
                    break;

                pols_rotate.rotate(x1, y1, z1, x2, y2, z2, angle);
                fig.rotate(x1, y1, z1, x2, y2, z2, angle);

                reDrawPols(phi_a, psi_a, new PointPol(view_x, view_y, view_z));
                pictureBox3.Image = pictureBox3.Image;
                //pictureBox4.Image = pictureBox4.Image;

                System.Threading.Thread.Sleep(sleep);
                Application.DoEvents();
            }
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);

            reDrawPols(phi_a, psi_a, new PointPol(view_x, view_y, view_z));
        }

        private void hScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);

            reDrawPols(phi_a, psi_a, new PointPol(view_x, view_y, view_z));
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {           
            double curr_ind = 1.2;
            ind *= curr_ind;
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);

            pols_rotate.scale(curr_ind);
            fig.scale(curr_ind);

            reDrawPols(phi_a, psi_a, new PointPol(view_x, view_y, view_z));
        }

        private void buttonLightColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.ShowDialog();
            light_color = cd.Color;
        }

        private void buttonDoLight_Click(object sender, EventArgs e)
        {
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);
            double.TryParse(textBoxLightLocationX.Text, out light_location_x);
            double.TryParse(textBoxLightLocationY.Text, out light_location_y);
            double.TryParse(textBoxLightLocationZ.Text, out light_location_z);

            ClearPic4(phi_a, psi_a);

            fig.updLightLocations(light_location_x, light_location_y, light_location_z);
            drawPolyhedron(fig, light_color, phi_a, psi_a, new PointPol(view_x, view_y, view_z));
            //pictureBox4.Image = pictureBox4.Image;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double time = double.Parse(textBoxLightTime.Text);

            double curr_angle = 0;
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);
            double angle = double.Parse(textBoxLightAngle.Text);
            int sleep = int.Parse(textBoxLightSleep.Text);

            DateTime date1 = DateTime.Now;
            while (true)
            {
                DateTime date2 = DateTime.Now;
                if (date2.Subtract(date1).TotalSeconds >= time)
                    break;

                int radius = 300;
                light_location_x = (int)Math.Round(radius * Math.Cos(curr_angle));
                light_location_z = (int)Math.Round(radius * Math.Sin(curr_angle));
                curr_angle += Math.PI / 360 * angle;

                fig.updLightLocations(light_location_x, light_location_y, light_location_z);
                reDrawPols(phi_a, psi_a, new PointPol(view_x, view_y, view_z));

                pictureBox3.Image = pictureBox3.Image;
                //pictureBox4.Image = pictureBox4.Image;

                System.Threading.Thread.Sleep(sleep);
                Application.DoEvents();
            }
        }

        private void buttonLoadTexture_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "fruites.jpg";
            ofd.ShowDialog();
            texture = new Bitmap(ofd.FileName, true);

            foreach (Control c in Controls)
            {
                if (c.Name != "buttonLoadTexture" && c.Name != "pictureBox3")
                    c.Visible = true;
            }

            //Bitmap bmp = RotateImage(texture, 
            //new PointF(pictureBox1.Width/2, pictureBox1.Height/2), 30);
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            /*Rectangle bmpRect = BoundsOfBitmap(texture);
            Bitmap bmp = RotateImage(texture,
                new PointF(bmpRect.Width / 2, bmpRect.Height / 2), 30);
            bmp = bmp.Clone(bmpRect, bmp.PixelFormat);
            bmp = ResizeBitmap(bmp, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;*/
        }

        private void textBoxLightLocationX_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(textBoxLightLocationX.Text, out light_location_x);
            double.TryParse(textBoxLightLocationY.Text, out light_location_y);
            double.TryParse(textBoxLightLocationZ.Text, out light_location_z);
        }

        private void textBoxLightLocationY_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(textBoxLightLocationX.Text, out light_location_x);
            double.TryParse(textBoxLightLocationY.Text, out light_location_y);
            double.TryParse(textBoxLightLocationZ.Text, out light_location_z);
        }

        private void textBoxLightLocationZ_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(textBoxLightLocationX.Text, out light_location_x);
            double.TryParse(textBoxLightLocationY.Text, out light_location_y);
            double.TryParse(textBoxLightLocationZ.Text, out light_location_z);
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            double curr_ind = 0.83333;
            ind *= curr_ind;
            double phi_a = hScrollBar1.Value;
            double psi_a = hScrollBar2.Value;
            double view_x = double.Parse(textBoxViewVectorX.Text);
            double view_y = double.Parse(textBoxViewVectorY.Text);
            double view_z = double.Parse(textBoxViewVectorZ.Text);

            pols_rotate.scale(curr_ind);
            fig.scale(curr_ind);

            reDrawPols(phi_a, psi_a, new PointPol(view_x, view_y, view_z));
        }
	
        private double scalarProduct(PointPol vec1, PointPol vec2)
        {
            return (vec1.X * vec2.X + vec1.Y * vec2.Y + vec1.Z * vec2.Z);
        }
    }


    public class PointPol
    {
        public double X, Y, Z, W;

        public PointPol(double x, double y, double z)
        {
            X = x; Y = y; Z = z; W = 1;
        }

        public PointPol(double x, double y, double z, double w)
        {
            X = x; Y = y; Z = z; W = w;
        }

        public double[,] getP()
        {
            return new double[1, 4] { { X, Y, Z, W } };
        }

        public double[,] getP1()
        {
            return new double[3, 1] { { X }, { Y }, { Z } };
        }

        public double[,] getPol()
        {
            return new double[4, 1] { { X }, { Y }, { Z }, { W } };
        }

        public double[,] matrix_multiplication(double[,] m1, double[,] m2)
        {
            double[,] res = new double[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); ++i)
                for (int j = 0; j < m2.GetLength(1); ++j)
                    for (int k = 0; k < m2.GetLength(0); k++)
                        res[i, j] += m1[i, k] * m2[k, j];
            return res;
        }

        public PointPol scale(double ind_scale, double a, double b, double c)
        {
            double[,] transfer = new double[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { -a, -b, -c, 1 } };
            var t1 = matrix_multiplication(getP(), transfer);

            t1 = matrix_multiplication(t1, new double[4, 4] { { ind_scale, 0, 0, 0 }, { 0, ind_scale, 0, 0 }, { 0, 0, ind_scale, 0 }, { 0, 0, 0, 1 } });
            transfer = new double[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { a, b, c, 1 } };
            t1 = matrix_multiplication(t1, transfer);

            return translatePol(t1);
        }

        public PointPol rotate(Vector direction, double angle, double a, double b, double c)
        {
            double phi = Math.PI / 360 * angle;
            PointPol p = shift(-a, -b, -c);

            double x1 = direction.P1.X;
            double y1 = direction.P1.Y;
            double z1 = direction.P1.Z;

            double x2 = direction.P2.X;
            double y2 = direction.P2.Y;
            double z2 = direction.P2.Z;

            double vecx = x2 - x1;
            double vecy = y2 - y1;
            double vecz = z2 - z1;

            double len = Math.Sqrt(vecx * vecx + vecy * vecy + vecz * vecz);

            double l = vecx / len;
            double m = vecy / len;
            double n = vecz / len;

            double[,] transfer = new double[4, 4] {
                    { l*l+Math.Cos(phi)*(1 - l*l), l*(1-Math.Cos(phi))*m + n*Math.Sin(phi), l*(1-Math.Cos(phi))*n - m*Math.Sin(phi), 0 },
                    { l*(1-Math.Cos(phi))*m - n*Math.Sin(phi), m*m+Math.Cos(phi)*(1 - m*m), m*(1-Math.Cos(phi))*n + l*Math.Sin(phi), 0 },
                    { l*(1-Math.Cos(phi))*n + m*Math.Sin(phi), m*(1-Math.Cos(phi))*n - l*Math.Sin(phi), n*n+Math.Cos(phi)*(1 - n*n), 0 },
                    { 0, 0, 0, 1 } };
            var t1 = matrix_multiplication(p.getP(), transfer);

            t1 = matrix_multiplication(t1, transfer);

            PointPol p2 = translatePol(t1);
            PointPol p3 = p2.shift(a, b, c);

            return p3;
        }

        public PointPol reflection(string axis)
        {

            if (axis == "X")
            {
                return new PointPol(-X, Y, Z);
            }
            if (axis == "Y")
            {
                return new PointPol(X, -Y, Z);
            }
            if (axis == "Z")
            {
                return new PointPol(X, Y, -Z);
            }
            return new PointPol(X, Y, Z);
        }

        private PointPol translatePol(double[,] f)
        {
            return new PointPol(f[0, 0], f[0, 1], f[0, 2], f[0, 3]);
        }

        private PointPol translatePol1(double[,] f)
        {
            return new PointPol(f[0, 0], f[1, 0], f[2, 0], f[3, 0]);
        }

        public PointPol shift(double x, double y, double z)
        {
            double[,] shiftMatrix = new double[4, 4] { { 1, 0, 0, x }, { 0, 1, 0, y }, { 0, 0, 1, z }, { 0, 0, 0, 1 } };
            return translatePol1(matrix_multiplication(shiftMatrix, getPol()));
        }

		public PointPol normalize()
		{
			double len = Math.Sqrt(X * X + Y * Y + Z * Z);
			return new PointPol(X / len, Y / len, Z / len);
		}

        public Point To2D()
        {
            double phi = Math.PI / 180 * 145;
            double psi = Math.PI / 180 * 45;
            double[,] displayMatrix1 = new double[4, 4] {
                { 1, 0, 0, 0 },
                { 0, Math.Cos(phi), Math.Sin(phi), 0 },
                { 0, -Math.Sin(phi), Math.Cos(phi), 0 },
                { 0, 0, 0, 1 } };
            double[,] displayMatrix2 = new double[4, 4] {
                { Math.Cos(psi), 0, -Math.Sin(psi), 0 },
                { 0, 1, 0, 0 },
                { Math.Sin(psi), 0, Math.Cos(psi), 0 },
                { 0, 0, 0, 1 } };

            var displayMatrix = matrix_multiplication(displayMatrix1, displayMatrix2);
            var temp = matrix_multiplication(displayMatrix, getP1());
            int x = Convert.ToInt32(temp[0, 0]);
            int y = Convert.ToInt32(temp[1, 0]);

            var temp2d = new Point(x, y);
            return temp2d;
        }

        public Point To2D(double phi_a, double psi_a)
        {
            double phi = Math.PI / 180 * phi_a;
            double psi = Math.PI / 180 * psi_a;
            double[,] displayMatrix1 = new double[4, 4] {
                { 1, 0, 0, 0 },
                { 0, Math.Cos(phi), Math.Sin(phi), 0 },
                { 0, -Math.Sin(phi), Math.Cos(phi), 0 },
                { 0, 0, 0, 1 } };
            double[,] displayMatrix2 = new double[4, 4] {
                { Math.Cos(psi), 0, -Math.Sin(psi), 0 },
                { 0, 1, 0, 0 },
                { Math.Sin(psi), 0, Math.Cos(psi), 0 },
                { 0, 0, 0, 1 } };

            var displayMatrix = matrix_multiplication(displayMatrix1, displayMatrix2);
            var temp = matrix_multiplication(displayMatrix, getP1());
            int x = Convert.ToInt32(temp[0, 0]);
            int y = Convert.ToInt32(temp[1, 0]);

            var temp2d = new Point(x, y);
            return temp2d;
        }
    }

    public class Vector
    {
        public PointPol P1;
        public PointPol P2;

        public Vector(PointPol p1, PointPol p2) { P1 = p1; P2 = p2; }

        public void scale(double ind_scale, double a, double b, double c)
        {
            P1 = P1.scale(ind_scale, a, b, c);
            P2 = P2.scale(ind_scale, a, b, c);
        }

        public void shift(double a, double b, double c)
        {
            P1 = P1.shift(a, b, c);
            P2 = P2.shift(a, b, c);
        }

        public void rotate(Vector e, double angle, double a, double b, double c)
        {
            P1 = P1.rotate(e, angle, a, b, c);
            P2 = P2.rotate(e, angle, a, b, c);
        }

        public void reflection(string axis)
        {

            if (axis == "X")
            {
				P1.X = -P1.X;
            }
            if (axis == "Y")
            {
				P1.Y = -P1.Y;
			}
            if (axis == "Z")
            {
				P1.Z = -P1.Z;
			}
        }

        public Tuple<Point, Point> to2d()
        {
            return Tuple.Create(P1.To2D(), P2.To2D());
        }
    }

    public class Edge
    {
        public int nP1;
        public int nP2;


        public Edge(int p1, int p2) { nP1 = p1; nP2 = p2; }
    }

    public class PolygonNew
    {
        public List<Edge> edges = new List<Edge>();

        //по граням
        public PolygonNew(List<Edge> edg)
        {
            foreach (var el in edg)
            {
                edges.Add(el);
            }
        }
    }

    public class Polygon
    {
        public List<PointPol> points = new List<PointPol>();
        public List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
        public Color color;

        /*public Polygon(List<Edge> edg)
        {
            foreach (var el in edg)
            {
                edges.Add(el);
                //points.Add(el.P1);
                //points.Add(el.P2);
            }
        }*/
        //грани
        public Polygon(List<PointPol> ps)
        { 
            for (int i = 0; i < ps.Count(); ++i)
            {
                PointPol p = ps[i];
                int ind = points.FindIndex(pinl => pointsEqual(p, pinl));
                if (ind == -1)
                    points.Add(p);
            }

            if (points.Count() > 1)
            {
                PointPol p2, p1 = ps.First();
                int indp, ind2, ind1 = points.FindIndex(pinl => pointsEqual(p1, pinl));
                for (int i = 1; i < ps.Count(); ++i)
                {
                    p2 = ps[i];
                    ind2 = points.FindIndex(pinl => pointsEqual(p2, pinl));
                    indp = edges.FindIndex(pair =>
                        (pair.Item1 == ind2) && (pair.Item2 == ind1));
                    if ((ind1 != ind2) && (indp == -1))
                        edges.Add(Tuple.Create(ind1, ind2));
                    ind1 = ind2;
                }

                p2 = ps.First();
                ind2 = points.FindIndex(pinl => pointsEqual(p2, pinl));
                indp = edges.FindIndex(pair =>
                    (pair.Item1 == ind2) && (pair.Item2 == ind1));
                if ((ind1 != ind2) && (indp == -1))
                    edges.Add(Tuple.Create(ind1, ind2));
            }

            deleteDuplicates();
        }

        private void deleteDuplicates()
        {
            edges = edges.Distinct().ToList();
        }

        private bool pointsEqual(PointPol p1, PointPol p2)
        {
            return (Math.Abs(p1.X - p2.X) < 0.01) &&
                (Math.Abs(p1.Y - p2.Y) < 0.01) &&
                (Math.Abs(p1.Z - p2.Z) < 0.01);
        }
        private bool vectorsEqual(Vector e1, Vector e2)
        {
            return (pointsEqual(e1.P1, e2.P1) && pointsEqual(e1.P2, e2.P2)) ||
                (pointsEqual(e1.P1, e2.P2) && pointsEqual(e1.P2, e2.P1));
        }
        /*
        public void find_center(ref double x, ref double y, ref double z)
        {
            x = 0; y = 0; z = 0;

            foreach (var p in points)
            {
                x += p.X;
                y += p.Y;
                z += p.Z;
            }

            x /= points.Count();
            y /= points.Count();
            z /= points.Count();
        }
        */

        public void find_center(ref double x, ref double y, ref double z)
        {
            x = 0; y = 0; z = 0;

            foreach (var p in points)
            {
                x += p.X;
                y += p.Y;
                z += p.Z;
            }

            x /= points.Count();
            y /= points.Count();
            z /= points.Count();
        }

        public void closest_to_zero(string axis, ref double x, ref double y, ref double z)
        {
            x = points.First().X; y = points.First().Y; z = points.First().Z;

            double min_dist;
            if (axis == "X")
            {
                min_dist = Math.Abs(y);

                foreach (var p in points)
                {
                    double dist = Math.Abs(p.Y);
                    if (dist < min_dist)
                    {
                        min_dist = dist;
                        x = p.X; y = p.Y; z = p.Z;
                    }
                }
            }
            if (axis == "Y")
            {
                min_dist = Math.Abs(x);

                foreach (var p in points)
                {
                    double dist = Math.Abs(p.X);
                    if (dist < min_dist)
                    {
                        min_dist = dist;
                        x = p.X; y = p.Y; z = p.Z;
                    }
                }
            }
            if (axis == "Z")
            {
                min_dist = Math.Abs(y);

                foreach (var p in points)
                {
                    double dist = Math.Abs(p.Y);
                    if (dist < min_dist)
                    {
                        min_dist = dist;
                        x = p.X; y = p.Y; z = p.Z;
                    }
                }
            }

        }
		public PointPol normVecOfPlane()
		{
			if (points.Count() > 2)
			{
				PointPol p1 = points[0];
				PointPol p2 = points[1];
				PointPol p3 = points[2];

				double kx, ky, kz, d;
				double xa = p1.X, ya = p1.Y, za = p1.Z;
				double a21 = p2.X - xa, a22 = p2.Y - ya, a23 = p2.Z - za;
				double a31 = p3.X - xa, a32 = p3.Y - ya, a33 = p3.Z - za;

				kx = a22 * a33 - a23 * a32;
				ky = -(a21 * a33 - a23 * a31);
				kz = a21 * a32 - a22 * a31;
				d = (kx * -xa) + (ky * -ya) + (kz * -za);

				return new PointPol(kx, ky, kz);
			}
			else
				return new PointPol(0, 0, 0);
		}


		public void scale(double ind_scale, double a, double b, double c)
        {
            for (int i = 0; i < points.Count(); ++i)
                points[i] = points[i].scale(ind_scale, a, b, c);
            deleteDuplicates();
        }

        public void shift(double a, double b, double c)
        {
            for (int i = 0; i < points.Count(); ++i)
                points[i] = points[i].shift(a, b, c);
            deleteDuplicates();
        }

        public void rotate(Vector axis, double angle, double a, double b, double c)
        {
            for (int i = 0; i < points.Count(); ++i)
                points[i] = points[i].rotate(axis, angle, a, b, c);
            deleteDuplicates();
        }

        public void reflection(string axis)
        {
            for (int i = 0; i < points.Count(); ++i)
                points[i] = points[i].reflection(axis);
        }

        public List<Tuple<Point, Point>> to2d()
        {
            List<Tuple<Point, Point>> l = new List<Tuple<Point, Point>>();
            foreach (var item in edges)
            {
                l.Add(Tuple.Create(points[item.Item1].To2D(), points[item.Item2].To2D()));
            }
            l.Add(Tuple.Create(points[edges.Last().Item2].To2D(), points[edges.First().Item2].To2D()));
            return l;
        }

        public List<Tuple<Point, Point>> to2d(double phi_a, double psi_a)
        {
            List<Tuple<Point, Point>> l = new List<Tuple<Point, Point>>();
            foreach (var item in edges)
            {
                l.Add(Tuple.Create(points[item.Item1].To2D(phi_a, psi_a), 
                    points[item.Item2].To2D(phi_a, psi_a)));
            }
            l.Add(Tuple.Create(points[edges.Last().Item2].To2D(phi_a, psi_a), 
                points[edges.First().Item2].To2D(phi_a, psi_a)));
            return l;
        }
    }

    public class Polyhedron
    {
        double light_loc_x, light_loc_y, light_loc_z;
        public Dictionary<int, List<int>> point_to_pols = 
            new Dictionary<int, List<int>>();
        public List<PointPol> points = new List<PointPol>();
        public List<Polygon> polygons;
		public Dictionary<int, PointPol> point_to_normal = new Dictionary<int, PointPol>();
        public Dictionary<int, double> point_to_diffuse = new Dictionary<int, double>();

        public Polyhedron()
        {
            polygons = new List<Polygon>();
        }

        public Polyhedron(List<Polygon> pols)
        {

        }

        public void updLightLocations(double light_location_x, double light_location_y, double light_location_z)
        {
            light_loc_x = light_location_x;
            light_loc_y = light_location_y;
            light_loc_z = light_location_z;
            updPoints();
        }

        private double scalarProduct(PointPol vec1, PointPol vec2)
        {
            return (vec1.X * vec2.X + vec1.Y * vec2.Y + vec1.Z * vec2.Z);
        }

        private bool pointsEqual(PointPol p1, PointPol p2)
        {
            return (Math.Abs(p1.X - p2.X) < 0.01) &&
                (Math.Abs(p1.Y - p2.Y) < 0.01) &&
                (Math.Abs(p1.Z - p2.Z) < 0.01);
        }

        public void updPoints()
        {
            points.Clear();
            foreach (var pol in polygons)
            {
                for (int i = 0; i < pol.points.Count(); ++i)
                {
                    int indpol = points.FindIndex(p => pointsEqual(p, pol.points[i]));
                    if (indpol == -1)
                        points.Add(pol.points[i]);
                }
            }
            updPointDicts();
        }

        public void updPointDicts()
        {
            point_to_pols.Clear();
            point_to_normal.Clear();
            point_to_diffuse.Clear();
            fillPointToPols();
            fillPointToNormal();
            fillPointToDiffuse();
        }

        public void addPolygon(Polygon pol)
        {
            polygons.Add(pol);

            for (int i = 0; i < pol.points.Count(); ++i)
            {
                int indpol = points.FindIndex(p => pointsEqual(p, pol.points[i]));
                if (indpol == -1)
                    points.Add(pol.points[i]);
            }
			if (polygons.Count() > 5)
			{
                updPointDicts();
			}
        }

		public void clearPolygons()
		{
			polygons.Clear();
			point_to_pols.Clear();
            point_to_normal.Clear();
            point_to_diffuse.Clear();
			points.Clear();
		}

		public void fillPointToPols()
        {
            for (int indpol = 0; indpol < polygons.Count(); ++indpol)
            {
                for (int indppol = 0; indppol < polygons[indpol].points.Count(); ++indppol)
                {
                    int indpoint = points.FindIndex(p => pointsEqual(p, polygons[indpol].points[indppol]));
                    int inddkey = point_to_pols.Keys.ToList().FindIndex(ind => ind == indpoint);
                    if (inddkey == -1)
                    {
						point_to_pols.Add(indpoint, new List<int>());
						point_to_pols[indpoint].Add(indpol);
                    }
                    else
                    {
                        int inddpol = point_to_pols[indpoint].FindIndex(numpol => numpol == indpol);
                        if (inddpol == -1)
                        {
							point_to_pols[indpoint].Add(indpol);
                        }
                    }
                }
            }
        }

		public void fillPointToNormal()
		{
			foreach (var indpoint in point_to_pols.Keys)
			{
				PointPol sum = new PointPol(0, 0, 0);
				foreach (var indpol in point_to_pols[indpoint])
				{
					PointPol curr_norm = polygons[indpol].normVecOfPlane();
					sum.X += curr_norm.X;
					sum.Y += curr_norm.Y;
					sum.Z += curr_norm.Z;
				}
				sum.X /= point_to_pols.Count();
				sum.Y /= point_to_pols.Count();
				sum.Z /= point_to_pols.Count();
				point_to_normal.Add(indpoint, sum.normalize());
			}
		}

        public void fillPointToDiffuse()
        {
            foreach (var indpoint in point_to_normal.Keys)
            {
                double px = points[indpoint].X;
                double py = points[indpoint].Y;
                double pz = points[indpoint].Z;

                //PointPol light_normal = new PointPol(px - light_loc_x, py - light_loc_y, pz - light_loc_z).normalize();
                PointPol light_normal = new PointPol(light_loc_x - px, light_loc_y - py, light_loc_z - pz).normalize();
                PointPol v_norm = point_to_normal[indpoint].normalize();

                double diffuse = Math.Max(scalarProduct(v_norm, light_normal), 0.0)*2;
                point_to_diffuse.Add(indpoint, diffuse);
            }
        }
		
        public void find_center(ref double x, ref double y, ref double z)
        {
            x = 0; y = 0; z = 0;

            double a = 0, b = 0, c = 0;

            foreach (var p in polygons)
            {
                p.find_center(ref a, ref b, ref c);
                x += a;
                y += b;
                z += c;
            }

            x /= polygons.Count();
            y /= polygons.Count();
            z /= polygons.Count();
        }

        public void scale(double ind)
        {
            double a = 0, b = 0, c = 0;
            find_center(ref a, ref b, ref c);
            foreach (var item in polygons)
            {
                item.scale(ind, a, b, c);
            }
            updPoints();
        }

        public void shift(int x, int y, int z)
        {
            foreach (var item in polygons)
            {
                item.shift(x, y, z);
            }
            updPoints();
        }

        public void reflection(string axis)
        {
            foreach (var item in polygons)
            {
                item.reflection(axis);
            }
            updPoints();
        }

        public void rotate(int x1, int y1, int z1,
            int x2, int y2, int z2, double angle)
        {
            PointPol p1 = new PointPol(x1, y1, z1);
            PointPol p2 = new PointPol(x2, y2, z2);
            Vector ed = new Vector(p1, p2);

            double a = 0, b = 0, c = 0;
            find_center(ref a, ref b, ref c);
            foreach (var item in polygons)
            {
                item.rotate(ed, angle, a, b, c);
            }
            updPoints();
        }
    }
}