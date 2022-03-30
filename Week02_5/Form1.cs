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

namespace Week02_5
{
    public partial class Form1 : Form
    {
        private LinkedList<CMyData> total_lines;
        private CMyData mydata;
        private Point p;

        private Color CurrentColor;

        public Form1()
        {
            CurrentColor = Color.FromArgb(255, 0, 0);
            total_lines = new LinkedList<CMyData>();
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //foreach (ArrayList line in total_lines)
            foreach (CMyData line in total_lines)
            {
                Pen p = new Pen(line.Color, 1);
                //for (int i = 1; i < line.Count; i++)    //이제 AR로 갖고 놀거라공
                for (int i = 1; i < line.AR.Count; i++)
                {
                    //e.Graphics.DrawLine(Pens.Black, (Point)line[i - 1], (Point)line[i]);
                    e.Graphics.DrawLine(p, (Point)line.AR[i - 1], (Point)line.AR[i]);
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            total_lines.AddLast(mydata);
            //mydata = new CMyData();
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

        private void 사각형ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.FromArgb(255, 0, 0);
        }

        private void 원형ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.FromArgb(0, 255, 0);
        }

        private void 자유곡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.FromArgb(0, 0, 255);
        }
    }
    public class CMyData
    {
        private Color color;//펜컬러
        private int width;//크기
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
        public ArrayList AR
        {
            get { return Ar; }
        }
    }
}
