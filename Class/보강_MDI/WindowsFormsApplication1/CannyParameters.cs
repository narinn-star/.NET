using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class CannyParameters : Form
    {
        Form1 _form;
        public CannyParameters()
        {
            InitializeComponent();
        }
        public CannyParameters(Form1 fm)
        {
            InitializeComponent();
            _form = fm;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(_form!=null)
            {
                _form.ApplyCanny((double)numericThreshold.Value,(double)numericThresholdLink.Value);
            }
        }
    }
}
