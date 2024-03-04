using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Пароплазменное_облако_2
{
    public partial class Form1 : Form
    {
        float[] ro1n, ro2n, Vrn, Urn, pn, Tn, S, rn, pn1, pn2, Vmax, Umax, ro1n1, ro2n1, Vrn1, Urn1, Tn1;
        float cp1 = 140.000000000000f, cp2 = 2100.000000000000f, alpha1 = 35.00000000000f, alpha2 = 2.00000000000f, h = 2200000, pa = 101325.00000000000f, M = 0.0000000400000f, D = 105.000000000000f, dt, t, dr, B, x1, x2, mu1 = 0.207000000000f, mu2 = 0.01800000000f, R = 8.31000000000f, aa, I0 = 1e+6f, A, R0 = 0.0011f, vb;
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        int N, C, C0;
        float kt = 0.0000010000000000f, kr = 0.000036000000000f, dt1, dr1, t1;
        private void button1_Click(object sender, EventArgs e)
        {
            int N1 = 1;
            int N0 = 10000;
            pictureBox1.Width = 500;
            pictureBox1.Height = 500;
            int a = pictureBox1.Width;
            int b = pictureBox1.Height;


            dr = Convert.ToSingle(Math.Sqrt(((a * b) / 3.14f) * ((float)N1 / N0)));
            N = Convert.ToInt32(Math.Round(a / (2 * dr)));
            dr1 = kr * dr;
            int c = Convert.ToInt32(Math.Round(R0 / dr1));
            pn = new float[N];
            pn1 = new float[N];
            pn2 = new float[N];
            Vrn = new float[N];
            Urn = new float[N];
            Tn = new float[N];
            ro1n = new float[N];
            ro2n = new float[N];
            Vrn1 = new float[N];
            Urn1 = new float[N];
            Tn1 = new float[N];
            ro1n1 = new float[N];
            ro2n1 = new float[N];
            S = new float[N];
            Vmax = new float[N];
            Umax = new float[N];
            rn = new float[N];

            for (int i = 0; i <= N - 1; i++)
            {
                rn[i] = (i + 1) * Convert.ToSingle(Math.Round(dr)) * kr;
                S[i] = 4 * 3.14f * rn[i] * rn[i];
                Vrn[i] = 0.00000f;
                Urn[i] = 0.00000f;
                ro1n[i] = 0.0000000f;
                ro2n[i] = 0.0000f;
                pn[i] = pa;
                Tn[i] = 293.000000f;

            }





            timer1.Start();
        }
        private void Usl()
        {
            if (t1 <= 1e-5)
            {
                vb = 220;
                aa = 22.000000e+6f;

            }
            if (t1 <= 5e-5 && t1 > 1e-5)
            {
                vb = 45;
                aa = -4.3750000000e+6f;
            }
            if (t1 <= 50e-5 && t1 > 5e-5)
            {
                vb = 8;
                aa = -82.222200000000e+3f;
            }
            if (t1 <= 120e-5 && t1 > 50e-5)
            {
                vb = 2.5f;
                aa = -7.857000000e+3f;
            }
            if (t1>120e-5)
            {
                aa = 0;
            }



        }
        private void Calc()
        {
            int c = Convert.ToInt32(Math.Round(R0 / dr1));
            if (t1 <= 0.0012)
            {
                Tn[c - 1] = 7000;

            }    
                
            for (int i = 0; i < C - 1; i++)
            {   
                

                if (i == c - 1)
                {
                    x1 = 1;

                }
                else
                {
                    x1 = 0;
                }
                if (i == C - 2)
                {
                    x2 = 1;
                }
                else
                {
                    x2 = 0;
                }
                if (t1 <= 0.0012)
                {
                    A = Convert.ToSingle(I0 * Math.Sin(2616 * t1));
                }
                else
                {
                    A = 0;
                }
                
                B = (D * (alpha1 + alpha2) * (Math.Abs(Tn[C - 1] - Tn[C - 2]) / dr1)) / h;
            }

            for(int i =0; i<C-1;i++)
            {
                Vrn1[i] = -(dt1 / dr1) * (((pn[i + 1] - pn[i]) / ro1n[i])+Vrn[i]*Vrn[i+1])-Vrn[i]*Vrn[i]*((2*dt1/rn[i])-(dt1/dr1))+Vrn[i];
                Urn1[i] = -(dt1 / dr1) * (((pn[i + 1] - pn[i]) / ro2n[i]) + Urn[i] * Urn[i + 1]) - Urn[i] * Urn[i] * ((2 * dt1 / rn[i]) - (dt1 / dr1)) + Urn[i];
                ro1n1[i] = A * x1 * dt1 - (dt1 / dr1) * (ro1n[i + 1] * Vrn[i] + ro1n[i] * Vrn[i + 1]) + 2 * ro1n[i] * Vrn[i] * (dt1 / dr1 - 2 * dt1 / rn[i]) + ro1n[i];
                ro2n1[i] = B * x2 * dt1 - (dt1 / dr1) * (ro2n[i + 1] * Urn[i] + ro2n[i] * Urn[i + 1]) + 2 * ro2n[i] * Urn[i] * (dt1 / dr1 - 2 * dt1 / rn[i]) + ro2n[i];
                pn1[i] = R * Tn[i] * (ro1n[i] / mu1);
                pn2[i] = R * Tn[i] * (ro2n[i] / mu2);
                pn[i] = pn1[i] + pn2[i];
                Vmax[i] = Convert.ToSingle(Math.Sqrt(Math.Abs((2 * pn1[i]) / ro1n[i])));
                Umax[i] = Convert.ToSingle(Math.Sqrt(Math.Abs((2 * pn2[i]) / ro2n[i])));
                if(Math.Abs(Vrn1[i])> Math.Abs(Vmax[i]))
                {
                    Vrn1[i] = Vmax[i];
                }
                if (Math.Abs(Urn1[i]) > Math.Abs(Umax[i]))
                {
                    Urn1[i] = Umax[i];
                }

            }

            for (int i = 1; i < C - 1; i++)
            {
                Tn1[i] = ((alpha1 + alpha2) / (ro1n[i] * cp1 + ro2n[i] * cp2)) * ((2 * dr1 / (rn[i] * dr1)) * (Tn[i + 1] - Tn[i]) + (dt1 / (dr1 * dr1)) * (Tn[i + 1] - 2 * Tn[i] + Tn[i - 1])) - ((ro1n[i] * cp1 * Vrn[i] + ro2n[i] * cp2 * Urn[i]) / (ro1n[i] * cp1 + ro2n[i] * cp2)) * (dt1 / dr1) * (Tn[i + 1] - Tn[i]) + Tn[i];
            }
            
                Tn[C - 1] = 373.000000f;
                pn[C - 1] = (float)(aa * M / S[C - 1]) + pa;
                ro1n[C - 1] = (pn[C - 1] * mu1) / (R * Tn[C - 1]);
                ro2n[C - 1] = (pn[C - 1] * mu2) / (R * Tn[C - 1]);
        }
        private void Art()
        {
            Color color1;
            Graphics gr;
            Bitmap bmp;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bmp);
            gr.Clear(Color.White);
            for (int i = N - 1; i >= 0; i--)
            {
                int a = pictureBox1.Width;
                int b = pictureBox1.Height;
                int x0, y0;
                x0 = a / 2;
                y0 = b / 2;

                Point point = new Point(x0 - Convert.ToInt32((i + 1) * dr), y0 - Convert.ToInt32((i + 1) * dr));
                Size size = new Size(Convert.ToInt32(2 * (i + 1) * dr), Convert.ToInt32(2 * (i + 1) * dr));
                Rectangle rect = new Rectangle(point, size);

                if (radioButton1.Checked == true)
                {
                    color1 = Color.FromArgb(0, 0, 0);
                    if (Vrn[i] >= 360 & Vrn[i] <= 700)
                    {
                        color1 = Color.Red;
                    }
                    if (Vrn[i] >= 300 & Vrn[i] <= 360)
                    {
                        color1 = Color.Orange;
                    }
                    if (Vrn[i] >= 240 & Vrn[i] <= 300)
                    {
                        color1 = Color.Yellow;
                    }
                    if (Vrn[i] >= 180 & Vrn[i] <= 240)
                    {
                        color1 = Color.Green;
                    }
                    if (Vrn[i] >= 120 & Vrn[i] <= 180)
                    {
                        color1 = Color.SkyBlue;
                    }
                    if (Vrn[i] >= 60 & Vrn[i] <= 120)
                    {
                        color1 = Color.Blue;
                    }
                    if (Vrn[i] >= 0.0 & Vrn[i] <= 60)
                    {
                        color1 = Color.DarkBlue;
                    }
                    SolidBrush brush1 = new SolidBrush(color1);

                    gr.FillEllipse(brush1, rect);
                    if (Math.Abs(t1 - 1.000000e-5) <= 1.0e-10)
                    {
                        pictureBox1.Image.Save("D:\\pic1.png");
                    }
                    if (Math.Abs(t1 - 2.500000e-5) <= 1.0e-10)
                    {
                        pictureBox1.Image.Save("D:\\pic7.png");
                    }



                }
                if (radioButton2.Checked == true)
                {
                    color1 = Color.FromArgb(0, 0, 0);
                    if (Urn[i] >= 600 & Urn[i] <= 800)
                    {
                        color1 = Color.Red;
                    }
                    if (Urn[i] >= 500 & Urn[i] <= 600)
                    {
                        color1 = Color.Orange;
                    }
                    if (Urn[i] >= 400 & Urn[i] <= 500)
                    {
                        color1 = Color.Yellow;
                    }
                    if (Urn[i] >= 300 & Urn[i] <= 400)
                    {
                        color1 = Color.Green;
                    }
                    if (Urn[i] >= 200 & Urn[i] <= 300)
                    {
                        color1 = Color.SkyBlue;
                    }
                    if (Urn[i] >= 50 & Urn[i] <= 200)
                    {
                        color1 = Color.Blue;
                    }
                    if (Urn[i] >= 0 & Urn[i] <= 50)
                    {
                        color1 = Color.DarkBlue;
                    }

                    SolidBrush brush1 = new SolidBrush(color1);

                    gr.FillEllipse(brush1, rect);
                    if (Math.Abs(t1 - 1.000000e-5) <= 1.0e-10)
                    {
                        pictureBox1.Image.Save("D:\\pic2.png");
                    }
                    if (Math.Abs(t1 - 2.500000e-5) <= 1.0e-10)
                    {
                        pictureBox1.Image.Save("D:\\pic8.png");
                    }

                }
                if (radioButton3.Checked == true)
                {
                    color1 = Color.FromArgb(0, 0, 0);
                    if (Math.Abs(ro1n[i]) >= 24 & Math.Abs(ro1n[i]) <= 34)
                    {
                        color1 = Color.Red;
                    }
                    if (Math.Abs(ro1n[i]) >= 20 & Math.Abs(ro1n[i]) <= 24)
                    {
                        color1 = Color.Orange;
                    }
                    if (Math.Abs(ro1n[i]) >= 16 & Math.Abs(ro1n[i]) <= 20)
                    {
                        color1 = Color.Yellow;
                    }
                    if (Math.Abs(ro1n[i]) >= 12 & Math.Abs(ro1n[i]) <= 16)
                    {
                        color1 = Color.Green;
                    }
                    if (Math.Abs(ro1n[i]) >= 8 & Math.Abs(ro1n[i]) <= 12)
                    {
                        color1 = Color.SkyBlue;
                    }
                    if (Math.Abs(ro1n[i]) >= 4 & Math.Abs(ro1n[i]) <= 8)
                    {
                        color1 = Color.Blue;
                    }
                    if (Math.Abs(ro1n[i]) >= 0 & Math.Abs(ro1n[i]) <= 4)
                    {
                        color1 = Color.DarkBlue;
                    }
                    SolidBrush brush1 = new SolidBrush(color1);

                    gr.FillEllipse(brush1, rect);
                    if (Math.Abs(t1 - 1.000000e-5) <= 1.0e-10)
                    {
                        pictureBox1.Image.Save("D:\\pic3.png");
                    }
                    if (Math.Abs(t1 - 2.500000e-5) <= 1.0e-10)
                    {
                        pictureBox1.Image.Save("D:\\pic9.png");
                    }


                }
                if (radioButton4.Checked == true)
                {
                    color1 = Color.FromArgb(0, 0, 0);
                    if (Math.Abs(ro2n[i]) >= 11 & Math.Abs(ro2n[i]) <= 16)
                    {
                        color1 = Color.Red;
                    }
                    if (Math.Abs(ro2n[i]) >= 8 & Math.Abs(ro2n[i]) <= 11)
                    {
                        color1 = Color.Orange;
                    }
                    if (Math.Abs(ro2n[i]) >= 5 & Math.Abs(ro2n[i]) <= 8)
                    {
                        color1 = Color.Yellow;
                    }
                    if (Math.Abs(ro2n[i]) >= 2 & Math.Abs(ro2n[i]) <= 5)
                    {
                        color1 = Color.Green;
                    }
                    if (Math.Abs(ro2n[i]) >= 0.6 & Math.Abs(ro2n[i]) <= 2)
                    {
                        color1 = Color.SkyBlue;
                    }
                    if (Math.Abs(ro2n[i]) >= 0.3 & Math.Abs(ro2n[i]) <= 0.6)
                    {
                        color1 = Color.Blue;
                    }
                    if (Math.Abs(ro2n[i]) >= 0 & Math.Abs(ro2n[i]) <= 0.3)
                    {
                        color1 = Color.DarkBlue;
                    }
                    SolidBrush brush1 = new SolidBrush(color1);

                    gr.FillEllipse(brush1, rect);
                    if (Math.Abs(t1 - 1.000000e-5) <= 1.0e-10)
                    {
                        pictureBox1.Image.Save("D:\\pic4.png");
                    }
                    if (Math.Abs(t1 - 2.500000e-5) <= 1.0e-10)
                    {
                        pictureBox1.Image.Save("D:\\pic10.png");
                    }
                }
                if (radioButton5.Checked == true)
                {
                    color1 = Color.FromArgb(0, 0, 0);
                    if (Math.Abs(Tn[i]) >= 5000 & Math.Abs(Tn[i]) <= 20000)
                    {
                        color1 = Color.Red;
                    }
                    if (Math.Abs(Tn[i]) >= 380 & Math.Abs(Tn[i]) <= 5000)
                    {
                        color1 = Color.Orange;
                    }
                    if (Math.Abs(Tn[i]) >= 370 & Math.Abs(Tn[i]) <= 380)
                    {
                        color1 = Color.Yellow;
                    }
                    if (Math.Abs(Tn[i]) >= 365 & Math.Abs(Tn[i]) <= 370)
                    {
                        color1 = Color.Green;
                    }
                    if (Math.Abs(Tn[i]) >= 360 & Math.Abs(Tn[i]) <= 365)
                    {
                        color1 = Color.SkyBlue;
                    }
                    if (Math.Abs(Tn[i]) >= 200 & Math.Abs(Tn[i]) <= 360)
                    {
                        color1 = Color.Blue;
                    }
                    if (Math.Abs(Tn[i]) >= 0 & Math.Abs(Tn[i]) <= 200)
                    {
                        color1 = Color.DarkBlue;
                    }
                    SolidBrush brush1 = new SolidBrush(color1);


                    gr.FillEllipse(brush1, rect);
                    if (Math.Abs(t1 - 1.000000e-5) <= 1.0e-10)
                    {
                        pictureBox1.Image.Save("D:\\pic5.png");
                    }
                    if (Math.Abs(t1 - 2.500000e-5) <= 1.0e-10)
                    {
                        pictureBox1.Image.Save("D:\\pic11.png");
                    }
                }
                if (radioButton6.Checked == true)
                {
                    color1 = Color.FromArgb(0, 0, 0);
                    if (Math.Abs(pn[i]) >= 6000000 & Math.Abs(pn[i]) <= 100000000)

                    {
                        color1 = Color.Red;
                    }
                    if (Math.Abs(pn[i]) >= 5000000 & Math.Abs(pn[i]) <= 6000000)
                    {
                        color1 = Color.Orange;
                    }
                    if (Math.Abs(pn[i]) >= 500000 & Math.Abs(pn[i]) <= 5000000)
                    {
                        color1 = Color.Yellow;
                    }
                    if (Math.Abs(pn[i]) >= 300000 & Math.Abs(pn[i]) <= 500000)
                    {
                        color1 = Color.Green;
                    }
                    if (Math.Abs(pn[i]) >= 200000 & Math.Abs(pn[i]) <= 300000)
                    {
                        color1 = Color.SkyBlue;
                    }
                    if (Math.Abs(pn[i]) >= 100000 & Math.Abs(pn[i]) <= 200000)
                    {
                        color1 = Color.Blue;
                    }
                    if (Math.Abs(pn[i]) >= 0 & Math.Abs(pn[i]) <= 100000)
                    {
                        color1 = Color.DarkBlue;
                    }
                    SolidBrush brush1 = new SolidBrush(color1);

                    gr.FillEllipse(brush1, rect);
                    if (Math.Abs(t1 - 1.000000e-5) <= 1.0e-10)
                    {
                        pictureBox1.Image.Save("D:\\pic6.png");
                    }
                    if (Math.Abs(t1 - 2.500000e-5) <= 1.0e-10)
                    {
                        pictureBox1.Image.Save("D:\\pic12.png");
                    }
                }

            }
            pictureBox1.Image = bmp;

        }
        private void Reclaim()
        {
            for(int i =0; i<C-1;i++)
            {
                Vrn[i] = Vrn1[i];
                Urn[i] = Urn1[i];
                ro1n[i] = ro1n1[i];
                ro2n[i] = ro2n1[i];
                
            }
            for(int i =1;i<C-1;i++)
            {
                Tn[i] = Tn1[i];
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dt = timer1.Interval;
            t += dt;
            dt1 = (kt * dt) / 1000;
            t1 = (t * kt) / 1000;

            textBox1.Text = string.Format("{0:f6}", t1 * 1000000);
            timer1.Enabled = true;
            Usl();
            if (Math.Abs(t1 - 1.01e-5) <= 1.0e-10 || Math.Abs(t1 - 5.01e-5) <= 1e-10 || Math.Abs(t1 - 5.001e-4) <= 1e-10 || Math.Abs(t1 - 1.2001e-3) <= 1e-10)
            {
                C0 = C - Convert.ToInt32(Math.Round(((vb * t1) / dr1)));
            }
            C = C0 + Convert.ToInt32(Math.Ceiling(((vb * t1) / dr1)));
            Calc();
            Art();
            Reclaim();

        }

        public Form1()
        {
            InitializeComponent();
        }
    }
}
