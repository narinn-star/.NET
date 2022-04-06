using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week03_4
{
    public partial class Form2 : Form
    {
        //private Color DialogPenColor;
        public int iDialogPenWidth { get; set; }
        public Color DialogPenColor { get; set; }

        public Form2()
        {
            InitializeComponent();
        }

        //public Color Color
        //{
        //    get
        //    {
        //        return DialogPenColor;
        //    }
        //    set
        //    {
        //        DialogPenColor = value;
        //    }
        //}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            iDialogPenWidth = int.Parse(comboBox1.Text);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            iDialogPenWidth = int.Parse(comboBox1.Text);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            DialogPenColor = Color.FromArgb(hScrollBar1.Value, hScrollBar2.Value, hScrollBar3.Value);
            textBox1.Text = Convert.ToString(hScrollBar1.Value);
            textBox2.Text = Convert.ToString(hScrollBar2.Value);
            textBox3.Text = Convert.ToString(hScrollBar3.Value);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 2; i <= 10; i += 2)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.Text = iDialogPenWidth.ToString();

            hScrollBar1.Value = DialogPenColor.R;
            hScrollBar2.Value = DialogPenColor.G;
            hScrollBar3.Value = DialogPenColor.B;

            //textBox1.Text = Convert.ToString(hScrollBar1.Value);
            //textBox2.Text = Convert.ToString(hScrollBar2.Value);
            //textBox3.Text = Convert.ToString(hScrollBar3.Value);

            textBox1.Text = DialogPenColor.R.ToString();
            textBox2.Text = DialogPenColor.G.ToString();
            textBox3.Text = DialogPenColor.B.ToString();

            //이 위치에 작성하면 Load 했을 때 텍스트박스가 비어서 나옴..
            //hScrollBar1.Value = DialogPenColor.R;
            //hScrollBar2.Value = DialogPenColor.G;
            //hScrollBar3.Value = DialogPenColor.B;
        }
    }
}
