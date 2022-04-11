using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/* 더블 버퍼링을 하기 전
namespace DoubleBuffer
{
	public partial class Form1 : Form
	{
		int ex = 10, ey = 100;
		const int r = 15;

		public Form1()
		{
			InitializeComponent();
			timer1.Start();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			int x, y;

			for (x = 0; x < ClientRectangle.Right; x += 10)
			{
				e.Graphics.DrawLine(Pens.Black, x, 0, x, ClientRectangle.Bottom);	//격자무늬 긋기 (세로)
			}
			for (y = 0; y < ClientRectangle.Bottom; y += 10)
			{
				e.Graphics.DrawLine(Pens.Black, 0, y, ClientRectangle.Right, y);	//격자무늬 긋기 (가로)
			}

			e.Graphics.FillEllipse(new SolidBrush(Color.LightGreen), ex - r, ey - r, r * 2, r * 2);
			e.Graphics.DrawEllipse(new Pen(Color.Blue, 5), ex - r, ey - r, r * 2, r * 2);
		}

		private void timer1_Tick(object sender, EventArgs e)	//타이머 발생했을 때마다 실행 (timer1 속성 - Interval에 몇초 간격인지 지정 가능)
		{
			ex += 6;	//x (원)의 위치
			if (ex > ClientRectangle.Right) ex = 0;	//오른쪽 끝까지 닿으면 다시 왼쪽 끝부터 시작하도록
			Invalidate();
		}
	}
	// 가장 쉽게 해결하는 방법은 DobleBuffer Property 값을 true로 바꾸어주면 됨.
}
//*/

//* 더블 버퍼링을 하고 난 후
namespace DoubleBuffer
{
	public partial class Form1 : Form
	{
		int ex = 10, ey = 100;
		const int r = 15;
		Bitmap B;	//도화지 객체. 아직 초기화는 안함.

		public Form1()
		{
			InitializeComponent();
			timer1.Start();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			if (B != null)
			{
				e.Graphics.DrawImage(B, 0, 0);
				//e : 폼 윈도우
				//BackgroundImage 예제처럼.. 도화지(B)에 미리 다~ 그려놓고 그대로 옮겨오면 한번에 팡!! 나온다.. 요말임
			}
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
            //base.OnPaintBackground(e);
			// Invalidate() 발생하면 도화지까지 갈아버리는 동작이 있는데, 그 찰나에 깜빡임이 보인다. 그래서 OnPaintBackground를 오버라이딩 하여 배경을 지우는 것을 없앤 것.
			// 배경색으로 지우지 않음
		}

		private void timer1_Tick(object sender, EventArgs e)	// 0.05초 간격으로 실행됨. (속성창에서 지정 가능)
		{
			int x, y;

			if (B == null || B.Width != ClientSize.Width || B.Height != ClientSize.Height)
			{
				B = new Bitmap(ClientSize.Width, ClientSize.Height);
			}

			Graphics G = Graphics.FromImage(B);	//그리기 도구 인스턴스 생성
			G.Clear(SystemColors.Window);	// Clear 하지 않으면 동그라미가 지나간 흔적이 계속 남음.

			ex += 6;	//동그라미 위치 증가
			if (ex > ClientRectangle.Right) ex = 0;	//오른쪽 경계 넘어서면 다시 0으로 초기화

			for (x = 0; x < ClientRectangle.Right; x += 10)		//세로 격자 그리기
			{
				G.DrawLine(Pens.Black, x, 0, x, ClientRectangle.Bottom);	//인스턴스 B에다가 격자 다 그림
			}
			for (y = 0; y < ClientRectangle.Bottom; y += 10)	//가로 격자 그리기
			{
				G.DrawLine(Pens.Black, 0, y, ClientRectangle.Right, y);
			}

			G.FillEllipse(new SolidBrush(Color.LightGreen), ex - r, ey - r, r * 2, r * 2);	//동그라미
			G.DrawEllipse(new Pen(Color.Blue, 5), ex - r, ey - r, r * 2, r * 2);	//동그라미의 테두리

			Invalidate();	//Form1_Paint() 호출 -> timer1_Tick에서 도화지에 그려둔걸 그대로 옮기기
		}
	}
}
//*/
