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
    public partial class Luong : Form
    {
        public Luong()
        {
            InitializeComponent();
        }
        private void Luong_Load(object sender, EventArgs e)
        {
            LayDSL();
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
        private void LayDSL()
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            try
            {
                con.Open();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "LayDSL";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Connection = con;
                da.Fill(dt);
                dtgLuong.DataSource = dt;
                con.Close();
                dtgLuong.Columns[0].Width = 35;
                dtgLuong.Columns[0].HeaderText = "Mã lương";
                dtgLuong.Columns[1].Width = 130;
                dtgLuong.Columns[1].HeaderText = "Giáo viên";
                dtgLuong.Columns[2].Width = 80;
                dtgLuong.Columns[2].HeaderText = "Lương cơ sở";
                dtgLuong.Columns[3].Width = 80;
                dtgLuong.Columns[3].HeaderText = "Hệ số lương";
                dtgLuong.Columns[4].Width = 80;
                dtgLuong.Columns[4].HeaderText = "Phạt";
                dtgLuong.Columns[5].Width = 80;
                dtgLuong.Columns[5].HeaderText = "Thưởng";
                dtgLuong.Columns[6].Width = 80;
                dtgLuong.Columns[6].HeaderText = "Phụ cấp";
                dtgLuong.Columns[7].Width = 80;
                dtgLuong.Columns[7].HeaderText = "Lương";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        

        private void dtgLuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dtgLuong.Rows[e.RowIndex];
            txtMaL.Text = Convert.ToString(row.Cells["MaL"].Value);
            txtGV.Text = Convert.ToString(row.Cells["TenGV"].Value);
            txtLuongCS.Text = Convert.ToString(row.Cells["LuongCoSo"].Value);
            txtPhat.Text = Convert.ToString(row.Cells["Phat"].Value);
            txtThuong.Text = Convert.ToString(row.Cells["Thuong"].Value);
            txtPhuCap.Text = Convert.ToString(row.Cells["Phucap"].Value);
            txtLuong.Text = Convert.ToString(row.Cells["Luong"].Value);
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            string strLCS = "", strHSL = "", strPhat = "", strThuong = "", strPC = "";
            double LCS = 0, Phat = 0, Thuong = 0, PC = 0,luong = 0;
            strLCS = txtLuongCS.Text;
            strHSL= cbHSL.Text;
            strPhat = txtPhat.Text;
            strThuong = txtThuong.Text;
            strPC = txtPhuCap.Text;
            LCS = float.Parse(strLCS);
            Phat = float.Parse(strPhat);    
            Thuong = float.Parse(strThuong);
            PC = float.Parse(strPC);
            switch (strHSL)
            {
                case "Hạng 1: 4.40":
                    luong = LCS * 4.40 - Phat + Thuong + PC;
                    break;
                case "Hạng 1: 4.74":
                    luong = LCS * 4.74 - Phat + Thuong + PC;
                    break;
                case "Hạng 2: 4.0":
                    luong = LCS * 4.0 - Phat + Thuong + PC;
                    break;
                case "Hạng 2: 4.34":
                    luong = LCS * 4.34 - Phat + Thuong + PC;
                    break;
                case "Hạng 3: 2.34":
                    luong = LCS * 2.34 - Phat + Thuong + PC;
                    break;
                case "Hạng 3: 2.67":
                    luong = LCS * 2.67 - Phat + Thuong + PC;
                    break;
            }
            txtLuong.Text = luong.ToString();



        }
        private void Reset()
        {
            txtMaL.Text = "";
            txtGV.Text = "";
            txtLuongCS.Text = "";
            cbHSL.Text = "";
            txtPhat.Text = "";
            txtThuong.Text = "";
            txtPhuCap.Text = "";
            txtLuong.Text = "";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string strHSL = "";
            strHSL = cbHSL.Text;
            double kq = 0;
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ThemL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MaL", SqlDbType.Int).Value = txtMaL.Text;
                cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar).Value = txtGV.Text;
                cmd.Parameters.Add("@LuongCoSo", SqlDbType.Int).Value = txtLuongCS.Text;
                switch (strHSL)
                {
                    case "Hạng 1: 4.40":
                        kq = 4.40;
                        break;
                    case "Hạng 1: 4.74":
                        kq = 4.74;
                        break;
                    case "Hạng 2: 4.0":
                        kq = 4.0;
                        break;
                    case "Hạng 2: 4.34":
                        kq = 4.34;
                        break;
                    case "Hạng 3: 2.34":
                        kq = 2.34;
                        break;
                    case "Hạng 3: 2.67":
                        kq = 2.67;
                        break;
                }
                cmd.Parameters.Add("@HeSoLuong", SqlDbType.Float).Value = kq;
                cmd.Parameters.Add("@Phat", SqlDbType.Int).Value = txtPhat.Text;
                cmd.Parameters.Add("@Thuong", SqlDbType.Int).Value = txtThuong.Text;
                cmd.Parameters.Add("@PhuCap", SqlDbType.Int).Value = txtPhuCap.Text;
                cmd.Parameters.Add("@Luong", SqlDbType.Int).Value = txtLuong.Text;

                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LayDSL();
                MessageBox.Show("Thêm mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaL.Text == "")
            {
                MessageBox.Show("Vui lòng điền mã cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaL.Focus();
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "XoaL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaL", SqlDbType.Int).Value = Convert.ToInt32(txtMaL.Text);
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSL();
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
            string strHSL = "";
            strHSL = cbHSL.Text;
            double kq = 0;
            if (txtMaL.Text == "")
            {
                MessageBox.Show("Vui lòng điền mã kỷ luật cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaL.Focus();
                txtMaL.SelectAll();
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SuaL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaL", SqlDbType.Int).Value = Convert.ToInt32(txtMaL.Text);
                    cmd.Parameters.Add("@TenGV", SqlDbType.NVarChar).Value = txtGV.Text;
                    cmd.Parameters.Add("@LuongCoSo", SqlDbType.Int).Value = txtLuongCS.Text;
                    switch (strHSL)
                    {
                        case "Hạng 1: 4.40":
                            kq = 4.40;
                            break;
                        case "Hạng 1: 4.74":
                            kq = 4.74;
                            break;
                        case "Hạng 2: 4.0":
                            kq = 4.0;
                            break;
                        case "Hạng 2: 4.34":
                            kq = 4.34;
                            break;
                        case "Hạng 3: 2.34":
                            kq = 2.34;
                            break;
                        case "Hạng 3: 2.67":
                            kq = 2.67;
                            break;
                    }
                    cmd.Parameters.Add("@HeSoLuong", SqlDbType.Float).Value = kq;
                    cmd.Parameters.Add("@Phat", SqlDbType.Int).Value = txtPhat.Text;
                    cmd.Parameters.Add("@Thuong", SqlDbType.Int).Value = txtThuong.Text;
                    cmd.Parameters.Add("@PhuCap", SqlDbType.Int).Value = txtPhuCap.Text;
                    cmd.Parameters.Add("@Luong", SqlDbType.Int).Value = txtLuong.Text;


                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSL();
                    MessageBox.Show("Sửa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            (dtgLuong.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                LayDSL();
            }
        }
    }
    
}
