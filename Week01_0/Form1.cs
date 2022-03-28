using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;   //배열, 배열리스트, 해시 테이블, 큐 등을 담고있음. ArrayList를 사용하기 위해 선언, p373 참고

namespace Week01
{
    public partial class Form1 : Form
    {
        ArrayList ar;   //ArrayList 'ar' 이라는 배열 리스트를 선언
        public Form1()
        {
            InitializeComponent();
            ar = new ArrayList();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e) //e : 사용자가 클릭한 버튼의 클래스 인스턴스를 다 가져오는 것
        {
            Random Random = new Random();
            
            if(e.Button == MouseButtons.Left)
            {
                CMyData c = new CMyData();  //여기 한 줄로 인해서 클래스 인스턴스, 즉 CMyData가 가지는 5칸짜리 인스턴스가 생성, 원과 사각형의 정보를 저장할 클래스
                c.Shape = (int)Random.Next(2);  // 0~1 생성해서 도형 결정 (1 == 원, 2 == 사각형)
                c.Size = (int)Random.Next(50, 200); // 도형의 사이즈를 결정 (50이상 200미만)
                c.Point = new Point(e.X, e.Y);  // 마우스 버튼 클릭시 좌표 저장, e.X 하면 X에 해당하는 정보를 e에서 빼옴
                c.bColor = Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));    // 브러쉬 컬러 랜덤 선택
                c.pColor = Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));    // 펜 컬러 랜덤 선택, property의 set을 호출
                ar.Add(c);  //ArrayList인 ar에 클래스 인스턴스 집어넣음
                /*
                Graphics G = CreateGraphics();
                G.DrawEllipse(Pens.Red, e.X, e.Y, c.Size, c.Size); 이거 쓰고 Invalidate 지우면 .. 머요?
                G.FillEllipse(Brushes.Blue, e.X, e.Y, c.Size, c.Size);
                 */
            }
            Invalidate();   //os에게 WM_PAINT 메시지를 발생시켜달라고 요구. => Form1_Paint를 호출하게 됨
        }
        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            foreach (CMyData c in ar) //ar이라는 배열에서 하나씩 빼온다는 의미. 생성된 클래스 인스턴스 박스에서 하나씩 가져오는 느낌
            {
                SolidBrush brc = new SolidBrush(c.bColor);
                Pen p = new Pen(c.pColor);  //여기서는 property의 get 호출

                if (c.Shape == 1)
                {
                    Graphics G = e.Graphics;
                    G.DrawEllipse(p, c.Point.X, c.Point.Y, c.Size, c.Size);
                    //e.Graphics.DrawEllipse(p, c.Point.X, c.Point.Y, c.Size, c.Size); 위 두줄과 똑같음
                    e.Graphics.FillEllipse(brc, c.Point.X, c.Point.Y, c.Size, c.Size);
                }
                else
                {
                    e.Graphics.DrawRectangle(p, c.Point.X, c.Point.Y, c.Size, c.Size);
                    e.Graphics.FillRectangle(brc, c.Point.X, c.Point.Y, c.Size, c.Size);
                }
            }
        }

        //CMyData 자료구조 class 추가
        public class CMyData    //프로퍼티 참고 p.210
        {
            private Point point;    // 위치
            //private Color penCol;   // 펜 컬러
            private Color brushCol; // 브러쉬 컬러
            private int size, shape;// 크기, 모양

            public Color pColor//펜컬러, pColor 형태로 변경해주면 pColor가 메소드도 되면서 property도 되는 두가지 역할을 하게 해줌.
            {
                get;    //{ return penCol; }
                set;    //{ penCol = value; }
            }

            public Color bColor
            {
                get { return brushCol; }
                set { brushCol = value; }
            }

            public Point Point
            {
                get { return point; }
                set { point = value; }
            }

            public int Size
            {
                get { return size; }
                set { size = value; }
            }

            public int Shape
            {
                get { return shape; }
                set { shape = value; }
            }
        }

        
    }
}
