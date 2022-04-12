using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDIPractice_NR
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public Image image { get; set; }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, 0, 0, image.Width, image.Height);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(image.Width, image.Height);  //Form2에 여는 이미지 크기에 맞게 폼 크기를 바로 조절해서 띄움
            //this.Size = new Size(image.Width, image.Height);      //폼 테두리까지 포함한 Size를 말하기 때문에 그림이 조금씩 잘려나옴.
        }
    }
}
