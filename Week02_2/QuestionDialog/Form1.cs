using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuestionDialog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 dlg = new Form2();                        //새로운 대화상자 띄울거야
            dlg.ShowDialog();                               //대화상자 띄우고 BLOCK.. BLOCK : Form2 띄우고 Form1은 비활성화(멈춤)
            //ShowDialog메소드 : "이거 대화상자로 쓸거임"      
            //Form2의 버튼이 눌러질 때 Form2 닫고 Form1 다시 활성화시킴
            if (dlg.DialogResult == DialogResult.Yes)       //DialogResult.Yes == (DialogResult)6
            {
                this.Text = "그럼 이제 일해";                 // == Text = "---", this : Form1
            }
            else if (dlg.DialogResult == DialogResult.No)
            {
                Text = "빨리 밥 먹고 와";
            }
            dlg.Dispose();                                  //Dispose(버림) 할 때까지 dlg 인스턴스는 살아있음
        }
    }
}