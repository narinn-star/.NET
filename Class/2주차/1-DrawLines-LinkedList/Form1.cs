using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace DrawLines
{
    public partial class Form1 : Form
    {
        private int x, y;
        private LinkedList<ArrayList> total_lines;
        private ArrayList ar;

        public Form1()
        {
            total_lines = new LinkedList<ArrayList>();
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
                ar = new ArrayList();
                ar.Add(new Point(x, y));
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics G = CreateGraphics();
                G.DrawLine(Pens.Black, x, y, e.X, e.Y);
                x = e.X;
                y = e.Y;
                ar.Add(new Point(x, y));
                G.Dispose();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            total_lines.AddLast(ar);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (ArrayList line in total_lines)
            {
                for (int i = 1; i < line.Count; i++)
                {
                    e.Graphics.DrawLine(Pens.Black, (Point)line[i - 1], (Point)line[i]);
                }
            }
        }
    }
}
