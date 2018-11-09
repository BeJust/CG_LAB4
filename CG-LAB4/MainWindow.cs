using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLib;

namespace CG_LAB4
{
    public partial class MainWindow : Form
    {
        ClassLib.Screen screen;
        Camera cam;
        static Drawer drawer;
        bool drawing = false;
        static Graphics g;
        public static bool flMove = false;
        MouseEventArgs e0;
            

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Drawer.Sv[0] = hScrollBar1.Value / 30d;
            MyDraw();
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            Drawer.Sv[1] = hScrollBar2.Value / 30d;
            MyDraw();
            
        }

        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            Drawer.Sv[2] = 1.5 + hScrollBar3.Value / 100d;
            MyDraw();
        }

        private void tetr_rb_CheckedChanged(object sender, EventArgs e)
        {
            byte flBody = Convert.ToByte((sender as RadioButton).Tag);
            Drawer.body = new Body(flBody);
            MainWindow.MyDraw();
        }

        public static void MyDraw()
        {
            drawer.Draw();
            
            g.DrawImage(drawer.bitmap, Program.mainWindow.ClientRectangle);
            
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            MouseWheel += new MouseEventHandler(FormMain_MouseWheel);
            g = CreateGraphics();
            screen = new ClassLib.Screen(Size, -2, 2, -2, 2);
            cam = new Camera();
            drawer = new Drawer();
            
            
        }

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            drawing = true;
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                if (e.Button == MouseButtons.Left)
                {
                    double x = e.X - ClientRectangle.Width / 2;
                    double y = e.Y - ClientRectangle.Height / 2;
                    cam.SetAngle(x, y);
                }
                else
                {
                    e0 = e;
                }
                MyDraw();
            }
        }

        private void MainWindow_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
        }

        void FormMain_MouseWheel(object sender, MouseEventArgs e)
        {
            cam.ChangeWindowXY(e.X, e.Y, e.Delta);
            MyDraw();
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            MyDraw();
        }

        private void body_rb_CheckedChanged(object sender, EventArgs e)
        {
            cam.FlRotate = false;
            MainWindow.MyDraw();
        }

        private void axis_rb_CheckedChanged(object sender, EventArgs e)
        {
            cam.FlRotate = true;
            MainWindow.MyDraw();
        }
    }
}
