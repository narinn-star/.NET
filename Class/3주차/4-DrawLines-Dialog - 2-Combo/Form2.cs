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
        private Color DialogPenColor;
        public int iDialogPenWidth { get; set; }

        public Color Color
        {
            get
            {
                if (radioButton1.Checked) DialogPenColor = Color.Red;
                if (radioButton2.Checked) DialogPenColor = Color.Green;
                if (radioButton3.Checked) DialogPenColor = Color.Blue;
                return DialogPenColor;
            }
            set
            {
                DialogPenColor = value;
                if (DialogPenColor == Color.Red) radioButton1.Checked = true;
                if (DialogPenColor == Color.Green) radioButton2.Checked = true;
                if (DialogPenColor == Color.Blue) radioButton3.Checked = true;
            }
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            iDialogPenWidth = (((ComboBox)sender).SelectedIndex + 1) * 2;
            //iDialogPenWidth = (sender.SelectedIntex + 1) * 2; //위 처럼 업캐스팅 해주어야 작동함. 나는 ComboBox인거 알지만 얘는몰랑
            //iDialogPenWidth = int.Parse(comboBox1.Text) // 위에꺼랑 똑같음. 돌아가는 건 문제 없당
            //sender : 보내는 사람(ComboBox), e : 이벤트를..
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 2; i <= 10; i += 2)
            {
                comboBox1.Items.Add(i);
            }
            //comboBox1.SelectedIndex = iDialogPenWidth / 2 - 1;
            comboBox1.Text = iDialogPenWidth.ToString();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            iDialogPenWidth = int.Parse(comboBox1.Text);
        }
    }
}
