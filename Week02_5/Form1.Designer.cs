﻿
namespace Week02_5
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.메뉴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.사각형ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.원형ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.자유곡ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.대화상자ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.메뉴ToolStripMenuItem,
            this.대화상자ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 메뉴ToolStripMenuItem
            // 
            this.메뉴ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.사각형ToolStripMenuItem,
            this.원형ToolStripMenuItem,
            this.자유곡ToolStripMenuItem});
            this.메뉴ToolStripMenuItem.Name = "메뉴ToolStripMenuItem";
            this.메뉴ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.메뉴ToolStripMenuItem.Text = "메뉴";
            // 
            // 사각형ToolStripMenuItem
            // 
            this.사각형ToolStripMenuItem.Name = "사각형ToolStripMenuItem";
            this.사각형ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.사각형ToolStripMenuItem.Text = "Red";
            this.사각형ToolStripMenuItem.Click += new System.EventHandler(this.사각형ToolStripMenuItem_Click);
            // 
            // 원형ToolStripMenuItem
            // 
            this.원형ToolStripMenuItem.Name = "원형ToolStripMenuItem";
            this.원형ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.원형ToolStripMenuItem.Text = "Green";
            this.원형ToolStripMenuItem.Click += new System.EventHandler(this.원형ToolStripMenuItem_Click);
            // 
            // 자유곡ToolStripMenuItem
            // 
            this.자유곡ToolStripMenuItem.Name = "자유곡ToolStripMenuItem";
            this.자유곡ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.자유곡ToolStripMenuItem.Text = "Blue";
            this.자유곡ToolStripMenuItem.Click += new System.EventHandler(this.자유곡ToolStripMenuItem_Click);
            // 
            // 대화상자ToolStripMenuItem
            // 
            this.대화상자ToolStripMenuItem.Name = "대화상자ToolStripMenuItem";
            this.대화상자ToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.대화상자ToolStripMenuItem.Text = "대화상자";
            this.대화상자ToolStripMenuItem.Click += new System.EventHandler(this.대화상자ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 메뉴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 사각형ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 원형ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 자유곡ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 대화상자ToolStripMenuItem;
    }
}

