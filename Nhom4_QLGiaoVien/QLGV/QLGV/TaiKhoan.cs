using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLGV
{
    public partial class TaiKhoan : Form
    {
        public TaiKhoan()
        {
            InitializeComponent();
        }
        private void TaiKhoan_Load(object sender, EventArgs e)
        {
            LayDSTK();
        }

        private void LayDSTK()
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            try
            {
                con.Open();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "LayDSTK";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Connection = con;
                da.Fill(dt);
                dtgTaiKhoan.DataSource = dt;
                con.Close();
                dtgTaiKhoan.Columns[0].Width = 35;
                dtgTaiKhoan.Columns[0].HeaderText = "ten dang nhap";
                dtgTaiKhoan.Columns[1].Width = 130;
                dtgTaiKhoan.Columns[1].HeaderText = "Giáo viên";
                dtgTaiKhoan.Columns[2].Width = 80;
                dtgTaiKhoan.Columns[2].HeaderText = "mật khẩu";
                dtgTaiKhoan.Columns[3].Width = 80;
                dtgTaiKhoan.Columns[3].HeaderText = "IDPer";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtgTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dtgTaiKhoan.Rows[e.RowIndex];
            txtTenDangNhap.Text = Convert.ToString(row.Cells["tendangnhap"].Value);
            txtGV.Text = Convert.ToString(row.Cells["TenGV"].Value);
            txtMatKhau.Text = Convert.ToString(row.Cells["matkhau"].Value);
           
        }

        private void Reset()
        {
            txtTenDangNhap.Text = "";
            txtGV.Text = "";
            txtMatKhau.Text = "";            
            rbtAdmin.Checked = false;
            rbtUser.Checked = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int n = 0;
            if (int.TryParse(this.txtMatKhau.Text, out n) && txtMatKhau.Text.Length == 8)
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "ThemTK";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tendangnhap", SqlDbType.NVarChar).Value = txtTenDangNhap.Text;
                    cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar).Value = txtGV.Text;
                    cmd.Parameters.Add("@matkhau", SqlDbType.NVarChar).Value = txtMatKhau.Text;
                    if (rbtAdmin.Checked == true)
                    {
                        cmd.Parameters.Add("@IDPer", SqlDbType.Int).Value = 1;
                    }
                    else
                    {
                        cmd.Parameters.Add("@IDPer", SqlDbType.Int).Value = 0;
                    }

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSTK();
                    MessageBox.Show("Thêm mới nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("mật khẩu phải chử số và có 8 ký tự");

            
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            
                if (txtTenDangNhap.Text == "")
                {
                    MessageBox.Show("Vui lòng điền ID nhân viên cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenDangNhap.Focus();
                    txtTenDangNhap.SelectAll();
                }
                else
                {
                    try
                    {
                        SqlConnection conn = new SqlConnection();
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "SuaTK";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@tendangnhap", SqlDbType.NVarChar).Value = txtTenDangNhap.Text;
                        cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar).Value = txtGV.Text;
                        cmd.Parameters.Add("@matkhau", SqlDbType.NVarChar).Value = txtMatKhau.Text;
                        if (rbtAdmin.Checked == true)
                        {
                            cmd.Parameters.Add("@IDPer", SqlDbType.Int).Value = 1;
                        }
                        else
                        {
                            cmd.Parameters.Add("@IDPer", SqlDbType.Int).Value = 0;
                        }

                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        LayDSTK();
                        MessageBox.Show("Sửa tài khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
           

            
            
           
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "")
            {
                MessageBox.Show("Vui lòng điền tên đăng nhập cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenDangNhap.Focus();
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "XoaTK";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tendangnhap", SqlDbType.NVarChar).Value = txtTenDangNhap.Text;

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSTK();
                    MessageBox.Show("Xóa TÀI KHOẢN thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string rowFilter = string.Format("{0} like '{1}'", "TenGV", "*" + txtTimKiem.Text + "*");
            (dtgTaiKhoan.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                LayDSTK();
            }
        }
    }
}
