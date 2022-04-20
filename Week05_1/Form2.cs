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
            label5.Invalidate();//정보가 변경되어서 그려져야 하는 부분에다가 label5.Paint 메소드를 계속 뿌려줘 라는 의미 
            
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
            iDialogPenWidth = int.Parse(comboBox1.Text);
            label5.Invalidate();//정보가 변경되어서 그려져야 하는 부분에다가 label5.Paint 메소드를 계속 뿌려줘 라는 의미 
        }


        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            DialogPenColor = Color.FromArgb(hScrollBar1.Value, hScrollBar2.Value, hScrollBar3.Value);
            textBox1.Text = hScrollBar1.Value.ToString();
            textBox2.Text = hScrollBar2.Value.ToString();
            textBox3.Text = hScrollBar3.Value.ToString();
            label5.Invalidate();//정보가 변경되어서 그려져야 하는 부분에다가 label5.Paint 메소드를 계속 뿌려줘 라는 의미 
        }

        private void label5_Paint(object sender, PaintEventArgs e)//스스로 지 window 에다가 그린다는 의미 
        {
            e.Graphics.DrawLine(new Pen(DialogPenColor, iDialogPenWidth), 0, label5.Height / 2, 
                label5.Width, label5.Height / 2);
            //여기 PAINT로 그리는 로직을 이용해서 label5.invalidate 즉 다시 Paint 해야할때마다 다시 그려줘 라는 메시지를 전달해주면 계속해서 다시그려짐
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            hScrollBar1.Value = int.Parse(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            hScrollBar2.Value = int.Parse(textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            hScrollBar3.Value = int.Parse(textBox3.Text);
        }
    }
}
