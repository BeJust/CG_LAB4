using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ClassLib
{
    public class Screen
    {
        public static Size Size { get; private set; }
        public static double Xmin { get; set; }
        public static double Xmax { get; set; }
        public static double Ymin { get; set; }
        public static double Ymax { get; set; }


        public Screen(Size sz, double xMin, double xMax, double yMin, double yMax)
        {
            Size = sz; //I2 - width J2 - height
            Xmin = xMin;
            Xmax = xMax;
            Ymin = yMin;
            Ymax = yMax;
        }

        

    }
}
