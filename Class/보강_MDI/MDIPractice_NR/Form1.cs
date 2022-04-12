using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDIPractice_NR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if(op.ShowDialog() == DialogResult.OK)
            {
                Image I = Image.FromFile(op.FileName);

                Form2 Child = new Form2();
                Child.image = I;
                Child.MdiParent = this;
                Child.Show();   //그려짐 (Form2의 Paint 호출)
            }
        }

        private void 밝게하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = (Form2)this.ActiveMdiChild;       //Focus되어있는 폼!
            if(Child != null)
            {
                Image I = Child.image;

                Bitmap B = new Bitmap(I);
                for(int y = 0; y < B.Height; y++)
                    for(int x = 0; x < B.Width; x++)
                    {
                        Color color = B.GetPixel(x, y);
                        int r = color.R;
                        int g = color.G;
                        int b = color.B;
                        //Saturation
                        if (((ToolStripMenuItem)sender).Text.Equals("밝게하기"))
                        {
                            r = r + 50 > 255 ? 255 : r + 50;    //255보다 크면 255로, 그렇지 않으면 +50
                            g = g + 50 > 255 ? 255 : g + 50;
                            b = b + 50 > 255 ? 255 : b + 50;
                        }
                        B.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                Child = new Form2();
                Child.image = B;
                Child.MdiParent = this;
                Child.Show();
            }
        }

        private void 어둡게하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = (Form2)this.ActiveMdiChild;       //Focus되어있는 폼!
            if (Child != null)
            {
                Image I = Child.image;

                Bitmap B = new Bitmap(I);
                for (int y = 0; y < B.Height; y++)
                    for (int x = 0; x < B.Width; x++)
                    {
                        Color color = B.GetPixel(x, y);
                        int r = color.R;
                        int g = color.G;
                        int b = color.B;
                        //Saturation
                        if (((ToolStripMenuItem)sender).Text.Equals("어둡게하기"))
                        {
                            r = r - 50 < 0 ? 0 : r - 50;        //0보다 작으면 0으로, 그렇지 않으면 -50
                            g = g - 50 < 0 ? 0 : g - 50;
                            b = b - 50 < 0 ? 0 : b - 50;
                        }
                        B.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                Child = new Form2();
                Child.image = B;
                Child.MdiParent = this;
                Child.Show();
            }
        }

        private void 흑백ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = (Form2)this.ActiveMdiChild;
            if(Child != null)
            {
                Image I = Child.image;

                Bitmap B = new Bitmap(I);
                for(int y = 0; y < B.Height; y++)
                    for(int x = 0; x < B.Width; x++)
                    {
                        Color color = B.GetPixel(x, y);
                        byte r = (byte)((color.R + color.G + color.B) / 3);
                        byte g = r; // (byte)((color.R + color.G + color.B) / 3);
                        byte b = r; // (byte)((color.R + color.G + color.B) / 3);

                        B.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                Child = new Form2();
                Child.image = B;
                Child.MdiParent = this;
                Child.Show();
            }
        }

        private void 이진화ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 Child = (Form2)this.ActiveMdiChild;
            if (Child != null)
            {
                Image I = Child.image;

                Bitmap B = new Bitmap(I);
                for (int y = 0; y < B.Height; y++)
                    for (int x = 0; x < B.Width; x++)
                    {
                        //흑백으로 만든 후에
                        Color color = B.GetPixel(x, y);
                        int r = ((color.R + color.G + color.B) / 3);
                        int g = ((color.R + color.G + color.B) / 3);
                        int b = ((color.R + color.G + color.B) / 3);

                        // 검정과 흰색으로만 나타내기
                        r = r >= 127 ? 0 : 255;
                        g = g >= 127 ? 0 : 255;
                        b = b >= 127 ? 0 : 255;
                        
                        B.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                Child = new Form2();
                Child.image = B;
                Child.MdiParent = this;
                Child.Show();
            }
        }
    }
}
