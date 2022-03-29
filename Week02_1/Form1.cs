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

namespace Week02_1
{
    public partial class Form1 : Form
    {
        //private LinkedList<ArrayList> total_lines;
        private LinkedList<CMyData> total_lines;
        CMyData data;
        private int x, y;
        private Color CurrentPenColor;
        private ArrayList ar;

        public Form1()
        {
            //total_lines = new LinkedList<ArrayList>();
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
                //ar = new ArrayList(); // 처음 선 집어넣으려고 ar 하나 만듬, //여기서 안하고 CMyData 생성자에서 ArrayList 생성
                //ar.Add(new Point(x, y));    //처음 찍은 점 하나를 ar에다가 넣음
                data = new CMyData();
                data.Color = CurrentPenColor;
                data.AR.Add(new Point(x, y));   //CMyData의 AR ArrayList에 좌표 추가
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics G = CreateGraphics();
                Pen p = new Pen(data.Color, 1); //펜설정 (색깔, 굵기)
                G.DrawLine(p, x, y, e.X, e.Y); //펜, 처음 찍은 좌표 x, y, 현재 움직이고 있는 좌표 e.X, e.Y
                x = e.X;
                y = e.Y;
                //ar.Add(new Point(x, y)); //사용자가 그릴때마다 그려 넣음 //이제 data.AR로 갖고 놀거임
                data.AR.Add(new Point(x, y));
                G.Dispose();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            total_lines.AddLast(data);  //up 되면 방금 그린 모든 정보들을 AddLast를 통해서 끝에다가 달아줌
            //total_lines.AddLast(ar);
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

        private void rEDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Red;
        }

        private void gREENToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Green;
        }

        private void bLUEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentPenColor = Color.Blue;
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
}
