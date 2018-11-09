using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public struct Vector
    {
        private double[] values;
        public Vector(double x, double y, double z)
        {
            values = new double[4];
            values[0] = x; values[1] = y; values[2] = z; values[3] = 1;
        }
        public Vector(double x, double y, double z, double w)
        {
            values = new double[4];
            values[0] = x; values[1] = y; values[2] = z; values[3] = w;
        }
        public double X { get { return values[0]; } set { values[0] = value; } }
        public double Y { get { return values[1]; } set { values[1] = value; } }
        public double Z { get { return values[2]; } set { values[2] = value; } }
        public double W { get { return values[3]; } set { values[3] = value; } }

        public double this[int idx]
        {
            get { return values[idx]; }
            set { values[idx] = value; }
        }

        public static Vector Zero()
        {
            Vector result = new Vector(0, 0, 0);
            result.W = 0;
            return result;
        }

        
    }
}
