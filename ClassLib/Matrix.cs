using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public struct Matrix
    {
        static double[] VM_Mult(double[] A, double[][] B)
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
