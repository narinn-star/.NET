using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BackgroundImage
{
	public partial class Form1 : Form
	{
		private Bitmap B = new Bitmap(600, 400);	//빈 도화지 하나 가져오기

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImage(B, 0, 0);
			/* 직접 그리기
			 * 아래 소스 주석처리 하면 그려지는 과정이 보이지 않은 채 바로바로 그려지는 것을 볼 수 있음.
			e.Graphics.Clear(BackColor);
			Random R = new Random();

			for (int i = 0; i < 500; i++)
			{
				SolidBrush Br = new SolidBrush(Color.FromArgb(R.Next(256),
					R.Next(256), R.Next(256), R.Next(256)));
				e.Graphics.FillEllipse(Br, R.Next(600), R.Next(400), R.Next(70) + 30,
					R.Next(70) + 30);	//30px보다는 크고 69px보다 작은 Ellipse 생성
			}
			//*/
		}

		private void Form1_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Graphics G = Graphics.FromImage(B);	//B를 그리기 위한 그리기 도구통 G
				G.Clear(BackColor);		// 이전에 그렸던 그림 지우기
				Random R = new Random();

				for (int i = 0; i < 500; i++)
				{
					SolidBrush Br = new SolidBrush(Color.FromArgb(R.Next(256),
						R.Next(256), R.Next(256), R.Next(256)));
					G.FillEllipse(Br, R.Next(600), R.Next(400), R.Next(70) + 30,
						R.Next(70) + 30);
				}
				Invalidate();	//e.Graphics.Clear(BackColor) 역할이 포함되어 있음.
			}
		}
	}
}
