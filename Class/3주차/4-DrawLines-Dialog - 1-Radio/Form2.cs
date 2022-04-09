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
        private int DialogWidth;

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

        public int Width
        {
            get
            {
                DialogWidth = comboBox1.SelectedIndex;
                return DialogWidth;
            }
            set
            {
                DialogWidth = value;
                comboBox1.SelectedIndex = DialogWidth;
            }
        }

        public Form2()
        {
            InitializeComponent();
            comboBox1.Items.Add("2");
            comboBox1.Items.Add("4");
            comboBox1.Items.Add("6");
            comboBox1.Items.Add("8");
            comboBox1.Items.Add("10");
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DialogWidth = Convert.ToInt32(comboBox1.SelectedItem);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            DialogWidth = Convert.ToInt32(comboBox1.Text);
        }
    }
}
