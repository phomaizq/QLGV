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
    public partial class Thuong : Form
    {
        public Thuong()
        {
            InitializeComponent();
        }
        private void Thuong_Load(object sender, EventArgs e)
        {
            LayDST();
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
        private void LayDST()
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            try
            {
                con.Open();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "LayDST";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Connection = con;
                da.Fill(dt);
                dtgThuong.DataSource = dt;
                con.Close();
                dtgThuong.Columns[0].Width = 35;
                dtgThuong.Columns[0].HeaderText = "Ma Thưởng";
                dtgThuong.Columns[1].Width = 130;
                dtgThuong.Columns[1].HeaderText = "Giáo viên";
                dtgThuong.Columns[2].Width = 130;
                dtgThuong.Columns[2].HeaderText = "Lý do";
                dtgThuong.Columns[3].Width = 80;
                dtgThuong.Columns[3].HeaderText = "Ngày thưởng";
                dtgThuong.Columns[4].Width = 130;
                dtgThuong.Columns[4].HeaderText = "Thưởng";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtgThuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dtgThuong.Rows[e.RowIndex];
            txtMT.Text = Convert.ToString(row.Cells["MaThuong"].Value);
            txtGV.Text = Convert.ToString(row.Cells["TenGV"].Value);
            txtLyDo.Text = Convert.ToString(row.Cells["LyDo"].Value);
            dtpNgay.Text = Convert.ToString(row.Cells["NgayKhenThuong"].Value);
            txtThuong.Text = Convert.ToString(row.Cells["Thuong"].Value);
        }
        private void Reset()
        {
            txtMT.Text = "";
            txtGV.Text = "";
            txtLyDo.Text = "";
            txtThuong.Text = "";
            dtpNgay.Value = DateTime.Now;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ThemThuong";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MaThuong", SqlDbType.NVarChar).Value = txtMT.Text;
                cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar).Value = txtGV.Text;
                cmd.Parameters.Add("@Lydo", SqlDbType.NVarChar).Value = txtLyDo.Text;
                cmd.Parameters.Add("@NgayKhenThuong", SqlDbType.Date).Value = dtpNgay.Text;
                cmd.Parameters.Add("@Thuong", SqlDbType.NVarChar).Value = txtThuong.Text;

                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LayDST();
                MessageBox.Show("Thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMT.Text == "")
            {
                MessageBox.Show("Vui lòng điền mã thưởng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMT.Focus();
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "XoaThuong1";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaThuong", SqlDbType.Int).Value = Convert.ToInt32(txtMT.Text);
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDST();
                    MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (txtThuong.Text == "")
            {
                MessageBox.Show("Vui lòng điền mã thưởng cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtThuong.Focus();
                txtThuong.SelectAll();
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SuaThuong1";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaThuong", SqlDbType.Int).Value = Convert.ToInt32(txtMT.Text);
                    cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar).Value = txtGV.Text;
                    cmd.Parameters.Add("@LyDo", SqlDbType.NVarChar).Value = txtLyDo.Text;

                    cmd.Parameters.Add("@NgayKhenThuong", SqlDbType.Date).Value = dtpNgay.Text;
                    cmd.Parameters.Add("@Thuong", SqlDbType.NVarChar).Value = txtThuong.Text;


                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDST();
                    MessageBox.Show("Sửa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
