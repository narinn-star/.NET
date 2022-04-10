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
//이진 포매터 사용
using System.Runtime.Serialization.Formatters.Binary;

namespace DrawLines
{
    public partial class Form1 : Form
    {
        private LinkedList<CMyData> total_lines;
        CMyData data;
        private int x, y;
        private Color CurrentPenColor;
        private int iCurrentPenWidth;

        public Form1()
        {
            total_lines = new LinkedList<CMyData>();
            CurrentPenColor = Color.Black;
            iCurrentPenWidth = 2;
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
                data = new CMyData();
                data.Color = CurrentPenColor;
                data.Width = iCurrentPenWidth;
                //ar = new ArrayList();
                //ar.Add(new Point(x, y));
                data.AR.Add(new Point(x, y));
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Capture && e.Button == MouseButtons.Left)
            {
                Graphics G = CreateGraphics();
                //펜 설정하기(색깔과 굵기) 
                Pen p = new Pen(data.Color, data.Width);
                G.DrawLine(p, x, y, e.X, e.Y);
                x = e.X;
                y = e.Y;
                data.AR.Add(new Point(x, y));
                G.Dispose();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            total_lines.AddLast(data);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (CMyData line in total_lines)
            {
                Pen p = new Pen(line.Color, line.Width);
                for (int i = 1; i < line.AR.Count; i++)
                {
                    e.Graphics.DrawLine(p, (Point)line.AR[i - 1], (Point)line.AR[i]);
                }
            }
        }

        private void 대화상자ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2();
            dlg.DialogPenColor = CurrentPenColor;  //set
            dlg.iDialogPenWidth = iCurrentPenWidth;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CurrentPenColor = dlg.DialogPenColor;      //get
                iCurrentPenWidth = dlg.iDialogPenWidth;
            }
            dlg.Dispose();

        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = @"C:\temp";
            saveFileDialog1.Title = "파일 저장하기";
            saveFileDialog1.Filter = "Bin 파일|*.bin|모든 파일|*.*";
            //byte[] data = { 65, 66, 67, 68, 69, 70, 71, 72 };
            //int[] data = { 65, 66, 67, 68, 69, 70, 71, 72 };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create,
                    FileAccess.Write);
                //BinaryWriter bw = new BinaryWriter(fs);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, total_lines);
                //fs.Write(data, 0, data.Length);
                fs.Close();
            }


            MessageBox.Show(saveFileDialog1.FileName + "파일에 기록했습니다.");
        }

        private void 불러오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //byte[] data = new byte[8];
            //int[] data = new int[8];
            try
            {
                openFileDialog1.InitialDirectory = @"C:\temp";
                openFileDialog1.Title = "파일 불러오기";
                openFileDialog1.Filter = "Bin 파일|*.bin|모든 파일|*.*";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate,
                        FileAccess.Read);
                    BinaryFormatter bf = new BinaryFormatter();
                    total_lines = (LinkedList<CMyData>)bf.Deserialize(fs);
                    fs.Close();
                    Invalidate();
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("지정한 파일이 없습니다.");
            }
        }

    }
    [Serializable]
    class CMyData
    {
        private ArrayList Ar;

        public CMyData()  //생성자함수
        {
            Ar = new ArrayList();
        }
        public Color Color
        {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }
        public ArrayList AR
        {
            get { return Ar; }
        }
    }
}
