using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week02_5
{
    public partial class Form2 : Form
    {
        private Color iDialogColor;
        
        public Color Color
        {
            get
            {
                if (radioButton1.Checked) iDialogColor = Color.FromArgb(255, 0, 0);
                if (radioButton2.Checked) iDialogColor = Color.FromArgb(0, 255, 0);
                if (radioButton3.Checked) iDialogColor = Color.FromArgb(0, 0, 255);
                return iDialogColor;
            }
            set
            {
                iDialogColor = value;
                if (iDialogColor == Color.FromArgb(255, 0, 0)) radioButton1.Checked = true;
                if (iDialogColor == Color.FromArgb(0, 255, 0)) radioButton2.Checked = true;
                if (iDialogColor == Color.FromArgb(0, 0, 255)) radioButton3.Checked = true;
            }
        }
        public Form2()
        {
            InitializeComponent();
        }
    }
}
