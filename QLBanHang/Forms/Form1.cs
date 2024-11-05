using System.Data.SqlClient;
using QLBanHang.Forms;
using System.Windows.Forms;

namespace QLBanHang
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            string userName = txtTenDN.Text;
            string passWord = txtMK.Text;

            using (SqlConnection connect = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
            {
                connect.Open();
                string sql = "select MaNhanVien, TenNhanVien from tblNhanVien where TenNhanVien = @userName and MatKhau = @passWord";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@passWord", passWord);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Lấy thông tin mã nhân viên và tên nhân viên
                    string maNhanVien = reader["MaNhanVien"].ToString();
                    string tenNhanVien = reader["TenNhanVien"].ToString();

                    frmHoaDon hoaDonForm = new frmHoaDon
                    {
                        MaNhanVien = maNhanVien,
                        TenNhanVien = tenNhanVien
                    };
                    hoaDonForm.Show();
                    this.Hide(); // Ẩn form đăng nhập
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát chương trình không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
