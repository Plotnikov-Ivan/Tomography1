using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Tomography1
{
    public partial class Form1 : Form
    {

        Bitmap bmp = new Bitmap(800, 800);
        Graphics g;
        int xi, yi, xd, yd;
        double phi, cos, sin;
        Pen pencil = new Pen(Color.Blue, 2);

        private void button1_Click(object sender, EventArgs e)
        {
            DrawCircle();
            int R = 300;
            int m = Convert.ToInt32(textBox2.Text);
            int N = Convert.ToInt32(textBox4.Text);
            if (radioButton1.Checked)
            {
                int h = Convert.ToInt32(textBox3.Text);
                Draw_Parall(R, m, h, N);
            }
            else if (radioButton2.Checked)
            {
                double alpha = Convert.ToInt32(textBox3.Text);
                Draw_Fan(R, m, alpha, N);
            }

            pictureBox1.Image = bmp;
        }

        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrawCircle();
        }

        private void DrawCircle()
        {
            g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            g.DrawEllipse(Pens.Black, 100, 100, 600, 600);
            g.DrawLine(Pens.Black, 50, 400, 750, 400);
            g.DrawLine(Pens.Black, 400, 50, 400, 750);
            g.FillEllipse(Brushes.Black, 395, 395, 10, 10);
            pictureBox1.Image = bmp;
        }



        private void Draw_Fan(int R, int m, double alpha, int N)
        {
            double alpha_cur;
            for (int n = 0; n < N; n++)
            {
                xi = 400; yi = 400;
                phi = (Math.PI * n * 2) / N;
                sin = Math.Sin(phi);
                cos = Math.Cos(phi);
                xi += Convert.ToInt32(-R * cos);
                yi += Convert.ToInt32(-R * sin);
                for (int i = -m; i <= m; i++)
                {
                    alpha_cur = (alpha / m) * i * Math.PI / 180;
                    xd = 400; yd = 400;
                    xd += Convert.ToInt32(R * cos - 2 * R * sin * Math.Tan(alpha_cur));
                    yd += Convert.ToInt32(R * sin + 2 * R * cos * Math.Tan(alpha_cur));
                    g.DrawLine(pencil, xi, yi, xd, yd);
                }
            }
        }



        private void Draw_Parall(int R, int m, int h, int N)
        {
            for (int n = 0; n < N; n++)
            {
                phi = (Math.PI * n) / N;
                sin = Math.Sin(phi);
                cos = Math.Cos(phi);
                for (int i = -m; i <= m; i++)
                {
                    xi = 400; yi = 400; xd = 400; yd = 400;
                    xi += Convert.ToInt32(-R * cos - i * h * sin);
                    yi += Convert.ToInt32(-R * sin + i * h * cos);
                    xd += Convert.ToInt32(R * cos - i * h * sin);
                    yd += Convert.ToInt32(R * sin + i * h * cos);
                    g.DrawLine(pencil, xi, yi, xd, yd);
                }
            }
        }

    }
}
