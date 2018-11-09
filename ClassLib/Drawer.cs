using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ClassLib
{
    public class Drawer
    {
        public static double[] Sv = { -0.0, -0.0, 1.5, 1 }; // источник света
        public Bitmap bitmap { get; set; }
        public SolidBrush myBrush { get; set; }
        public static Body body { get; set; }
        public static Body body0 { get; set; }

        public Drawer()
        {
            bitmap = new Bitmap(Screen.Size.Width, Screen.Size.Height);
            myBrush = new SolidBrush(Color.White);
            body = new Body(0);
            body0 = new Body(1);
        }

        int II(double x)
        {
            return (int)Math.Round((x - Screen.Xmin) * Screen.Size.Width / (Screen.Xmax - Screen.Xmin));
        }

        int JJ(double y)
        {
            return (int)Math.Round((y - Screen.Ymax) * Screen.Size.Height / (Screen.Ymin - Screen.Ymax));
        }
        

        Point IJ(double[] Vt)
        {
            Point result;
            Vt = Matrix.Rotate(Vt, 0, Camera.Alf, 0, 0);
            Vt = Matrix.Rotate(Vt, 1, Camera.Bet, 0, 0);
            result = new Point(II(Vt[0]), JJ(Vt[1]));
            return result;
        }

        double[] Norm(double[] V1, double[] V2, double[] V3)
        {
            double[] Result = new double[4];
            double[] A = new double[4];
            double[] B = new double[4];
            A[0] = V2[0] - V1[0]; A[1] = V2[1] - V1[1]; A[2] = V2[2] - V1[2];
            B[0] = V3[0] - V1[0]; B[1] = V3[1] - V1[1]; B[2] = V3[2] - V1[2];
            double u = A[1] * B[2] - A[2] * B[1];
            double v = -A[0] * B[2] + A[2] * B[0];
            double w = A[0] * B[1] - A[1] * B[0];

            double d = Math.Sqrt(u * u + v * v + w * w);
            if (d != 0)
            {
                Result[0] = u / d;
                Result[1] = v / d;
                Result[2] = w / d;
            }
            else
            {
                Result[0] = 0;
                Result[1] = 0;
                Result[2] = 0;
            }
            return Result;
        }

        void DrawFaces(Graphics g)
        {
            int L1 = body.Faces.Length;
            int L0 = body.Faces[0].p.Length;
            Point[] w = new Point[L0];

            double[][] Vn = new double[3][];
            double[][] Wn = new double[3][];
            for (int i = 0; i < L1; i++)
            {
                for (int j = 0; j < L0; j++)
                {
                    double[] Vt = body.Vertexs[body.Faces[i].p[j]];                  
                    Vt = Matrix.Rotate(Vt, 0, Camera.Alf1, 0, 0);
                    Vt = Matrix.Rotate(Vt, 1, Camera.Bet1, 0, 0);
                    if (j <= 2) Vn[j] = Vt;
                    Vt = Matrix.Rotate(Vt, 3, 0, 0, 0);
                    Vt = Matrix.Rotate(Vt, 0, Camera.Alf, 0, 0);
                    Vt = Matrix.Rotate(Vt, 1, Camera.Bet, 0, 0);
                    w[j].X = II(Vt[0]);
                    w[j].Y = JJ(Vt[1]);
                    if (j <= 2) Wn[j] = Vt;
                }
                body.Faces[i].N = Norm(Vn[0], Vn[1], Vn[2]);
                double[] NN = Norm(Wn[0], Wn[1], Wn[2]);
                double d = Math.Abs(NN[2]);
                Color col = Color.FromArgb(0, 0, (byte)(Math.Round(255 * d)));
                SolidBrush br = new SolidBrush(col);
                if (NN[2] < 0)
                    g.FillPolygon(br, w);
            }
        }

        public void DrawBody(Graphics g)
        {
            DrawFaces(g);
        }

        private void Shadow(Graphics g)
        {
            Point P = IJ(Sv);
            g.FillRectangle(Brushes.Red, P.X - 2, P.Y - 2, 5, 5);
            g.DrawLine(Pens.Red, P.X - 4, P.Y, P.X + 4, P.Y);
            g.DrawLine(Pens.Red, P.X, P.Y - 4, P.X, P.Y + 5);

            Point P1, P2;
            double Zh = -1;
            for (int i = 0; i < 41; i++)
            {
                P1 = IJ(Vector.ToVector(-1 + i * 0.2 / 4, -1, Zh));
                P2 = IJ(Vector.ToVector(-1 + i * 0.2 / 4, 1, Zh));
                g.DrawLine(Pens.Silver, P1.X, P1.Y, P2.X, P2.Y);
            }
            for (int i = 0; i < 41; i++)
            {
                P1 = IJ(Vector.ToVector(-1, -1 + i * 0.2 / 4, Zh));
                P2 = IJ(Vector.ToVector(1, -1 + i * 0.2 / 4, Zh));
                g.DrawLine(Pens.Silver, P1.X, P1.Y, P2.X, P2.Y);
            }

            int L1 = body.Faces.Length;
            int L0 = body.Faces[0].p.Length;
            Point[] w = new Point[L0];

            for (int i = 0; i < L1; i++)
            {
                for (int j = 0; j < L0; j++)
                {
                    double[] Vt = body.Vertexs[body.Faces[i].p[j]];                  
                    Vt = Matrix.Rotate(Vt, 0, Camera.Alf1, 0, 0);
                    Vt = Matrix.Rotate(Vt, 1, Camera.Bet1, 0, 0);
                    Vt = Matrix.Rotate(Vt, 3, 0, 0, 0);
                    Vt[0] = Sv[0] + (Vt[0] - Sv[0]) * (Zh - Sv[2]) / (Vt[2] - Sv[2]);
                    Vt[1] = Sv[1] + (Vt[1] - Sv[1]) * (Zh - Sv[2]) / (Vt[2] - Sv[2]);
                    Vt[2] = Zh;
                    Vt = Matrix.Rotate(Vt, 0, Camera.Alf, 0, 0);
                    Vt = Matrix.Rotate(Vt, 1, Camera.Bet, 0, 0);
                    w[j].X = II(Vt[0]);
                    w[j].Y = JJ(Vt[1]);
                }
                g.FillPolygon(Brushes.Silver, w);
            }
        }

        public void Draw()
        {
           // I2 = bitmap.Width;
            //J2 = bitmap.Height;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Color cl = Color.FromArgb(255, 255, 255);
                g.Clear(cl);
                g.SmoothingMode = SmoothingMode.HighQuality;
                Shadow(g);
                DrawBody(g);       // рисование тела
            }
        }
    }
}
