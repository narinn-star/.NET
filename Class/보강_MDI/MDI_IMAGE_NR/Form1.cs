using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDI_IMAGE_NR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image I = Image.FromFile(openFileDialog1.FileName);

                Form2 Child = new Form2();
                Child.image = I;                // 이미지가 Form2으로 넘어옴
                Child.MdiParent = this;
                Child.Show();
            }
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "JPEG File(*.jpg)|*.jpg|Bitmap File(*bmp)|*.bmp|PNG File(*png)|*.png";
            //saveFileDialog1.
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Form2 Child = (Form2)this.ActiveMdiChild;       // Form2 Child = ActiveMdiChild as Form2;와 동일, ActiveMdiChild : 선택된 창
                if (Child != null)
                {
                    // 교수님은 위에 Filter 없어도 되던데 그거 왜그런거임 ㅇㅅㅇ..
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            Child.image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case 2:
                            Child.image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                        case 3:
                            Child.image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                    }
                }
            }
        }

        private void 닫기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = (Form2)this.ActiveMdiChild;           // Form2 Child = ActiveMdiChild as Form2;와 동일, ActiveMdiChild : 선택된 창
            if (Child != null)
                Child.Close();

            //Form2 form = ActiveMdiChild as Form2;
            //form.Dispose();
        }

        private void smoothingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 original = (Form2)this.ActiveMdiChild;    //Form2에 정의되어있는 image 가져옴

            if (original != null)
            {
                Bitmap gBitmap = new Bitmap(original.image);
                //https://docs.microsoft.com/ko-kr/dotnet/api/system.drawing.imaging.pixelformat?view=netframework-4.8
                
                // 흑백으로 바꿔주는 역할
                if (gBitmap.PixelFormat.ToString() != "Format8bppIndexed")          // Format8bppIndexed = 인덱싱된, 픽셀당 8비트 형식으로 지정 따라서 색상표에 256 색이 포함, 즉 흑백이 아닐 때
                {
                    for (int i = 0; i < gBitmap.Height; i++)
                    {
                        for (int j = 0; j < gBitmap.Width; j++)
                        {
                            int color = gBitmap.GetPixel(j, i).R + gBitmap.GetPixel(j, i).G + gBitmap.GetPixel(j, i).B;
                            color /= 3;
                            Color c = Color.FromArgb(color, color, color);
                            gBitmap.SetPixel(j, i, c);
                        }
                    }
                }

                // Smoothing Mask
                Bitmap Smoothing = new Bitmap(original.image);
                int[,] m = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
                int sum;

                for (int x = 1; x < gBitmap.Width - 1; x++)
                {
                    for (int y = 1; y < gBitmap.Height - 1; y++)
                    {
                        sum = 0;
                        for (int r = -1; r < 2; r++)
                        {
                            for (int c = -1; c < 2; c++)
                                sum += m[r + 1, c + 1] * gBitmap.GetPixel(x + r, y + c).R;
                        }
                        sum /= 9;   //1이 9개..  마스크를 씌운다
                        Smoothing.SetPixel(x, y, Color.FromArgb(sum, sum, sum));
                    }
                }

                //gBitmap : 원래 가져온 이미지, 이를 이용해서 Smoothing을 만들기 때문에 새로운 창 띄움
                Form2 child = new Form2();
                child.image = Smoothing;
                child.MdiParent = this;
                child.Show();
            }
        }

        private void edgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 original = ActiveMdiChild as Form2;   //활성화 된 창 가져오기

            if (original != null)
            {
                // 흑백으로 바꿔주는 역할
                Bitmap gBitmap = new Bitmap(original.image);    
                //https://docs.microsoft.com/ko-kr/dotnet/api/system.drawing.imaging.pixelformat?view=netframework-4.8
                
                if (gBitmap.PixelFormat.ToString() != "Format8bppIndexed")          // Format8bppIndexed = 흑백이 아니면 흑백으로 바꿔줌
                {
                    for (int i = 0; i < gBitmap.Height; i++)
                    {
                        for (int j = 0; j < gBitmap.Width; j++)
                        {
                            int color = gBitmap.GetPixel(j, i).R + gBitmap.GetPixel(j, i).G + gBitmap.GetPixel(j, i).B;
                            color /= 3;
                            Color c = Color.FromArgb(color, color, color);
                            gBitmap.SetPixel(j, i, c);
                        }
                    }
                }

                // Edge
                Bitmap Edge = new Bitmap(gBitmap);
                int[,] m = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
                int sum;
                for (int x = 1; x < gBitmap.Width - 1; x++)
                {
                    for (int y = 1; y < gBitmap.Height - 1; y++)
                    {
                        sum = 0;
                        for (int r = -1; r < 2; r++)
                        {
                            for (int c = -1; c < 2; c++)
                                sum += m[r + 1, c + 1] * gBitmap.GetPixel(x + r, y + c).R;
                        }
                        sum = Math.Abs(sum);                    // 0에서 255의 차이는 -255이기 때문에 마이너스 값이 나오지 않게 하기 위해 절댓값을 취해줌
                        if (sum > 255) sum = 255;
                        //if (sum < 0) sum = 0;                 // 절댓값 취했을 때 음수가 나올 수 없기 때문에 필요없는 소스임
                        
                        //마스크 값(?)을 다 더하면 0이기 때문에 마스크를 씌우는 sum / 0의 과정은 생략한 것.

                        //gBitmap : 원래 가져온 이미지, 이를 이용해서 Edge을 만들기
                        Edge.SetPixel(x, y, Color.FromArgb(sum, sum, sum));
                    }
                }

                Form2 child = new Form2();
                child.image = Edge;
                child.MdiParent = this;
                child.Show();
            }
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 original = ActiveMdiChild as Form2;
            if (original != null)
            {
                Bitmap gBitmap = new Bitmap(original.image);
                //https://docs.microsoft.com/ko-kr/dotnet/api/system.drawing.imaging.pixelformat?view=netframework-4.8

                if (gBitmap.PixelFormat.ToString() != "Format8bppIndexed")          // Format8bppIndexed = 흑백이 아니면 흑백으로 바꿔줌
                {
                    for (int i = 0; i < gBitmap.Height; i++)
                    {
                        for (int j = 0; j < gBitmap.Width; j++)
                        {
                            int color = gBitmap.GetPixel(j, i).R + gBitmap.GetPixel(j, i).G + gBitmap.GetPixel(j, i).B;
                            color /= 3;
                            Color c = Color.FromArgb(color, color, color);
                            gBitmap.SetPixel(j, i, c);
                        }
                    }
                }

                Bitmap Median = new Bitmap(gBitmap);
                int[] m = new int[9];
                for (int x = 1; x < gBitmap.Width - 1; x++)
                {
                    for (int y = 1; y < gBitmap.Height - 1; y++)
                    {
                        for (int r = -1; r < 2; r++)
                        {
                            for (int c = -1; c < 2; c++)
                                // 배열의 9개를 m배열로 옮겨온다.(?)
                                // 이미지 전체를 돌면서 샤샤ㅑㅅㄱ 다 담아온다..
                                m[(r + 1) * 3 + (c + 1)] = gBitmap.GetPixel(x + r, y + c).R;
                        }
                        Array.Sort(m);  //오름차순으로 정렬
                        Median.SetPixel(x, y, Color.FromArgb(m[4], m[4], m[4]));    //4번째 있는 값만 RGB에 넣는다.
                    }
                }
                Form2 child = new Form2();
                child.image = Median;
                child.MdiParent = this;
                child.Show();
            }
        }

        private void 밝게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = ActiveMdiChild as Form2;
            if (Child != null)
            {
                Image I = Child.image;
                Bitmap Bit = new Bitmap(I);
                for (int i = 0; i < Bit.Height; i++)
                {
                    for (int j = 0; j < Bit.Width; j++)
                    {
                        Color color = Bit.GetPixel(j, i);
                        int r = color.R + 50; if (r > 255) r = 255;
                        int g = color.G + 50; if (g > 255) g = 255;
                        int b = color.B + 50; if (b > 255) b = 255;
                        Bit.SetPixel(j, i, Color.FromArgb(r, g, b));
                    }
                }
                Form2 Child2 = new Form2();
                Child2.image = Bit;
                Child2.MdiParent = this;
                Child2.Show();
            }
        }

        private void 어둡게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = ActiveMdiChild as Form2;
            if (Child != null)
            {
                Image I = Child.image;
                Bitmap Bit = new Bitmap(I);
                for (int i = 0; i < Bit.Height; i++)
                {
                    for (int j = 0; j < Bit.Width; j++)
                    {
                        Color color = Bit.GetPixel(j, i);
                        int r = color.R - 50; if (r < 0) r = 0;
                        int g = color.G - 50; if (g < 0) g = 0;
                        int b = color.B - 50; if (b < 0) b = 0;
                        Bit.SetPixel(j, i, Color.FromArgb(r, g, b));
                    }
                }
                Form2 Child2 = new Form2();
                Child2.image = Bit;
                Child2.MdiParent = this;
                Child2.Show();
            }
        }
    }
}
