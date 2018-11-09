using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public struct Matrix
    {
        private double[] matrix;
        private Matrix(double[,] m)
        {
            matrix = new double[16];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    matrix[i * 4 + j] = m[i, j];
        }

        public double this[int r, int c]
        {
            get { return matrix[r * 4 + c]; }
            set { matrix[r * 4 + c] = value; }
        }

        public static Matrix Zero()
        {
            var m = new double[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            return new Matrix(m);
        }

        public static Matrix One()
        {
            var m = Zero();
            for (int i = 0; i < 4; i++)
                m[i, i] = 1;
            return m;
        }

        public static Vector operator *(Vector vector, Matrix matrix)
        {
            var v = Vector.Zero();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    v[i] += matrix[i, j] * vector[j];
            return v;
        }

        public static Vector Rotate(Vector V, int k, double fi, double p, double r)
        {
            Matrix M = One();
            switch (k)
            {
                case 0:
                    M[0,0] = 1; M[0,1] = 0; M[0,2] = 0;
                    M[1,0] = 0; M[1,1] = Math.Cos(fi); M[1,2] = Math.Sin(fi);
                    M[2,0] = 0; M[2,1] = -Math.Sin(fi); M[2,2] = Math.Cos(fi);
                    break;
                case 1:
                    M[0,0] = Math.Cos(fi); M[0,1] = 0; M[0,2] = -Math.Sin(fi);
                    M[1,0] = 0; M[1,1] = 1; M[1,2] = 0;
                    M[2,0] = Math.Sin(fi); M[2,1] = 0; M[2,2] = Math.Cos(fi);
                    break;
                case 2:
                    M[0,0] = Math.Cos(fi); M[0,1] = Math.Sin(fi); M[0,2] = 0;
                    M[1,0] = -Math.Sin(fi); M[1,1] = Math.Cos(fi); M[1,2] = 0;
                    M[2,0] = 0; M[2,1] = 0; M[2,2] = 1;
                    break;
                case 3:
                    M[0,0] = 1; M[0,1] = 0; M[0,2] = 0;
                    M[1,0] = 0; M[1,1] = 1; M[1,2] = 0;
                    M[2,0] = 0; M[2,1] = 0; M[2,2] = 1;
                    M[1,3] = p; M[2,3] = r;
                    break;
            }
            return V * M;
  
        }
    }
}
