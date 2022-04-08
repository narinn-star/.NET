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
            CurrentPenColor = Color.Red;    //최초 색 빨강으로 설정
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
                //ar = new ArrayList(); //처음 선 집어넣으려고 ar 하나 만듬
                data = new CMyData();
                ar.Add(new Point(x, y));    //처음 찍은 점 하나를 ar에다가 넣음
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics G = CreateGraphics();
                G.DrawLine(Pens.Black, x, y, e.X, e.Y); //검정, 처음 찍은 좌표 x, y, 현재 움직이고 있는 좌표 e.X, e.Y
                x = e.X;
                y = e.Y;
                ar.Add(new Point(x, y));//사용자가 그릴때마다 그려 넣음 
                G.Dispose();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            total_lines.AddLast(data);  //up 되면 방금 그린 모든 정보들을 AddLast를 통해서 끝에다가 달아줌
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //foreach(CMyData line in ar)
            foreach (ArrayList line in total_lines)
            {
                for (int i = 1; i < line.Count; i++)
                {
                    e.Graphics.DrawLine(Pens.Black, (Point)line[i - 1], (Point)line[i]);
                }
            }
        }

        private void 빨강ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        class CMyData
        {
            private ArrayList Ar;

            public CMyData()  //생성자함수
            {
                Ar = new ArrayList();
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
                get { return Ar; }
            }
        }
    }
}