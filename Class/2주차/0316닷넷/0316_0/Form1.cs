using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;//배열, 배열 리스트, 해시 테이블, 큐 등을 담고있음 / ArrayList를 사용하기 위해 선언 

namespace _0316
{
    public partial class Form1 : Form
    {
        ArrayList ar; //ArrayList 'ar' 이라는 배열리스트를 선언
        public Form1()
        {
            InitializeComponent();
            ar = new ArrayList();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)//여기서 e 의 의미는 사용자가 버튼클릭한 클래스 인스턴스를 다 가져와주는거임 
        {
            Random Random = new Random();

            if(e.Button==MouseButtons.Left)
            {
                CMYData c = new CMYData();//여기 한 줄로 인해서 클래스 인스턴스 즉 CMYDATA가 가지는 5칸짜리 인스턴스가 생성됨
                c.Shape = (int)Random.Next(2);//0~1 생성해서 도형을 결정한다
                c.Size = (int)Random.Next(50, 200);
                c.Point = new Point(e.X, e.Y);//e.X 하면 X에 해당하는 정보를 e 에서 빼옴
                c.bColor = Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));
                c.pColor = Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));//여기서는 property 의 set을  호출하는거임
                ar.Add(c);//ArrayList 인 ar 에 클래스 인스턴스를 집어넣음 
               /* Graphics G = CreateGraphics();
                G.DrawEllipse(Pens.Red, e.X, e.Y, c.Size, c.Size); 이거 쓰고 INvalidate 지우면 뭐 그렇게되는데 알제 ? 
                G.FillEllipse(Brushes.Blue, e.X, e.Y, c.Size, c.Size);*/ 
            }
            Invalidate();//os에게 WM_PAINT 메시지를 발생시켜달라고 요구 이렇게 되면 Form1_Paint를 호출하게 된다 
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach(CMYData c in ar)//ar이라는 배열에서 하나씩 빼온다는 의미 그러니까 생성된 클래스 인스턴스 박스에서 하나씩 가져오는 느낌 이해될거임
            {
                SolidBrush brc = new SolidBrush(c.bColor);
                Pen p = new Pen(c.pColor);//여기서는 property 의 get 을 호출하고 
                
                if(c.Shape==1)
                {
                    Graphics G = e.Graphics;//
                    G.DrawEllipse(p, c.Point.X, c.Point.Y, c.Size, c.Size);// 이거 두줄 밑에 한줄처럼 합칠수있고 같은 의미임
                    e.Graphics.FillEllipse(brc, c.Point.X, c.Point.Y, c.Size, c.Size);
                }
                else
                {
                    e.Graphics.DrawRectangle(p, c.Point.X, c.Point.Y, c.Size, c.Size);
                    e.Graphics.FillRectangle(brc, c.Point.X, c.Point.Y, c.Size, c.Size);
                }
            }
        }
    }
    public class CMYData
    {
        private Point point;
        //private Color penCol;
        private Color brushCol;
        private int size, shape;

        public  Color pColor//pColor 형태로 변경해주면 pColor 가 메소드도 되면서 property 도 되는 두가지 역할을 하게해줌
        {
            get;// { return penCol; }
            set ;//{ penCol = value; }
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
            set { shape = value; }//30번째 줄에서 발생한 Random 한 0 또는 1을 받아서 여기로 온다 , 즉 value 에 0또는1이 들어감
        }
    }
}
