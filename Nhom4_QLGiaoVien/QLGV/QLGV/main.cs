using QLGV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLGV
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        bool isthoat = true;
        private void main_Load(object sender, EventArgs e)
        {
            PQ();
        }

        public void PQ()
        {
            if (DangNhap.y == 1)
            {
                btnTaiKhoan.Enabled = false;
            }
            else
            {
                btnTaiKhoan.Enabled = true;    
            }
            
        }
        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.MdiParent = this;
            frm.Name = "Form1";
            frm.Show();
        }

        private void kỷLuậtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kyLuat frm = new kyLuat();
            frm.MdiParent = this;
            frm.Name = "kyLuat";
            frm.Show();
        }

        private void thưởngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thuong frm = new Thuong();
            frm.MdiParent = this;
            frm.Name = "Thuong";
            frm.Show();
        }

        private void tKBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TKB frm = new TKB();
            frm.MdiParent = this;
            frm.Name = "TKB";
            frm.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có chắc muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                isthoat = false;
                this.Close();
                DangNhap f = new DangNhap();    
                f.Show();
                

            }
        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(isthoat)
                Application.Exit();
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            TaiKhoan TK = new TaiKhoan();   
            TK.Show();
        }

        private void lươngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Luong frm = new Luong();
            frm.MdiParent = this;
            frm.Name = "Luong";
            frm.Show();
        }
    }
}
