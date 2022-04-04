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

namespace Week03_0
{
    public partial class Form1 : Form
    {
        private LinkedList<CMyData> total_lines;
        private CMyData mydata;
        private Point p;

        private Color CurrentColor;
        public Form1()
        {
            CurrentColor = Color.Red;
            total_lines = new LinkedList<CMyData>();
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mydata = new CMyData();
                mydata.Color = CurrentColor;
                p = new Point(e.X, e.Y);
                mydata.AR.Add(p);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics G = CreateGraphics();
                G.DrawLine(new Pen(mydata.Color, mydata.Width), p.X, p.Y, e.X, e.Y);
                p = new Point(e.X, e.Y);
                mydata.AR.Add(p);
                G.Dispose();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            total_lines.AddLast(mydata);
            mydata = new CMyData();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData line in total_lines)
            {
                Pen p = new Pen(line.Color, 1);
                for (int i = 1; i < line.AR.Count; i++)
                {
                    e.Graphics.DrawLine(p, (Point)line.AR[i - 1], (Point)line.AR[i]);
                }
            }
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Red;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Green;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Blue;
        }

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2();
            dlg.Color = CurrentColor;
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                CurrentColor = dlg.Color;
            }
            dlg.Dispose();
        }

        private void menuStrip1_MenuActivate(object sender, EventArgs e)
        {
            redToolStripMenuItem.Checked = (CurrentColor == Color.Red);
            greenToolStripMenuItem.Checked = (CurrentColor == Color.Green);
            blueToolStripMenuItem.Checked = (CurrentColor == Color.Blue);
        }
    }
    public class CMyData
    {
        private Color color;//펜컬러
        private int width;//크기
        private int shape;
        private ArrayList Ar;

        public CMyData()  //생성자함수
        {
            Ar = new ArrayList();
        }
        public Color Color//펜컬러
        {
            get { return color; }
            set { color = value; }
        }
        public int Width//크기
        {
            get { return width; }
            set { width = value; }
        }
        public int Shape
        {
            get { return shape; }
            set { shape = value; }
        }
        public ArrayList AR
        {
            get { return Ar; }
        }
    }
}
