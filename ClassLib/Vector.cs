using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public struct Vector
    {
        public static double[] ToVector(double x, double y, double z)
        {
            double[] result = new double[4];
            result[0] = x;
            result[1] = y;
            result[2] = z;
            result[3] = 1;
            return result;
        }


    }
}
