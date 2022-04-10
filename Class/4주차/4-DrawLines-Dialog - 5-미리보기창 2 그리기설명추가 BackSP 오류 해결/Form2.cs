using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawLines
{
    public partial class Form2 : Form
    {
        public Color DialogPenColor { get; set; }
        public int iDialogPenWidth { get; set; }

   
        public Form2()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            iDialogPenWidth = (((ComboBox)sender).SelectedIndex + 1) * 2;
            label5.Invalidate();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 2; i <= 10; i += 2)
            {
                comboBox1.Items.Add(i);
            }
            //comboBox1.SelectedIndex = iDialogPenWidth / 2 - 1;
            comboBox1.Text = iDialogPenWidth.ToString();

            hScrollBar1.Value = DialogPenColor.R;
            hScrollBar2.Value = DialogPenColor.G;
            hScrollBar3.Value = DialogPenColor.B;
            label2.Text = "R";
            label3.Text = "G";
            label4.Text = "B";
            textBox1.Text = DialogPenColor.R.ToString();
            textBox2.Text = DialogPenColor.G.ToString();
            textBox3.Text = DialogPenColor.B.ToString();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            //BackSpace 입력시 오류 발생...

            //이거는 BackSpace랑 문자열까지 다 잡힘.
            //if (comboBox1.Text != "") // or
            int value;
            if (int.TryParse(comboBox1.Text, out value))    //comboBox1에 입력되는 값이 int로 바뀌는지 파싱을 Try해보자. -> true이면 다음을 실행   
            {
                //오버플로우일 경우에는 여기에 if문 하나 더 추가해서 굵기에 제한을 두면 됨.
                iDialogPenWidth = value;
                //iDialogPenWidth = int.Parse(comboBox1.Text);
                label5.Invalidate();
            }

            //BackSpace만 잡는것
            //if (comboBox1.Text != "")
            //{
            //    iDialogPenWidth = int.Parse(comboBox1.Text);
            //    label5.Invalidate();
            //}

        }


        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            DialogPenColor = Color.FromArgb(hScrollBar1.Value, hScrollBar2.Value, hScrollBar3.Value);
            textBox1.Text = hScrollBar1.Value.ToString();
            textBox2.Text = hScrollBar2.Value.ToString();
            textBox3.Text = hScrollBar3.Value.ToString();
            label5.Invalidate(); //OS야 label5 윈도우로 WM_PAINT 메시지 보내도...
        }

        private void label5_Paint(object sender, PaintEventArgs e)  //WM_PAINT 호출했을 때 Graphics의 e가 불러와짐. 이 때 label5가 도화지가 됨.
        {
            //Graphics G = CreateGraphics();
            //G.DrawLine(new Pen(DialogPenColor, iDialogPenWidth), label5.Left - 30, label5.Top - 30, label5.Right + 30, label5.Bottom + 30);
            //G.DrawLine(new Pen(DialogPenColor, iDialogPenWidth), 0,0, label5.Right + 30, label5.Bottom + 30); //이 때 뭐가 도화지인지 확인 가능 -> form창
            //-30, +30 오류를 보이게 하려고 넣어둔것....
            //조사식으로 label5.Left와 label5.Right를 보면 숫자가 나오는데 이는 Form창의 좌측상단을 (0,0) 기준으로 좌표가나옴.

            //요걸로 해야 원래 아는 것처럼 보임
            e.Graphics.DrawLine(new Pen(DialogPenColor, iDialogPenWidth), 0, label5.Height / 2, label5.Width, label5.Height / 2);
            //도화지가 label5가 되었으니까 label5를 기준으로 0, label5.Height/2를 하면 label5크기대로 나옴.
        }
    }
}
