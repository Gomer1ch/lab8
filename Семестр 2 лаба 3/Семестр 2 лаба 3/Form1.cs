using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Семестр_2_лаба_3
{
    public partial class Form1 : Form
    {
        class House
        {
            public int X = 0;
            public int Y = 0;
            public float K = 1;
            public int Angle = 0;
            public House(int x, int y, float k, int angle)
            {
                X = x;
                Y = y;
                K = k;
                Angle = angle;
            }
            //задаём координаты и размер дома
        }
        Bitmap bmp;
        Graphics graph;
        Pen pen;
        House house;
        int quantity = 0;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graph = Graphics.FromImage(bmp);
            pen = new Pen(Color.Black);
            house = new House(pictureBox1.Width / 2, pictureBox1.Height / 2, 1, 0);
        }
        void DrawHouse()
        {
            //graph.Clear(pictureBox1.BackColor);
            graph.DrawRectangle(pen, house.X, house.Y, 100 * house.K, 100 * house.K);
            graph.DrawEllipse(pen, house.X + 35 * house.K, house.Y + 10 * house.K, 30 * house.K, 30 * house.K);
            graph.DrawLine(pen, house.X + 50 * house.K, house.Y + 10 * house.K, house.X + 50 * house.K, house.Y + 40 * house.K);
            graph.DrawLine(pen, house.X + 35 * house.K, house.Y + 25 * house.K, house.X + 65 * house.K, house.Y + 25 * house.K);
            Point point1 = new Point(Convert.ToInt32(house.X - 5 * house.K), Convert.ToInt32(house.Y + 3 * house.K));
            Point point2 = new Point(Convert.ToInt32(house.X + 30 * house.K), Convert.ToInt32(house.Y - 20 * house.K));
            Point point3 = new Point(Convert.ToInt32(house.X + 50 * house.K), Convert.ToInt32(house.Y - 50 * house.K));
            Point[] pts = new Point[3] { point1, point2, point3 };
            graph.DrawCurve(pen, pts, 1);
            Point point4 = new Point(Convert.ToInt32(house.X + 105 * house.K), Convert.ToInt32(house.Y + 3 * house.K));
            Point point5 = new Point(Convert.ToInt32(house.X + 70 * house.K), Convert.ToInt32(house.Y - 20 * house.K));
            Point[] pm = new Point[3] { point4, point5, point3 };
            graph.DrawCurve(pen, pm, 1);
            //Point point11 = new Point(Convert.ToInt32(house.X + 30 * house.K), Convert.ToInt32(house.Y + 100 * house.K));
            //Point point12 = new Point(Convert.ToInt32(house.X + 35 * house.K), Convert.ToInt32(house.Y + 65 * house.K));
            //Point point13 = new Point(Convert.ToInt32(house.X + 65 * house.K), Convert.ToInt32(house.Y + 65 * house.K));
            //Point point14 = new Point(Convert.ToInt32(house.X + 70 * house.K), Convert.ToInt32(house.Y + 100 * house.K));
            //Point[] pts2 = new Point[4] { point11, point12, point13, point14 };
            //graph.DrawCurve(pen, pts2, 3);
            Point start = new Point(Convert.ToInt32(house.X + 30 * house.K), Convert.ToInt32(house.Y + 100 * house.K));
            Point control1 = new Point(Convert.ToInt32(house.X + 30 * house.K), Convert.ToInt32(house.Y + 50 * house.K));
            Point control2 = new Point(Convert.ToInt32(house.X + 70 * house.K), Convert.ToInt32(house.Y + 50 * house.K));
            Point end = new Point(Convert.ToInt32(house.X + 70 * house.K), Convert.ToInt32(house.Y + 100 * house.K));
            Point[] bezierPoints = {start, control1, control2, end};
            graph.DrawBeziers(pen, bezierPoints);
            pictureBox1.Image = bmp;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            graph.Clear(pictureBox1.BackColor);
            DrawHouse();
        }

        private void KeyPress1(object sender, KeyPressEventArgs e)
        {
            label1.Text = "" + e.KeyChar;
            if (e.KeyChar == 'd')
            {
                house.X += 10;
                graph.Clear(pictureBox1.BackColor);
                DrawHouse();
            }
            if (e.KeyChar == 'w')
            {
                house.Y -= 10;
                graph.Clear(pictureBox1.BackColor);
                DrawHouse();
            }
            if (e.KeyChar == 's')
            {
                house.Y += 10;
                graph.Clear(pictureBox1.BackColor);
                DrawHouse();
            }
            if (e.KeyChar == 'a')
            {
                house.X -= 10;
                graph.Clear(pictureBox1.BackColor);
                DrawHouse();
            }

            char number = e.KeyChar;
            if (!(Char.IsDigit(number) || number == 8 || number == 44))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                house.K = Convert.ToSingle(textBox1.Text);
                quantity = Convert.ToInt32(textBox2.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float t = house.K;
            int n = 0;
            if (radioButton1.Checked) n = 1;
            if (radioButton2.Checked) n = 2;
            int x0 = Convert.ToInt32(pictureBox1.Width) - 110;
            int y0 = Convert.ToInt32(pictureBox1.Height) - 110;
            graph.Clear(pictureBox1.BackColor);
            switch (n)
            {
                case 1:
                    {
                        //прямая y=x
                        for (int i = 0; i < quantity; i++)
                        {
                            house = new House(x0, y0, house.K, 0);
                            DrawHouse();
                            if (checkBox1.Checked)
                            {
                                house.K *= 0.7F;
                            }
                            if (checkBox2.Checked)
                            {
                                house.K /= 0.7F;
                            }
                            x0 -= Convert.ToInt32(100 * house.K);
                            y0 -= Convert.ToInt32(100 * house.K);
                            house = new House(pictureBox1.Width / 2, pictureBox1.Height / 2, house.K, 0);
                        }
                        house.K = t;
                        
                    }
                    break;
                case 2:
                    {
                        //парабола y=b+k(x-a)^2
                        x0 = 0;
                        for (int i = 0; i < quantity; i++)
                        {
                            y0 = 50 + (x0 - Convert.ToInt32(pictureBox1.Width) / 2) * (x0 - Convert.ToInt32(pictureBox1.Width) / 2) / 500;
                            house = new House(x0, y0, house.K, 0);
                            DrawHouse();
                            if (checkBox1.Checked)
                            {
                                house.K *= 0.8F;
                            }
                            if (checkBox2.Checked)
                            {
                                house.K /= 0.8F;
                            }
                            x0 = Convert.ToInt32(x0 + 100*house.K);
                            house = new House(pictureBox1.Width / 2, pictureBox1.Height / 2, house.K, 0);
                        }
                        house.K = t;
                    }
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            graph.Clear(pictureBox1.BackColor);
            pictureBox1.Image = bmp;
            house = new House(pictureBox1.Width / 2, pictureBox1.Height / 2, house.K, 0);
        }
    }
}