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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace _20203179
{
    public partial class Form1 : Form
    {
        CMyData data;
        private int x, y;
        private Color CurrentPenColor;
        private int iCurrentShape;
        private int iCurrentWidth;
        private int iCurrentHeight;
        private ArrayList ar;

        Bitmap B;

        public Form1()
        {
            ar = new ArrayList();
            CurrentPenColor = Color.Black;
            iCurrentShape = 0;
            iCurrentWidth = 2;
            iCurrentHeight = 2;
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                data = new CMyData();

                x = e.X;
                y = e.Y;
            }
            Invalidate();
            Update();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (B == null || B.Width != ClientSize.Width || B.Height != ClientSize.Height)
                B = new Bitmap(ClientSize.Width, ClientSize.Height);

            Graphics G = Graphics.FromImage(B);
            G.Clear(SystemColors.Window);

            foreach (CMyData cd in ar)
            {
                SolidBrush brush = new SolidBrush(cd.Color);
                Pen pen = new Pen(cd.Color);

                if (cd.Shape == 0)
                {
                    //e.Graphics.DrawRectangle(pen, cd.Sx, cd.Sy, cd.Width, cd.Height);
                    //e.Graphics.FillRectangle(brush, cd.Sx, cd.Sy, cd.Width, cd.Height);
                    G.DrawRectangle(pen, cd.Sx, cd.Sy, cd.Width, cd.Height);
                    G.FillRectangle(brush, cd.Sx, cd.Sy, cd.Width, cd.Height);
                }
                else if (cd.Shape == 1)
                {
                    //e.Graphics.DrawEllipse(pen, cd.Sx, cd.Sy, cd.Width, cd.Height);
                    //e.Graphics.FillEllipse(brush, cd.Sx, cd.Sy, cd.Width, cd.Height);
                    G.DrawEllipse(pen, cd.Sx, cd.Sy, cd.Width, cd.Height);
                    G.FillEllipse(brush, cd.Sx, cd.Sy, cd.Width, cd.Height);
                }
            }

            if (B != null)
                e.Graphics.DrawImage(B, 0, 0);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics G = CreateGraphics();
                SolidBrush brush = new SolidBrush(CurrentPenColor);
                Pen pen = new Pen(CurrentPenColor);

                iCurrentWidth = e.X - x;
                iCurrentHeight = e.Y - y;

                if (iCurrentShape == 0)     //Rectangle
                {
                    G.DrawRectangle(pen, x, y, iCurrentWidth, iCurrentHeight);
                    G.FillRectangle(brush, x, y, iCurrentWidth, iCurrentHeight);
                }
                else if (iCurrentShape == 1)    //Ellipse
                {
                    G.DrawEllipse(pen, x, y, iCurrentWidth, iCurrentHeight);
                    G.FillEllipse(brush, x, y, iCurrentWidth, iCurrentHeight);
                }
                G.Dispose();
            }
            Invalidate();
            Update();
        }

        protected override void OnPaintBackground(PaintEventArgs e)     // Bitmap
        {
            //base.OnPaintBackground(e);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics G = CreateGraphics();
            SolidBrush brush = new SolidBrush(CurrentPenColor);
            Pen pen = new Pen(CurrentPenColor);

            iCurrentWidth = e.X - x;
            iCurrentHeight = e.Y - y;

            if (iCurrentShape == 0) //Rectangle
            {
                G.DrawRectangle(pen, data.Sx, data.Sy, data.Width, data.Height);
                G.FillRectangle(brush, data.Sx, data.Sy, data.Width, data.Height);
            }
            else if (iCurrentShape == 1)
            {
                G.DrawEllipse(pen, data.Sx, data.Sy, data.Width, data.Height);
                G.FillEllipse(brush, data.Sx, data.Sy, data.Width, data.Height);
            }
            G.Dispose();

            data.Sx = x;
            data.Sy = y;
            data.Height = iCurrentHeight;
            data.Width = iCurrentWidth;
            data.Shape = iCurrentShape;
            data.Color = CurrentPenColor;
            ar.Add(data);

            Invalidate();
            Update();
        }

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2();
            dlg.iDialogPenColor = CurrentPenColor;  //set
            dlg.iDialogShape = iCurrentShape;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CurrentPenColor = dlg.iDialogPenColor;      //get
                iCurrentShape = dlg.iDialogShape;
            }
            dlg.Dispose();
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create,
                FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, ar);
                // BinaryWriter bw = new BinaryWriter(fs);
                //Kim.Write(bw);
                fs.Close();
                MessageBox.Show(saveFileDialog1.FileName + " 파일에 기록했습니다.");
            }
        }

        private void 불러오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(openFileDialog1.FileName + "를 선택했습니다.");
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                ar = (ArrayList)bf.Deserialize(fs);
                fs.Close();
                Invalidate();
            }
        }
    }

    [Serializable]
    class CMyData
    {
        public Color Color { get; set; }
        public int Shape { get; set; }   // 0 = 사각형, 1 = 타원
        public int Sx { get; set; }      // 시작점
        public int Sy { get; set; }
        public int Width { get; set; }   // 폭, 높이
        public int Height { get; set; }
    }
}
