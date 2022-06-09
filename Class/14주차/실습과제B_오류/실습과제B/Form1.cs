﻿using System;
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

namespace 실습과제B
{
    public partial class Form1 : Form
    {
        static int svrPort = 9000;
        TcpClient tclient;
        Thread th1;
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
                    Invalidate();
                }
            }

        }

        ArrayList ar; // 배열 지정 데이터를 ArrayList에 저장한다.

        public Form1()
        {
            InitializeComponent();
            ar = new ArrayList();
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData c in ar) //ar안에 CMyData 추가.
            {

                e.Graphics.FillEllipse(Brushes.Red, c.Point.X, c.Point.Y, c.Size, c.Size);
                e.Graphics.DrawEllipse(Pens.Black, c.Point.X, c.Point.Y, c.Size, c.Size);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Random Random = new Random(); //랜덤 변수 지정

            if (e.Button == MouseButtons.Left) //마우스 왼쪽 클릭 이벤트
            {
                CMyData c = new CMyData(); //CMyData를 추가하고
                c.Size = (int)Random.Next(30, 100); // 사이즈는 50~200까지 랜덤으로 나온다.
                c.Point = new Point(e.X, e.Y); //마우스가 왼쪽에 클릭되는 위치를 저장해준다. 안할시에 0,0좌표에서 만들어짐 

                ar.Add(c); //배열에 저장
                if (tclient.Connected)
                {
                    NetworkStream ns = tclient.GetStream();
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ns, c);

                }
                Invalidate();
            }
        }
    }
    [Serializable]
    public class CMyData
    {
        private Point point;
        private int size;

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
    }
}
