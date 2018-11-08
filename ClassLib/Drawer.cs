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
        public double Xmin;
        public double Xmax;
        public double Ymin;
        public double Ymax;
        public static byte flEdge = 0;
        public int I2;
        public int J2;
        public static bool visibleShadow = false;
        public Bitmap bitmap;
        public static Body body;
        public double Alf;
        public double Bet;
        public double Alf1;
        public double Bet1;
        public static double Xs = 0; // 1-я точка схода
        public static double Zs = 0; // 2-я точка схода
        public static double[] Sv = { -0.0, -0.0, 1.5, 1 }; // источник света

        public Drawer(int VW, int VH)
        {
            Xmin = -2;
            Xmax = 2;
            Ymin = -2;
            Ymax = 2;
            Alf = 4.31;
            Bet = 4.92;
            Alf1 = 0;
            Bet1 = 0;
            I2 = VW;
            J2 = VH;
        }

        public void Draw()
        {
            I2 = bitmap.Width;
            J2 = bitmap.Height;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Color cl;
                if (flEdge == 2)
                    cl = Color.FromArgb(0, 0, 0);
                else
                    cl = Color.FromArgb(255, 255, 255);
                g.Clear(cl);
                g.SmoothingMode = SmoothingMode.HighQuality;
                if (visibleShadow) // рисование тени
                    Shadow(g);
                DrawBody(g);       // рисование тела
            }
        }

        public void DrawBody(Graphics g)
        {
            int L;
            if ((flEdge == 0) | (flEdge == 2))
            {
                L = body.Vertexs.Length;
                for (int i = 0; i < L; i++)
                {
                    body.VertexsT[i] = Rotate(body.Vertexs[i], 0, Alf1, 0, 0);
                    body.VertexsT[i] = Rotate(body.VertexsT[i], 1, Bet1, 0, 0);
                    body.VertexsT[i] = Rotate(body.VertexsT[i], 3, 0, Xs, Zs);
                    body.VertexsT[i] = Rotate(body.VertexsT[i], 0, Alf, 0, 0);
                    body.VertexsT[i] = Rotate(body.VertexsT[i], 1, Bet, 0, 0);
                }
            }
            DrawFaces(g);       
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
                    Vt = Rotate(Vt, 0, Alf1, 0, 0);
                    Vt = Rotate(Vt, 1, Bet1, 0, 0);
                    if (j <= 2) Vn[j] = Vt;
                    Vt = Rotate(Vt, 3, 0, Xs, Zs);
                    Vt = Rotate(Vt, 0, Alf, 0, 0);
                    Vt = Rotate(Vt, 1, Bet, 0, 0);
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
                P1 = IJ(ToVector(-1 + i * 0.2 / 4, -1, Zh));
                P2 = IJ(ToVector(-1 + i * 0.2 / 4, 1, Zh));
                g.DrawLine(Pens.Silver, P1.X, P1.Y, P2.X, P2.Y);
            }
            for (int i = 0; i < 41; i++)
            {
                P1 = IJ(ToVector(-1, -1 + i * 0.2 / 4, Zh));
                P2 = IJ(ToVector(1, -1 + i * 0.2 / 4, Zh));
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
                    Vt = Rotate(Vt, 0, Alf1, 0, 0);
                    Vt = Rotate(Vt, 1, Bet1, 0, 0);
                    Vt = Rotate(Vt, 3, 0, Xs, Zs);
                    Vt[0] = Sv[0] + (Vt[0] - Sv[0]) * (Zh - Sv[2]) / (Vt[2] - Sv[2]);
                    Vt[1] = Sv[1] + (Vt[1] - Sv[1]) * (Zh - Sv[2]) / (Vt[2] - Sv[2]);
                    Vt[2] = Zh;
                    Vt = Rotate(Vt, 0, Alf, 0, 0);
                    Vt = Rotate(Vt, 1, Bet, 0, 0);
                    w[j].X = II(Vt[0]);
                    w[j].Y = JJ(Vt[1]);
                }
                g.FillPolygon(Brushes.Silver, w);
            }
        }
        /////
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

        public int II(double x)
        {
            return (int)Math.Round((x - Xmin) * I2 / (Xmax - Xmin));
        }

        int JJ(double y)
        {
            return (int)Math.Round((y - Ymax) * J2 / (Ymin - Ymax));
        }

        double XX(int I)
        {
            return Xmin + (Xmax - Xmin) * I / I2;
        }

        double YY(int J)
        {
            return Ymax + (Ymin - Ymax) * J / J2;
        }

        public Point IJ(double[] Vt)
        {
            Point result;
            Vt = Rotate(Vt, 0, Alf, 0, 0);
            Vt = Rotate(Vt, 1, Bet, 0, 0);
            result = new Point(II(Vt[0]), JJ(Vt[1]));
            return result;
        }

        double[] ToVector(double x, double y, double z)
        {
            double[] result = new double[4];
            result[0] = x;
            result[1] = y;
            result[2] = z;
            result[3] = 1;
            return result;
        }

        public double[] VM_Mult(double[] A, double[][] B)
        {
            double[] result = new double[4];
            for (int j = 0; j < 4; j++)
            {
                result[j] = A[0] * B[0][j];
                for (int k = 1; k < 4; k++)
                    result[j] += A[k] * B[k][j];
            }
            if (result[3] != 0)
                for (int j = 0; j < 3; j++)
                    result[j] /= result[3];
            result[3] = 1;
            return result;
        }

        public static double[] Rotate(double[] V, int k, double fi, double p, double r)
        {
            double[][] M = new double[4][];

            for (int i = 0; i < 4; i++)
                M[i] = new double[4];

            for (int i = 0; i < 4; i++)
            {
                M[3][i] = 0;
                M[i][3] = 0;
            }
            M[3][3] = 1;
            switch (k)
            {
                case 0:
                    M[0][0] = 1; M[0][1] = 0; M[0][2] = 0;
                    M[1][0] = 0; M[1][1] = Math.Cos(fi); M[1][2] = Math.Sin(fi);
                    M[2][0] = 0; M[2][1] = -Math.Sin(fi); M[2][2] = Math.Cos(fi);
                    break;
                case 1:
                    M[0][0] = Math.Cos(fi); M[0][1] = 0; M[0][2] = -Math.Sin(fi);
                    M[1][0] = 0; M[1][1] = 1; M[1][2] = 0;
                    M[2][0] = Math.Sin(fi); M[2][1] = 0; M[2][2] = Math.Cos(fi);
                    break;
                case 2:
                    M[0][0] = Math.Cos(fi); M[0][1] = Math.Sin(fi); M[0][2] = 0;
                    M[1][0] = -Math.Sin(fi); M[1][1] = Math.Cos(fi); M[1][2] = 0;
                    M[2][0] = 0; M[2][1] = 0; M[2][2] = 1;
                    break;
                case 3:
                    M[0][0] = 1; M[0][1] = 0; M[0][2] = 0;
                    M[1][0] = 0; M[1][1] = 1; M[1][2] = 0;
                    M[2][0] = 0; M[2][1] = 0; M[2][2] = 1;
                    M[1][3] = p; M[2][3] = r;
                    break;
            }
            
            return VM_Mult(V, M);
        }
    }
}

