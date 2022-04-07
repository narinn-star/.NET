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

namespace Week01_2
{
    public partial class Form1 : Form
    {
        private LinkedList<ArrayList> total_lines;  //LinkedList 안에 요소는 ArrayList가 오고, 모두 연결되어 있음. ArrayList 한 개는 선 하나를 의미
        private ArrayList ar;
        private int x, y;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)   //캡쳐 동작을 직접 관리할 필요는 없으나, 캡쳐 동작 조건이 없으면 이상 동작을 할 수 있음
            {
                Graphics G = CreateGraphics();
                G.DrawLine(Pens.Black, x, y, e.X, e.Y);
                x = e.X;
                y = e.Y;
                G.Dispose();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach(ArrayList line in total_lines)  //전체 선에서 하나씩 빼오는데 그 선 하나하나가 ArrayList
            {
                for(int i = 1; i < line.Count; i++)
                {
                    e.Graphics.DrawLine(Pens.Black, (Point)line[i - 1], (Point)line[i]);
                }
            }
        }

        public Form1()
        {
            total_lines = new LinkedList<ArrayList>();
            InitializeComponent();
        }

    }
}
