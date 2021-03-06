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
        private LinkedList<CMyData> total_lines;
        CMyData data;
        private int x, y;
        private Color CurrentPenColor;
        private ArrayList ar;

        public Form1()
        {
            total_lines = new LinkedList<CMyData>();
            CurrentPenColor = Color.Red;
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
                data = new CMyData();
                data.Color = CurrentPenColor;
                //ar = new ArrayList();     
                //ar.Add(new Point(x, y));
                data.AR.Add(new Point(x, y));   
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics G = CreateGraphics();
                //펜 설정하기(색깔과 굵기) 
                Pen p = new Pen(data.Color, 1);
                G.DrawLine(p, x, y, e.X, e.Y);
                x = e.X;
                y = e.Y;
                data.AR.Add(new Point(x, y));
                G.Dispose();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            total_lines.AddLast(data);
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
            CurrentPenColor = Color.Red;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Green;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Blue;
        }
    }
    class CMyData
    {
        private ArrayList Ar;

        public CMyData()  //생성자함수
        {
            Ar = new ArrayList();   //생성되자마자 Ar 생성하고 시작 
        }
        public Color Color
        {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }
        public ArrayList AR
        {
            //ar 대신 AR을 가지고 논다고 생각하면 쉬움
            get { return Ar; }
        }
    }
}
