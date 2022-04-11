using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Image I = Image.FromFile("아기.jpg");
            Bitmap B = new Bitmap(I);   //그림 파일에서 픽셀에 담긴 칼라 값을 빼오는 것
                                        //* 
            for (int y = 0; y < B.Height; y++)  // B.Height : 그림 높이
                for (int x = 0; x < B.Width; x++)   //B.Width : 그림 폭
                {
                    Color color = B.GetPixel(x, y);
                    byte r = (byte)~color.R;    //값을 반전시킴. color.R이 1이면 r은 254 등
                    byte g = (byte)~color.G;
                    byte b = (byte)~color.B;
                    B.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            // */
            e.Graphics.DrawImage(B, 0, 0);

        }
    }
}
