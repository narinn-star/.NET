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
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace 선_그리기_통신
{
    public partial class Form1 : Form
    {
        private LinkedList<CMyData> total_lines;
        TcpClient tclient;
        CMyData data;
        ArrayList ar;
        Thread th1;
        Bitmap myBitmap;
        private int x, y;
        private Color CurrentPenColor;
        Graphics G;

        public Form1()
        {
            InitializeComponent();
            ar = new ArrayList();
            myBitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
        }

        private void socketSendReceive()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 8000);
            listener.Start();
            TcpClient serverClient = listener.AcceptTcpClient();
            if (serverClient.Connected)
            {
                NetworkStream ns = serverClient.GetStream();
                Encoding unicode = Encoding.Unicode;
                while (true)
                {
                    CMyData c = new CMyData();
                    BinaryFormatter bf = new BinaryFormatter();
                    c = (CMyData)bf.Deserialize(ns);
                    //x = e.X;
                    //y = e.Y;
                    data = new CMyData();
                    data.pColor = CurrentPenColor;
                    //data.Width = iCurrentPenWidth;
                    //ar = new ArrayList();
                    //ar.Add(new Point(x, y));
                    data.AR.Add(new Point(x, y));
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            th1 = new Thread(new ThreadStart(socketSendReceive));
            th1.IsBackground = true;
            th1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //foreach (CMyData line in total_lines)
            //{
            //    Pen p = new Pen(line.pColor, 1);
            //    for (int i = 1; i < line.AR.Count; i++)
            //    {
            //        e.Graphics.DrawLine(p, (Point)line.AR[i - 1], (Point)line.AR[i]);
            //    }
            //}
            if (myBitmap != null)//*Bitmap이 비워져있지 않으면 
            {
                e.Graphics.DrawImage(myBitmap, 0, 0);//*그대로 paint 이벤트 생기는곳에다가 다른도화지 Bitmap에 그려진 그림을 다시 내 윈도우에다가 그려줌 
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Random Random = new Random();
            if (e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
                data = new CMyData();
                data.pColor = CurrentPenColor;
                //data.Width = iCurrentPenWidth;
                //ar = new ArrayList();
                //ar.Add(new Point(x, y));
                data.AR.Add(new Point(x, y));

                if (tclient.Connected)
                {
                    NetworkStream ns = tclient.GetStream();
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ns, data);
                }
                else { label1.Text = "연결 끊김"; }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics G = CreateGraphics();
                //펜 설정하기(색깔과 굵기) 
                Pen p = new Pen(data.pColor, 1);
                G.DrawLine(p, x, y, e.X, e.Y);
                x = e.X;
                y = e.Y;
                data.Point = new Point(x, y);
                G.Dispose();
                
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            total_lines.AddLast(data);
            foreach (CMyData line in total_lines)//* 기존에 Paint 이벤트에서 수행했던부분을 Mouseup시에 그대로 myBitmap에다가 저장
            {
                Pen p = new Pen(line.pColor, 1);
                for (int i = 1; i < line.AR.Count; i++)
                {
                    G = Graphics.FromImage(myBitmap);//*여기서도 새로운 도화지 myBitmap에다가 그림
                    G.DrawLine(p, (Point)line.AR[i - 1], (Point)line.AR[i]);//*위 한줄로 인해서 이 그림은 새 도화지에다 그려지는거임
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tclient = new TcpClient("127.0.0.1", 9000);
            if (tclient.Connected)
            {
                label1.Text = "연결 성공!";
            }
        }
    }
    [Serializable]
    public class CMyData
    {
        private Point point;
        private Color penCol;
        private Color brushCol;
        private int size, shape;
        private ArrayList Ar;

        public CMyData()
        {
            Ar = new ArrayList();
        }
        public Color pColor
        {
            get { return penCol; }
            set { penCol = value; }
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
        public ArrayList AR
        {
            get { return Ar; }
        }
    }
}
