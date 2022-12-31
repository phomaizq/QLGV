using DocumentFormat.OpenXml.Spreadsheet;
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
    public partial class TKB : Form
    {
        public TKB()
        {
            InitializeComponent();
        }
        private void TKB_Load(object sender, EventArgs e)
        {
            LayDSTKB();
            PQ();


        }
        public void PQ()
        {
            if (DangNhap.y == 1)
            {
                btnSua.Enabled = true;
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                btnSua.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled = false;
            }
        }



        private void LayDSTKB()
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            try
            {
                con.Open();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "LayDSTKB";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Connection = con;
                da.Fill(dt);
                dtgTKB.DataSource = dt;
                con.Close();
                dtgTKB.Columns[0].Width = 35;
                dtgTKB.Columns[0].HeaderText = "MaTKB";
                dtgTKB.Columns[1].Width = 130;
                dtgTKB.Columns[1].HeaderText = "Giáo viên";
                dtgTKB.Columns[2].Width = 130;
                dtgTKB.Columns[2].HeaderText = "Tên lớp";
                dtgTKB.Columns[3].Width = 80;
                dtgTKB.Columns[3].HeaderText = "Môn học";
                dtgTKB.Columns[4].Width = 130;
                dtgTKB.Columns[4].HeaderText = "Ngày";
                dtgTKB.Columns[5].Width = 50;
                dtgTKB.Columns[5].HeaderText = "Số Tiết";
                dtgTKB.Columns[6].Width = 50;
                dtgTKB.Columns[6].HeaderText = "Tiết bắt đầu";
                dtgTKB.Columns[7].Width = 80;
                dtgTKB.Columns[7].HeaderText = "Phòng";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Reset()
        {
            txtMaTKB.Text = "";
            txtGV.Text = "";
            txtLop.Text = "";
            txtMonHoc.Text = "";
            txtSoTiet.Text = "";
            txtTietBatDau.Text = "";
            txtPhong.Text = "";
            dtpNgay.Value = DateTime.Now;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ThemTKB";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MaTKB", SqlDbType.NVarChar).Value = txtMaTKB.Text;
                cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar).Value = txtGV.Text;
                cmd.Parameters.Add("@TenLop", SqlDbType.NVarChar).Value = txtLop.Text;
                cmd.Parameters.Add("@MonHoc", SqlDbType.NVarChar).Value = txtMonHoc.Text;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = dtpNgay.Text;
                cmd.Parameters.Add("@SoTiet", SqlDbType.Int).Value = txtSoTiet.Text;
                cmd.Parameters.Add("@TietBatDau", SqlDbType.Int).Value = txtTietBatDau.Text;
                cmd.Parameters.Add("@Phong", SqlDbType.NVarChar).Value = txtPhong.Text;


                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LayDSTKB();
                MessageBox.Show("Thêm mới TKB thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaTKB.Text == "")
            {
                MessageBox.Show("Vui lòng điền mã tkb cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaTKB.Focus();
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "XoaTKB";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaTKB", SqlDbType.Int).Value = Convert.ToInt32(txtMaTKB.Text);
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSTKB();
                    MessageBox.Show("Xóa tkb thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaTKB.Text == "")
            {
                MessageBox.Show("Vui lòng điền mã cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaTKB.Focus();
                txtMaTKB.SelectAll();
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SuaTKB";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaTKB", SqlDbType.Int).Value = Convert.ToInt32(txtMaTKB.Text);
                    cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar).Value = txtGV.Text;
                    cmd.Parameters.Add("@TenLop", SqlDbType.NVarChar).Value = txtLop.Text;
                    cmd.Parameters.Add("@MonHoc", SqlDbType.NVarChar).Value = txtMonHoc.Text;
                    cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = dtpNgay.Text;
                    cmd.Parameters.Add("@SoTiet", SqlDbType.Int).Value = txtSoTiet.Text;
                    cmd.Parameters.Add("@TietBatDau", SqlDbType.Int).Value = txtTietBatDau.Text;
                    cmd.Parameters.Add("@Phong", SqlDbType.NVarChar).Value = txtPhong.Text;


                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSTKB();
                    MessageBox.Show("Sửa tkb thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dtgTKB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dtgTKB.Rows[e.RowIndex];
            txtMaTKB.Text = Convert.ToString(row.Cells["MaTKB"].Value);
            txtGV.Text = Convert.ToString(row.Cells["TenGV"].Value);
            txtLop.Text = Convert.ToString(row.Cells["TenLop"].Value);
            txtMonHoc.Text = Convert.ToString(row.Cells["MonHoc"].Value);
            dtpNgay.Text = Convert.ToString(row.Cells["Ngay"].Value);
            txtSoTiet.Text = Convert.ToString(row.Cells["SoTiet"].Value);
            txtTietBatDau.Text = Convert.ToString(row.Cells["TietBatDau"].Value);
            txtPhong.Text = Convert.ToString(row.Cells["Phong"].Value);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string rowFilter = string.Format("{0} like '{1}'", "TenGV", "*" + txtTimKiem.Text + "*");
            (dtgTKB.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                LayDSTKB();
            }
        }
    }
}
