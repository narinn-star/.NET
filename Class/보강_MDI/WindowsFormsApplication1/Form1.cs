using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// 반드시 네임스페이스를 명시해야하는 것 주의!
using Emgu.CV;
using Emgu.CV.Structure;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Image<Bgr, byte> _imgInput;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 파일 열기
            OpenFileDialog ofd = new OpenFileDialog();

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                _imgInput = new Image<Bgr, byte>(ofd.FileName); //_imgInput은 우리가 가져온 이미지를 담는 곳
                imageBox1.Image = _imgInput;    //이미 정의되어있는 프로퍼티.
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("종료 하시겠습니까?", "System Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void cannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyCanny();
            return;
        }

        public void ApplyCanny(double thresh = 50.0, double threshLink = 20.0)
        {
            if (_imgInput == null)
            {
                return;
            }
            Image<Gray, byte> _imgCanny = new Image<Gray, byte>(_imgInput.Width, _imgInput.Height, new Gray(0));

            _imgCanny = _imgInput.Canny(thresh, threshLink);
            imageBox1.Image = _imgCanny;
            return;
        }
        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            // 소벨 마스크
            if (_imgInput == null)
            {
                return;
            }
            // Bgr(컬러) -> Gray로 Concvert함
            Image<Gray, byte> _imgGray = _imgInput.Convert<Gray, byte>();   //흑백의 이미지로 바꾸겠다.
            Image<Gray, float> _imgSobel = new Image<Gray, float>(_imgInput.Width, _imgInput.Height, new Gray(0)); //우리가 가져온 이미지랑 동일한 크기로 인스턴스 하나 만듦
            
            //그냥 불러서 사용하면 됨..!
            _imgSobel = _imgGray.Sobel(1,0,3).Add(_imgGray.Sobel(0,1,3)).AbsDiff(new Gray(0.0));    //float이기 때문에 0.0 // 0.0을 기점으로해서 가져오겠다.
            imageBox1.Image = _imgSobel.Convert<Gray, byte>();
            return;
        }


        private void laplacianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 라플라시안 마스크
            if (_imgInput == null)
            {
                return;
            }
            Image<Gray, byte> _imgGray = _imgInput.Convert<Gray, byte>();
            Image<Gray, float> _imgLaplacian = new Image<Gray, float>(_imgInput.Width, _imgInput.Height, new Gray(0));

            //Laplace() 호출만 다르고 소벨과 동일함.
            _imgLaplacian = _imgGray.Laplace(7).AbsDiff(new Gray(0.0));
            imageBox1.Image = _imgLaplacian.Convert<Gray, byte>();
            return;
        }

        private void cannyParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CannyParameters cp = new WindowsFormsApplication1.CannyParameters(this);
            cp.StartPosition = FormStartPosition.CenterParent;
            cp.Show();
        }
    }
}
