 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Curve
{
	public partial class Form1 : Form
	{
        private LinkedList<ArrayList> total_lines;//LinkedList 안에 요소는 ArrayList가 오고 그런 놈들이 연결됨. ArrayList 한개는 선 한개를 의미함
        private ArrayList ar;
        private int x, y;

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
				G.Dispose();
			}
		}

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (ArrayList line in total_lines)//전체 줄에서 하나씩 빼내오는데 그 줄 하나하나가 ArrayList임
            {
                for (int i = 1; i < line.Count; i++)
                {
                    e.Graphics.DrawLine(Pens.Black, (Point)line[i - 1], (Point)line[i]);
                }
            }
        }
	}
}


