using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Penrose_Tiling
{
    public partial class Form1 : Form
    {
        double angle = 0;
        static double A = Math.PI / 5;

        public Form1()
        {
            InitializeComponent();
        }

        private void rotateRight(double x)
        {
            angle = angle + x;
        }

        private void rotateLeft(double x)
        {
            angle = angle - x;
        }

        private void Line(double x, double y, double x1, double y1)
        {
            Graphics fractal = panel1.CreateGraphics();
            fractal.DrawLine(new Pen(Color.Black, 2), (float)x, (float)y, (float)x1, (float)y1);
        }

        private void Triangle(double x1, double y1, double x2, double y2, double x3, double y3, SolidBrush brush)
        {
            Graphics fractal = panel1.CreateGraphics();

            //SolidBrush brush = new SolidBrush(Color.MediumBlue);

            fractal.FillPolygon(brush, new PointF[3] { new PointF((float)x1, (float)y1), new PointF((float)x2, (float)y2), new PointF((float)x3, (float)y3) });
        }

        private void hk(int n, double x, double y, double length,int rev)
        {
            double x1 = x, y1 = y;
            double l2 = length * (float)Math.Sqrt(2 * (1 - Math.Cos(A)));

            Line(x, y, x1 =  x + length * Math.Cos(angle),y1 = y + length * Math.Sin(angle));
            rotateLeft(rev * A);
            Line(x, y, x + length * Math.Cos(angle), y + length * Math.Sin(angle));
            Triangle(x, y, x1, y1, x + length * Math.Cos(angle), y + length * Math.Sin(angle), new SolidBrush(Color.DarkRed));
            
            Line(x1, y1, x + length * Math.Cos(angle), y + length * Math.Sin(angle));
            rotateRight(rev * A);
            
            if (n != 0)
            {
                hd(n - 1, x, y, length - (l2 * l2 / length), rev);
                rotateLeft(rev * A);
                x1 = x + length * Math.Cos(angle);
                y1 = y + length * Math.Sin(angle);
                rotateRight(rev * 4 * A);
                hk(n - 1, x1, y1, l2, rev);
                hk(n - 1, x1, y1, l2, -rev);
                rotateLeft(rev * 3 * A);
            }
        }

        private void hd(int n, double x, double y, double length, int rev)
        {
            double x1 = x, y1 = y;
            double l2 = length * (float)Math.Sqrt(2 * (1 - Math.Cos(A)));
            
            Line(x, y, x1 = x + length * Math.Cos(angle), y1 = y + length * Math.Sin(angle));
            rotateLeft(rev * A);
            Line(x, y, x + l2 * Math.Cos(angle), y + l2 * Math.Sin(angle));
            Triangle(x, y, x1, y1, x + l2 * Math.Cos(angle), y + l2 * Math.Sin(angle), new SolidBrush(Color.DarkBlue));
            Line(x1, y1, x + l2 * Math.Cos(angle), y + l2 * Math.Sin(angle));

            rotateRight(rev * A);

            if (n != 0)
            {
                rotateLeft(rev * A);
                hk(n - 1, x, y, l2, -rev);
                rotateRight(rev * A);
                x1 = x + length * Math.Cos(angle);
                y1 = y + length * Math.Sin(angle);
                rotateLeft(rev * 4 * A);
                hd(n - 1, x1, y1, l2, rev);
                rotateRight(rev * 4 * A);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = 0;

            n = Convert.ToInt32(trackBar1.Value);

            Graphics fractal = panel1.CreateGraphics();
            fractal.Clear(Color.White);
            angle = 0;

            for (int i = 0; i < 5; i++)
            {
                if (radioButton2.Checked)
                {
                    hd(n, panel1.Width / 2, panel1.Height / 2, 3 * panel1.Width / 7, 1);
                    hd(n, panel1.Width / 2, panel1.Height / 2, 3 * panel1.Width / 7, -1);
                }
                else if(radioButton1.Checked)
                {
                    hk(n, panel1.Width / 2, panel1.Height / 2, 3 * panel1.Width / 7, 1);
                    hk(n, panel1.Width / 2, panel1.Height / 2, 3 * panel1.Width / 7, -1);
                }
                rotateRight(2 * A);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
