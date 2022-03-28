using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week01_1
{
    public partial class Form1 : Form
    {
        public ArrayList ar;

        public Form1()
        {
            InitializeComponent();
            ar = new ArrayList();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point c = new Point(e.X, e.Y);
                ar.Add(c);
            }
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Point c in ar)
            {
                //e.Graphics.DrawEllipse(new Pen(Color.Black), c.X, c.Y, 20, 20);
                //e.Graphics.FillEllipse(new SolidBrush(Color.Red), c.X, c.Y, 20, 20);
                e.Graphics.DrawEllipse(Pens.Red, c.X, c.Y, 20, 20);
                e.Graphics.FillEllipse(Brushes.Red, c.X, c.Y, 20, 20);
                //e.Graphics.DrawEllipse(new Pen(Color.Red, 3), c.X, c.Y, 20, 20);

            }
        }
    }
}
