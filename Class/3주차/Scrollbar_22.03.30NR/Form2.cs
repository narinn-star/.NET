using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrollbar_22._03._30NR
{
    public partial class Form2 : Form
    {
        private Color DialogPenColor;
        public int iDialogPenWidth { get; set; }

        public Color Color
        {
            get
            {
                 return DialogPenColor;
            }
            set
            {
                DialogPenColor = value;
                
            }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            DialogPenColor = Color.FromArgb(hScrollBar1.Value,
                hScrollBar2.Value, hScrollBar3.Value);

            label2.Text = Convert.ToString(hScrollBar1.Value);
            label3.Text = Convert.ToString(hScrollBar2.Value);
            label4.Text = Convert.ToString(hScrollBar3.Value);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            iDialogPenWidth = (((ComboBox)sender).SelectedIndex + 1) * 2;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            iDialogPenWidth = int.Parse(comboBox1.Text);
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


            label2.Text = Convert.ToString(hScrollBar1.Value);
            label3.Text = Convert.ToString(hScrollBar2.Value);
            label4.Text = Convert.ToString(hScrollBar3.Value);
        }
    }
}
