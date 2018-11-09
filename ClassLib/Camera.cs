using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public class Camera
    {
        public static double Alf { get; set; }
        public static double Bet { get; set; }
        public static double Alf1 { get; set; }
        public static double Bet1 { get; set; }
        public bool FlRotate { get; set; } //вращение тела или осей(в зависимости от значения)
        

        public Camera()
        {
            Alf = 4.31;
            Bet = 4.92;
            Alf1 = 0;
            Bet1 = 0;
            FlRotate = false;
        }

        private double XX(int I)
        {
            return Screen.Xmin + (Screen.Xmax - Screen.Xmin) * I / Screen.Size.Width;
        }

        private double YY(int J)
        {
            return Screen.Ymax + (Screen.Ymin - Screen.Ymax) * J / Screen.Size.Height;
        }

        public void ChangeWindowXY(int u, int v, int Delta)
        {
            
            double coeff;
            double x = XX(u);
            double y = YY(v);
            if (Delta < 0)
                coeff = 1.03;
            else
                coeff = 0.97;
            Screen.Xmin = x - (x - Screen.Xmin) * coeff;
            Screen.Xmax = x + (Screen.Xmax - x) * coeff;
            Screen.Ymin = y - (y - Screen.Ymin) * coeff;
            Screen.Ymax = y + (Screen.Ymax - y) * coeff;
        }

        public void SetAngle(double x, double y)
        {
            if (FlRotate)
            {
                Alf = Math.Atan2(y, x);
                Bet = Math.Sqrt((x / 10) * (x / 10) + (y / 10) * (y / 10));
            }
            else
            {
                Alf1 = Math.Atan2(y, x);
                Bet1 = Math.Sqrt((x / 10) * (x / 10) + (y / 10) * (y / 10));
            }
        }
    }
}
