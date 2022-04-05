using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week03_1
{
    public partial class Form2 : Form
    {
        private Color iDialogColor;
        public int iDialogWidth { get; set; }

        public Color Color
        {
            get
            {
                if (radioButton1.Checked) iDialogColor = Color.Red;
                if (radioButton2.Checked) iDialogColor = Color.Green;
                if (radioButton3.Checked) iDialogColor = Color.Blue;
                return iDialogColor;
            }
            set
            {
                iDialogColor = value;
                if (iDialogColor == Color.Red) radioButton1.Checked = true;
                if (iDialogColor == Color.Green) radioButton2.Checked = true;
                if (iDialogColor == Color.Blue) radioButton3.Checked = true;
            }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            iDialogWidth = int.Parse(comboBox1.Text);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            iDialogWidth = int.Parse(comboBox1.Text);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 2; i <= 10; i+= 2)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.Text = iDialogWidth.ToString();
        }
    }
}
