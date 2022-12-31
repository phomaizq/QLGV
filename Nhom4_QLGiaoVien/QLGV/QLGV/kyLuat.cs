using DocumentFormat.OpenXml.Wordprocessing;
using QLGV;
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
    public partial class kyLuat : Form
    {

        public kyLuat()
        {
            InitializeComponent();
        }

        private void kyLuat_Load(object sender, EventArgs e)
        {
            LayDSKL();
            PQ();
            
        }
        public void PQ()
        {
            if (DangNhap.y ==1)
            {
                btnSua.Enabled = true;
                btnThem.Enabled = true;
                btnXoa.Enabled = true;  
            }
            else
            {
                btnSua.Enabled = false;
                btnThem.Enabled = false;
                btnXoa.Enabled=false;   
            }
        }
        
        private void LayDSKL()
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            try
            {
                con.Open();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "LayDSKL";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Connection = con;
                da.Fill(dt);
                dtgDSKL.DataSource = dt;
                con.Close();
                dtgDSKL.Columns[0].Width = 35;
                dtgDSKL.Columns[0].HeaderText = "MaKL";
                dtgDSKL.Columns[1].Width = 130;
                dtgDSKL.Columns[1].HeaderText = "Giáo viên";
                dtgDSKL.Columns[2].Width = 130;
                dtgDSKL.Columns[2].HeaderText = "Lý do";
                dtgDSKL.Columns[3].Width = 80;
                dtgDSKL.Columns[3].HeaderText = "Ngày kỷ luật";
                dtgDSKL.Columns[4].Width = 130;
                dtgDSKL.Columns[4].HeaderText = "Phạt";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Reset()
        {
            txtMKL.Text = "";
            txtGV.Text = "";
            txtLyDo.Text = "";
            txtPhat.Text = "";
            dtbNgay.Value = DateTime.Now;
        }


        private void btnThem_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ThemKL1";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MaKL", SqlDbType.NVarChar).Value = txtMKL.Text;
                cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar).Value = txtGV.Text;
                cmd.Parameters.Add("@Lido", SqlDbType.NVarChar).Value = txtLyDo.Text;
                cmd.Parameters.Add("@NgayKyLuat", SqlDbType.Date).Value = dtbNgay.Text;
                cmd.Parameters.Add("@Phat", SqlDbType.NVarChar).Value = txtPhat.Text;

                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LayDSKL();
                MessageBox.Show("Thêm mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtgDSKL_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dtgDSKL.Rows[e.RowIndex];
            txtMKL.Text = Convert.ToString(row.Cells["MaKL"].Value);
            txtGV.Text = Convert.ToString(row.Cells["TenGV"].Value);
            txtLyDo.Text = Convert.ToString(row.Cells["LiDo"].Value);
            dtbNgay.Text = Convert.ToString(row.Cells["NgayKyLuat"].Value);
            txtPhat.Text = Convert.ToString(row.Cells["Phat"].Value);

        }

        
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMKL.Text == "")
            {
                MessageBox.Show("Vui lòng điền mã kỷ luật cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMKL.Focus();
                txtMKL.SelectAll();
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SuaKL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaKL", SqlDbType.Int).Value = Convert.ToInt32(txtMKL.Text);
                    cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar).Value = txtGV.Text;
                    cmd.Parameters.Add("@LiDo", SqlDbType.NVarChar).Value = txtLyDo.Text;

                    cmd.Parameters.Add("@NgayKyLuat", SqlDbType.Date).Value = dtbNgay.Text;
                    cmd.Parameters.Add("@Phat", SqlDbType.NVarChar).Value = txtPhat.Text;


                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSKL();
                    MessageBox.Show("Sửa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (txtMKL.Text == "")
            {
                MessageBox.Show("Vui lòng điền mã kỹ luật cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMKL.Focus();
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "XoaKL1";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaKL", SqlDbType.Int).Value = Convert.ToInt32(txtMKL.Text);
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSKL();
                    MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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



