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
        
        public static Drawer drawer;
        static Graphics g;
        bool drawing = false;
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
            MyDraw();
        }

        public static void MyDraw()
        {
            
            drawer.Draw();
            g.DrawImage(drawer.bitmap, ClientRectangle);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            MouseWheel += new MouseEventHandler(FormMain_MouseWheel);
            drawer = new Drawer(ClientRectangle.Width, ClientRectangle.Height);
            g = CreateGraphics();
        }

        private void dodec_rb_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
