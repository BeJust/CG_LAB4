using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public class Matrix
    {
        private float[] matrix;

        private Matrix(float[,] m)
        {
            matrix = new float[16];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    matrix[i * 4 + j] = m[i, j];
        }
        public float this[int r, int c]
        {
            get { return matrix[r * 4 + c]; }
            set { matrix[r * 4 + c] = value; }
        }

        public static Matrix operator *(Matrix matrix, float number)
        {
            var m = Zero();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    m[i, j] = matrix[i, j] * number;
            return m;
        }
        public static Matrix operator *(Matrix first, Matrix second)
        {
            var m = Zero();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 4; k++)
                        m[i, j] += first[i, k] * second[k, j];
            return m;
        }

        public static Matrix Zero()
        {
            var m = new float[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            return new Matrix(m);
        }

        public static Matrix One()
        {
            var m = Zero();
            for (int i = 0; i < 4; i++)
                m[i, i] = 1;
            return m;
        }

        public static Matrix Create(float[,] matrix)
        {
            return new Matrix(matrix);
        }

        public static Matrix CreateRotationMatrix(int axis, float angle)
        {
            var m = Matrix.One();
            int a1 = (axis + 1) % 3;
            int a2 = (axis + 2) % 3;

            m[a1, a1] = (float)System.Math.Cos(angle);
            m[a1, a2] = (float)-System.Math.Sin(angle);
            m[a2, a1] = (float)System.Math.Sin(angle);
            m[a2, a2] = (float)System.Math.Cos(angle);

            return m;
        }

        
    }
}
