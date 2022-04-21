using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20203179
{
    public partial class Form2 : Form
    {
        private int shape;
        public Color iDialogPenColor;

        public int iDialogShape
        {
            get
            {
                if (radioButton1.Checked) shape = 0;
                if (radioButton2.Checked) shape = 1;
                return shape;
            }
            set
            {
                shape = value;
                if (shape == 0) radioButton1.Checked = true;
                if (shape == 1) radioButton2.Checked = true;

            }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            iDialogPenColor = Color.FromArgb(hScrollBar1.Value, hScrollBar2.Value, hScrollBar3.Value);
            textBox1.Text = hScrollBar1.Value.ToString();
            textBox2.Text = hScrollBar2.Value.ToString();
            textBox3.Text = hScrollBar3.Value.ToString();
            label4.Invalidate();
        }

        private void label4_Paint(object sender, PaintEventArgs e)
        {
            int padding = 10;
            if (shape == 0)  //Rectangle
            {
                e.Graphics.FillRectangle(new SolidBrush(iDialogPenColor), 0 + padding, 0 + padding, label4.Width - padding * 2, label4.Height - padding * 2);
            }
            else if (shape == 1)  //Ellipse
            {
                e.Graphics.FillEllipse(new SolidBrush(iDialogPenColor), 0 + padding, 0 + padding, label4.Width - padding * 2, label4.Height - padding * 2);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            hScrollBar1.Value = iDialogPenColor.R;
            hScrollBar2.Value = iDialogPenColor.G;
            hScrollBar3.Value = iDialogPenColor.B;
            textBox1.Text = iDialogPenColor.R.ToString();
            textBox2.Text = iDialogPenColor.G.ToString();
            textBox3.Text = iDialogPenColor.B.ToString();

        }

        private void buttonChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) iDialogShape = 0;
            if (radioButton2.Checked) iDialogShape = 1;
            label4.Invalidate();
        }
    }
}
