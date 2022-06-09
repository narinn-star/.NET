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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        TcpClient tclient;
        ArrayList ar;
        Thread th1;

        public Form1()
        {
            InitializeComponent();
            ar = new ArrayList();
        }

        private void socketSendReceive()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 9000);
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
                    ar.Add(c);
                    Graphics g = CreateGraphics();
                    SolidBrush sb = new SolidBrush(c.bColor);
                    Pen pen = new Pen(c.pColor);
                    g.FillEllipse(sb, c.Point.X, c.Point.Y, c.Size, c.Size);
                    g.DrawEllipse(pen, c.Point.X, c.Point.Y, c.Size, c.Size);
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Random Random = new Random();
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < 3; i++)
                {
                    CMyData c = new CMyData();
                    c.Shape = (int)Random.Next(2);
                    c.Size = (int)Random.Next(50, 200);
                    c.Point = new Point((int)Random.Next(50, 1000), (int)Random.Next(50,500));
                    c.bColor = Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));
                    c.pColor = Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));
                    ar.Add(c);
                    Graphics g = CreateGraphics();
                    SolidBrush sb = new SolidBrush(c.bColor);
                    Pen pen = new Pen(c.pColor);
                    g.FillEllipse(sb, c.Point.X, c.Point.Y, c.Size, c.Size);
                    g.DrawEllipse(pen, c.Point.X, c.Point.Y, c.Size, c.Size);
                    if (tclient.Connected)
                    {
                        NetworkStream ns = tclient.GetStream();
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(ns, c);
                    }
                    else { label1.Text = "연결 끊김"; }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tclient = new TcpClient("127.0.0.1", 8000);
            if (tclient.Connected)
            {
                label1.Text = "연결 성공!";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            th1 = new Thread(new ThreadStart(socketSendReceive));
            th1.IsBackground = true;
            th1.Start();
        }
    }

    [Serializable]
    public class CMyData
    {
        private Point point;
        private Color penCol;
        private Color brushCol;
        private int size, shape;

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

    }
}
