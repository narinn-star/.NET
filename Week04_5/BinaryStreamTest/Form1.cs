using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BinaryStreamTest
{
    public partial class Form1 : Form
    {
        // 주석 달려있는 부분이 교수님이 설명하신 문제점.
        Human Kim = new Human("김상형", 28);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = Kim.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"c:\Kim.bin", FileMode.Create,
                FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            Kim.Write(bw);
            // 하나씩 다로 접근하려면 아래 Human class에 Name, Age 변수를 public으로 두어야 함.
            //bw.Write(Kim.Name);
            //bw.Write(Kim.Age);
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "파일을 읽는 중";
            label1.Refresh();
            System.Threading.Thread.Sleep(1000);
            FileStream fs = new FileStream(@"c:\Kim.bin", FileMode.Open,
                FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            //Kim = new Human(br.ReadString(), br.ReadInt32());
            Kim = Human.Read(br);
            fs.Close();
            label1.Text = Kim.ToString();
        }
    }

    class Human
    {
        //public string Name;
        //public int Age;
        private string Name;
        private int Age;
        private float Temp;
        public Human(string aName, int aAge)
        {
            Name = aName;
            Age = aAge;
            Temp = 1.23f;
        }
        public override string ToString()
        {
            Temp += 1;
            return "이름 : " + Name + ", 나이:" + Age;
        }
        public void Write(BinaryWriter bw)
        {
            bw.Write(this.Name);
            bw.Write(this.Age);
        }
        public static Human Read(BinaryReader br)
        {
            return new Human(br.ReadString(), br.ReadInt32());
        }
    }
}