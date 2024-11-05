using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace QLBanHang.Forms
{
    public partial class frmHoaDon : Form
    {
        public string MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public class KhachHang
        {
            public string MaKH { get; set; }
            public string TenKH { get; set; }
            public string DiaChi { get; set; }
        }

        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            cbbMaNV.Text = MaNhanVien;
            txtTenNV.Text = TenNhanVien;

            LoadMaHoaDon();
            LoadKhachHang();
            LoadHangHoa();
        }
        private void LoadMaHoaDon()
        {
            using (SqlConnection connect = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
            {
                connect.Open();
                string sql = "SELECT MaHDBan FROM tblHDBan WHERE NgayBan >= DATEADD(week, -1, GETDATE())";
                SqlCommand cmd = new SqlCommand(sql, connect);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Thêm mã hóa đơn vào ComboBox
                        cbbTKMaHD.Items.Add(reader["MaHDBan"].ToString());
                    }
                }
            }

            // Cho phép nhập mã hóa đơn bất kỳ
            cbbTKMaHD.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbbTKMaHD.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void TongTien()
        {
            string maHDBan = txtMaHD.Text;
            using (SqlConnection connect = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
            {
                connect.Open();
                SqlTransaction transaction = connect.BeginTransaction();
                // Tính tổng thành tiền
                string totalQuery = "SELECT SUM(ThanhTien) FROM tblChiTietHDBan WHERE MaHDBan = @MaHDBan";
                decimal totalAmount = 0;
                using (SqlCommand totalCmd = new SqlCommand(totalQuery, connect, transaction))
                {
                    totalCmd.Parameters.AddWithValue("@MaHDBan", maHDBan);
                    object result = totalCmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        totalAmount = Convert.ToDecimal(result);
                    }
                }
                txtTongTien.Text = totalAmount.ToString("N2"); // Định dạng tổng tiền
            }
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            string maHD = cbbTKMaHD.Text;

            using (SqlConnection connect = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
            {
                connect.Open();
                // Truy vấn để lấy thông tin hóa đơn
                string sql = @"select MaHDBan, NgayBan, tblNhanVien.MaNhanVien, TenNhanVien, tblKhach.MaKhach, TenKhach, DiaChi, SoDienThoai
                from tblHDBan
                join tblNhanVien ON tblHDBan.MaNhanVien = tblNhanVien.MaNhanVien
                join tblKhach ON tblKhach.MaKhach = tblHDBan.MaKhach
                where tblHDBan.MaHDBan = @maHD";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.Parameters.AddWithValue("@maHD", maHD);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Cập nhật thông tin hóa đơn vào các điều khiển
                        txtMaHD.Text = reader["MaHDBan"].ToString();
                        dtpNgayBan.Value = Convert.ToDateTime(reader["NgayBan"]);
                        cbbMaNV.Text = reader["MaNhanVien"].ToString();
                        txtTenNV.Text = reader["TenNhanVien"].ToString();
                        cbbMaKH.Text = reader["MaKhach"].ToString();
                        txtTenKH.Text = reader["TenKhach"].ToString();
                        txtDiaChi.Text = reader["DiaChi"].ToString();
                        txtSDT.Text = reader["SoDienThoai"].ToString();
                        TongTien();
                    }
                    else
                    {
                        // Nếu không tìm thấy hóa đơn, xóa thông tin khỏi các ô
                        txtMaHD.Clear();
                        dtpNgayBan.Value = DateTime.Now; // Hoặc giá trị mặc định khác
                        cbbMaKH.SelectedIndex = -1; // Đặt lại ComboBox khách hàng
                        txtTenKH.Clear();
                        txtDiaChi.Clear();
                        txtSDT.Clear();
                        // Đặt lại mã nhân viên và tên nhân viên
                        cbbMaNV.Text = MaNhanVien;
                        txtTenNV.Text = TenNhanVien;
                        MessageBox.Show("Không tìm thấy hóa đơn trong 1 tuần qua với mã này.");
                    }
                }
            }
            LoadDataToDataGridView(maHD);
        }

        private void LoadKhachHang()
        {
            using (SqlConnection connect = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
            {
                connect.Open();
                string sql = "SELECT MaKhach FROM tblKhach"; // Chọn mã khách
                SqlCommand cmd = new SqlCommand(sql, connect);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Thêm mã khách hàng vào ComboBox
                        cbbMaKH.Items.Add(reader["MaKhach"].ToString());
                    }
                }
            }
            cbbMaKH.SelectedIndex = -1; // Đặt chỉ số của ComboBox về không
        }

        private void cbbMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbMaKH.SelectedItem != null)
            {
                string selectedItem = cbbMaKH.SelectedItem.ToString();
                string maKhach = selectedItem.Split('-')[0].Trim(); // Lấy mã khách

                // Gọi phương thức để cập nhật thông tin khách hàng
                LoadKhachHangDetails(maKhach);
            }
        }

        private void LoadKhachHangDetails(string maKhach)
        {
            using (SqlConnection connect = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
            {
                connect.Open();
                string sql = "SELECT TenKhach, DiaChi, SoDienThoai FROM tblKhach WHERE MaKhach = @MaKhach";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.Parameters.AddWithValue("@MaKhach", maKhach);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Cập nhật thông tin vào các TextBox
                        txtTenKH.Text = reader["TenKhach"].ToString();
                        txtDiaChi.Text = reader["DiaChi"].ToString();
                        txtSDT.Text = reader["SoDienThoai"].ToString();
                    }
                    else
                    {
                        // Nếu không tìm thấy khách hàng, có thể xóa thông tin
                        txtTenKH.Clear();
                        txtDiaChi.Clear();
                        txtSDT.Clear();
                    }
                }
            }
        }

        private void txtSDT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                XuLyThongTinKhachHang();
                e.Handled = true;  // Ngăn không cho Enter tạo ký tự mới
                e.SuppressKeyPress = true;  // Ngăn không cho tiếng beep khi Enter
            }
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }
        private KhachHang TimKiemKhachHangTheoSDT(string sdt)
        {
            KhachHang kh = null;

            using (SqlConnection connect = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
            {
                string sql = "SELECT MaKhach, TenKhach, DiaChi, SoDienThoai FROM tblKhach WHERE SoDienThoai = @SDT";
                SqlCommand command = new SqlCommand(sql, connect);
                command.Parameters.AddWithValue("@SDT", sdt);

                try
                {
                    connect.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        kh = new KhachHang
                        {
                            MaKH = reader["MaKhach"].ToString(),
                            TenKH = reader["TenKhach"].ToString(),
                            DiaChi = reader["DiaChi"].ToString()
                        };
                    }
                    else
                    {

                        reader.Close();

                        SqlCommand cmd = new SqlCommand("SELECT TOP 1 MaKhach FROM tblKhach ORDER BY MaKhach DESC", connect);
                        string maKH = cmd.ExecuteScalar()?.ToString() ?? "KH000";
                        int nextMaKH = int.Parse(maKH.Substring(2)) + 1;
                        cbbMaKH.Text = "KH" + nextMaKH.ToString("D3");
                    }
                    connect.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            return kh;
        }
        private void XuLyThongTinKhachHang()
        {
            string sdt = txtSDT.Text.Trim();

            if (!string.IsNullOrEmpty(sdt))
            {
                // Kiểm tra số điện thoại trong cơ sở dữ liệu
                KhachHang khachHang = TimKiemKhachHangTheoSDT(sdt);

                if (khachHang != null)
                {
                    // Nếu tìm thấy khách hàng
                    cbbMaKH.SelectedValue = khachHang.MaKH;
                    txtTenKH.Text = khachHang.TenKH;
                    txtDiaChi.Text = khachHang.DiaChi;

                    // Tìm mã khách trong ComboBox và chọn nó
                    int index = cbbMaKH.FindString(khachHang.MaKH);
                    if (index >= 0)
                    {
                        cbbMaKH.SelectedIndex = index; // Chọn mã khách hàng trong ComboBox
                    }
                    else
                    {
                        cbbMaKH.SelectedIndex = -1; // Nếu không tìm thấy, đặt lại chỉ số
                    }
                }
                else
                {
                    // Nếu không tìm thấy, cho phép nhập thông tin mới
                    txtTenKH.Clear();
                    txtDiaChi.Clear();
                }
            }
        }
        private void btnThemKH_Click(object sender, EventArgs e)
        {
            KhachHang khach = TimKiemKhachHangTheoSDT(txtSDT.Text);

            if (string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtTenKH.Text) || string.IsNullOrEmpty(cbbMaKH.Text) || string.IsNullOrEmpty(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng!");
            }
            else
            {

                if (!System.Text.RegularExpressions.Regex.IsMatch(txtSDT.Text, @"^0\d{9}$"))
                {
                    MessageBox.Show("Số điện thoại phải có 10 chữ số và bắt đầu bằng chữ số 0.");
                    return;
                }

                else
                {
                    if (khach == null)
                    {
                        using (SqlConnection connect = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
                        {
                            try
                            {
                                connect.Open();
                                string sql = "INSERT INTO tblKhach(MaKhach, TenKhach, DiaChi, SoDienThoai) VALUES(@MaKhach, @TenKhach, @DiaChi, @SoDienThoai)";
                                SqlCommand cmd = new SqlCommand(sql, connect);
                                cmd.Parameters.AddWithValue("@MaKhach", cbbMaKH.Text);
                                cmd.Parameters.AddWithValue("@TenKhach", txtTenKH.Text);
                                cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                                cmd.Parameters.AddWithValue("@SoDienThoai", txtSDT.Text);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Thêm khách hàng thành công!");


                                // Xóa dữ liệu cũ trong ComboBox trước khi tải lại danh sách
                                cbbMaKH.Items.Clear();
                                LoadKhachHang();

                                txtTenKH.Clear();
                                txtDiaChi.Clear();
                                txtSDT.Clear();
                                cbbMaKH.Text = "";
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Khách hàng đã tồn tại!");
                    }
                }
            }
        }


        private void LoadHangHoa()
        {
            using (SqlConnection connect = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
            {
                connect.Open();
                string sql = "SELECT MaHang FROM tblHang"; // Lấy danh sách mã hàng
                SqlCommand cmd = new SqlCommand(sql, connect);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Thêm mã hàng vào ComboBox
                        cbbMaHang.Items.Add(reader["MaHang"].ToString());
                    }
                }
            }
            cbbMaHang.SelectedIndex = -1; // Đặt chỉ số ComboBox về không
        }

        private void cbbMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maHang = cbbMaHang.Text;

            using (SqlConnection connect = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
            {
                connect.Open();
                string sql = @"SELECT TenHang, DonGiaBan FROM tblHang WHERE MaHang = @maHang";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.Parameters.AddWithValue("@maHang", maHang);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Cập nhật thông tin hàng hóa vào các TextBox
                        txtTenHang.Text = reader["TenHang"].ToString();
                        txtDonGia.Text = reader["DonGiaBan"].ToString();
                        CalculateThanhTien();
                    }
                }
            }
        }

        private void ResetForm()
        {
            // Xóa tất cả các TextBox và ComboBox
            txtMaHD.Clear();
            dtpNgayBan.Value = DateTime.Now; // Hoặc giá trị mặc định khác
            cbbMaNV.SelectedIndex = -1; // Đặt lại ComboBox nhân viên
            cbbMaKH.SelectedIndex = -1; // Đặt lại ComboBox khách hàng
            txtTenKH.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            cbbMaHang.SelectedIndex = -1; // Đặt lại ComboBox mặt hàng
            txtTenHang.Clear();
            txtDonGia.Clear();
            cbbTKMaHD.SelectedIndex = -1; // Đặt lại ComboBox tìm kiếm mã hóa đơn
            txtSoLuong.Clear();
            txtGiamGia.Clear();
            txtThanhTien.Clear();
            cbbTKMaHD.SelectedIndex = -1;
            txtTongTien.Clear();

        }
        private string GenerateNewMaHD()
        {
            string newMaHD = "";
            string todayDate = DateTime.Now.ToString("yyyyMMdd"); // Định dạng yymmdd

            using (SqlConnection connect = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
            {
                connect.Open();
                // Truy vấn để đếm số lượng hóa đơn hiện có với cùng ngày
                string sql = @"SELECT COUNT(*) FROM tblHDBan WHERE MaHDBan LIKE 'HDB" + todayDate + "_%'";
                SqlCommand cmd = new SqlCommand(sql, connect);
                int count = (int)cmd.ExecuteScalar();

                // Tạo mã hóa đơn mới
                int newCount = count + 1;
                newMaHD = $"HDB{todayDate}_0{newCount:D3}"; // Định dạng 001, 002, ...
            }

            return newMaHD;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetForm(); // Reset form về trạng thái ban đầu
            txtMaHD.Text = GenerateNewMaHD();
            if (dgvHDBan.DataSource is DataTable dataTable)
            {
                dataTable.Clear();
            }
            else
            {
                dgvHDBan.Rows.Clear();
            }
            cbbMaNV.Text = MaNhanVien;
            txtTenNV.Text = TenNhanVien;
        }

        private void CalculateThanhTien()
        {
            // Kiểm tra xem các trường nhập liệu có hợp lệ không
            if (decimal.TryParse(txtSoLuong.Text, out decimal soLuong) &&
                decimal.TryParse(txtDonGia.Text, out decimal donGia))
            {
                // Nếu txtGiamGia null hoặc rỗng, đặt giá trị mặc định là "0"
                if (string.IsNullOrEmpty(txtGiamGia.Text))
                {
                    txtGiamGia.Text = "0";
                }

                decimal giamGia = 0;
                decimal.TryParse(txtGiamGia.Text, out giamGia);

                // Tính toán thành tiền
                decimal thanhTien = soLuong * donGia;
                if (giamGia > 0)
                {
                    thanhTien -= (soLuong * donGia * (giamGia / 100));
                }

                txtThanhTien.Text = thanhTien.ToString("N0"); // Định dạng thành tiền
            }
            else
            {
                // Nếu dữ liệu không hợp lệ, đặt thành tiền về 0
                txtThanhTien.Text = "0";
            }
        }



        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            CalculateThanhTien();
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            CalculateThanhTien();
        }

        //8. Lưu hóa đơn
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các điều khiển trên form
            string maHDBan = txtMaHD.Text;
            string maNhanVien = cbbMaNV.Text;
            string maKhach = cbbMaKH.Text;
            string tenKhach = txtTenKH.Text;
            string diaChi = txtDiaChi.Text;
            string soDienThoai = txtSDT.Text;
            DateTime ngayBan = dtpNgayBan.Value;
            string tongTien = txtTongTien.Text;
            string maHang = cbbMaHang.Text;


            string thanhTien = txtThanhTien.Text; // TextBox cho Thành Tiền

            if (string.IsNullOrEmpty(maHDBan) || string.IsNullOrEmpty(maKhach) || string.IsNullOrEmpty(tenKhach) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(maHang) || string.IsNullOrEmpty(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hóa đơn!");
                return;
            }
            int soLuong = int.Parse(txtSoLuong.Text);

            // Bắt đầu kết nối và thực hiện lưu trữ
            using (SqlConnection conn = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string checkHDBan = "SELECT COUNT(*) FROM tblHDBan WHERE MaHDBan = @MaHDBan";
                    string checkSL = "SELECT SoLuong FROM tblHang WHERE MaHang = @MaHang";
                    int SL = 0;
                    using (SqlCommand checkSLCmd = new SqlCommand(checkSL, conn, transaction))
                    {
                        checkSLCmd.Parameters.AddWithValue("@MaHang", maHang);
                        object result = checkSLCmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            SL = Convert.ToInt32(result);
                        }
                    }
                    if (SL < soLuong)
                    {
                        MessageBox.Show("Số lượng hàng trong kho không đủ để thực hiện giao dịch. Tồn kho hiện tại: " + SL);
                        return; // Thoát khỏi phương thức nếu không đủ hàng
                    }

                    // Cập nhật số lượng hàng trong tblHang
                    string updateSL = "UPDATE tblHang SET SoLuong = SoLuong - @SoLuong WHERE MaHang = @MaHang";
                    using (SqlCommand updateSLCmd = new SqlCommand(updateSL, conn, transaction))
                    {
                        updateSLCmd.Parameters.AddWithValue("@SoLuong", soLuong);
                        updateSLCmd.Parameters.AddWithValue("@MaHang", maHang);
                        updateSLCmd.ExecuteNonQuery();
                    }

                    string existingMaKhach = null;
                    using (SqlCommand cmd = new SqlCommand(checkHDBan, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@MaHDBan", maHDBan);

                        int count = (int)cmd.ExecuteScalar();

                        if (count == 0)
                        {
                            // Nếu chưa tồn tại, thêm vào bảng tblHDBan
                            string insertHDBanQuery = "INSERT INTO tblHDBan (MaHDBan, MaNhanVien, MaKhach, NgayBan, TongTien) VALUES (@MaHDBan, @MaNhanVien, @MaKhach, @NgayBan, @TongTien)";
                            using (SqlCommand insertCmd = new SqlCommand(insertHDBanQuery, conn, transaction))
                            {
                                insertCmd.Parameters.AddWithValue("@MaHDBan", maHDBan);
                                insertCmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                                insertCmd.Parameters.AddWithValue("@MaKhach", maKhach);
                                insertCmd.Parameters.AddWithValue("@NgayBan", ngayBan);
                                insertCmd.Parameters.AddWithValue("@TongTien", tongTien);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                    if (txtGiamGia.Text == "")
                    {
                        txtGiamGia.Text = "0";
                    }
                    int giamGia = int.Parse(txtGiamGia.Text);
                    // Thêm vào bảng tblChiTietHDBan
                    string insertChiTietQuery = "INSERT INTO tblChiTietHDBan (MaHDBan, MaHang, SoLuong, GiamGia, ThanhTien) VALUES (@MaHDBan, @MaHang, @SoLuong, @GiamGia, @ThanhTien)";
                    using (SqlCommand insertChiTietCmd = new SqlCommand(insertChiTietQuery, conn, transaction))
                    {
                        insertChiTietCmd.Parameters.AddWithValue("@MaHDBan", maHDBan);
                        insertChiTietCmd.Parameters.AddWithValue("@MaHang", maHang);
                        insertChiTietCmd.Parameters.AddWithValue("@SoLuong", soLuong);
                        insertChiTietCmd.Parameters.AddWithValue("@GiamGia", giamGia);
                        insertChiTietCmd.Parameters.AddWithValue("@ThanhTien", thanhTien);
                        insertChiTietCmd.ExecuteNonQuery();
                    }

                    // Tính tổng thành tiền
                    string totalQuery = "SELECT SUM(ThanhTien) FROM tblChiTietHDBan WHERE MaHDBan = @MaHDBan";
                    decimal totalAmount = 0;
                    using (SqlCommand totalCmd = new SqlCommand(totalQuery, conn, transaction))
                    {
                        totalCmd.Parameters.AddWithValue("@MaHDBan", maHDBan);
                        object result = totalCmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            totalAmount = Convert.ToDecimal(result);
                        }
                    }
                    txtTongTien.Text = totalAmount.ToString("N2");

                    // Cập nhật tổng tiền vào tblHDBan
                    string updateTotalQuery = "UPDATE tblHDBan SET TongTien = @TongTien WHERE MaHDBan = @MaHDBan";
                    using (SqlCommand updateTotalCmd = new SqlCommand(updateTotalQuery, conn, transaction))
                    {
                        updateTotalCmd.Parameters.AddWithValue("@TongTien", totalAmount);
                        updateTotalCmd.Parameters.AddWithValue("@MaHDBan", maHDBan);
                        updateTotalCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Lưu hóa đơn thành công!");

                    LoadDataToDataGridView(maHDBan);
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi xảy ra
                    transaction.Rollback();
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
            txtSoLuong.Clear();
            txtGiamGia.Clear();
            txtThanhTien.Clear();
        }

        private void LoadDataToDataGridView(string maHDBan)
        {
            using (SqlConnection connect = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
            {
                try
                {
                    connect.Open();
                    string selectQuery = @"SELECT tblChiTietHDBan.MaHang, TenHang, tblChiTietHDBan.SoLuong, DonGiaBan, GiamGia, ThanhTien 
                        FROM tblChiTietHDBan 
                        JOIN tblHang ON tblChiTietHDBan.MaHang = tblHang.MaHang
                        WHERE MaHDBan = @MaHDBan";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@MaHDBan", maHDBan);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dgvHDBan.DataSource = dataTable; // Cập nhật DataGridView với dữ liệu
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }

        private void dgvHDBan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có phải nhấn đúp vào hàng hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy thông tin mặt hàng từ hàng được chọn
                string maHDBan = txtMaHD.Text; // Mã hóa đơn, cần lưu lại để cập nhật
                string maHang = dgvHDBan.Rows[e.RowIndex].Cells["MaHang"].Value.ToString(); // Giả sử tên cột là "MaHang"
                int soLuong = Convert.ToInt32(dgvHDBan.Rows[e.RowIndex].Cells["SoLuong"].Value); // Lấy số lượng của mặt hàng
                int thanhTien = Convert.ToInt32(dgvHDBan.Rows[e.RowIndex].Cells["ThanhTien"].Value); // Lấy thành tiền của mặt hàng

                // Xóa mặt hàng khỏi tblChiTietHDBan
                using (SqlConnection conn = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True"))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Xóa mặt hàng khỏi tblChiTietHDBan
                        string deleteQuery = "DELETE FROM tblChiTietHDBan WHERE MaHDBan = @MaHDBan AND MaHang = @MaHang";
                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@MaHDBan", maHDBan);
                            deleteCmd.Parameters.AddWithValue("@MaHang", maHang);
                            deleteCmd.ExecuteNonQuery();
                        }

                        // Cập nhật lại TongTien cho tblHDBan
                        string updateTongTienQuery = "UPDATE tblHDBan SET TongTien = TongTien - @ThanhTien WHERE MaHDBan = @MaHDBan";
                        using (SqlCommand updateCmd = new SqlCommand(updateTongTienQuery, conn, transaction))
                        {
                            updateCmd.Parameters.AddWithValue("@ThanhTien", thanhTien);
                            updateCmd.Parameters.AddWithValue("@MaHDBan", maHDBan);
                            updateCmd.ExecuteNonQuery();
                        }

                        // Hoàn lại số lượng hàng vào tblHang
                        string updateStockQuery = "UPDATE tblHang SET SoLuong = SoLuong + @SoLuong WHERE MaHang = @MaHang";
                        using (SqlCommand updateStockCmd = new SqlCommand(updateStockQuery, conn, transaction))
                        {
                            updateStockCmd.Parameters.AddWithValue("@SoLuong", soLuong);
                            updateStockCmd.Parameters.AddWithValue("@MaHang", maHang);
                            updateStockCmd.ExecuteNonQuery();
                        }

                        // Commit transaction
                        transaction.Commit();
                        MessageBox.Show("Mặt hàng đã được xóa thành công!");

                        // Cập nhật lại DataGridView
                        LoadDataToDataGridView(maHDBan);

                        // Lấy tổng tiền mới từ CSDL và cập nhật vào txtTongTien
                        string selectTongTienQuery = "SELECT TongTien FROM tblHDBan WHERE MaHDBan = @MaHDBan";
                        using (SqlCommand selectCmd = new SqlCommand(selectTongTienQuery, conn))
                        {
                            selectCmd.Parameters.AddWithValue("@MaHDBan", maHDBan);
                            object result = selectCmd.ExecuteScalar();
                            if (result != null && decimal.TryParse(result.ToString(), out decimal tongTien))
                            {
                                txtTongTien.Text = tongTien.ToString("N2");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction nếu có lỗi xảy ra
                        transaction.Rollback();
                        MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            string maHDBan = txtMaHD.Text; // Lấy mã hóa đơn cần xóa

            using (SqlConnection conn = new SqlConnection("Data source=LAPTOP-HBN2311\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True;MultipleActiveResultSets=True"))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Lấy tất cả mặt hàng trong tblChiTietHDBan liên quan đến hóa đơn này
                    string selectChiTietQuery = "SELECT MaHang, SoLuong FROM tblChiTietHDBan WHERE MaHDBan = @MaHDBan";
                    using (SqlCommand selectCmd = new SqlCommand(selectChiTietQuery, conn, transaction))
                    {
                        selectCmd.Parameters.AddWithValue("@MaHDBan", maHDBan);

                        using (SqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            // Duyệt qua từng mặt hàng và hoàn lại số lượng vào tblHang
                            while (reader.Read())
                            {
                                string maHang = reader["MaHang"].ToString();
                                int soLuong = Convert.ToInt32(reader["SoLuong"]);

                                string updateStockQuery = "UPDATE tblHang SET SoLuong = SoLuong + @SoLuong WHERE MaHang = @MaHang";
                                using (SqlCommand updateStockCmd = new SqlCommand(updateStockQuery, conn, transaction))
                                {
                                    updateStockCmd.Parameters.AddWithValue("@SoLuong", soLuong);
                                    updateStockCmd.Parameters.AddWithValue("@MaHang", maHang);
                                    updateStockCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    // Xóa các chi tiết của hóa đơn trong tblChiTietHDBan
                    string deleteChiTietQuery = "DELETE FROM tblChiTietHDBan WHERE MaHDBan = @MaHDBan";
                    using (SqlCommand deleteChiTietCmd = new SqlCommand(deleteChiTietQuery, conn, transaction))
                    {
                        deleteChiTietCmd.Parameters.AddWithValue("@MaHDBan", maHDBan);
                        deleteChiTietCmd.ExecuteNonQuery();
                    }

                    // Xóa hóa đơn trong tblHDBan
                    string deleteHDBanQuery = "DELETE FROM tblHDBan WHERE MaHDBan = @MaHDBan";
                    using (SqlCommand deleteHDBanCmd = new SqlCommand(deleteHDBanQuery, conn, transaction))
                    {
                        deleteHDBanCmd.Parameters.AddWithValue("@MaHDBan", maHDBan);
                        deleteHDBanCmd.ExecuteNonQuery();
                    }

                    // Commit transaction nếu tất cả lệnh đều thành công
                    transaction.Commit();
                    MessageBox.Show("Hóa đơn đã được hủy thành công!");

                    // Cập nhật lại giao diện sau khi hủy
                    ResetForm(); // Nếu có phương thức reset form
                    LoadDataToDataGridView(maHDBan); // Cập nhật lại DataGridView nếu có
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi xảy ra
                    transaction.Rollback();
                    MessageBox.Show("Có lỗi xảy ra khi hủy hóa đơn: " + ex.Message);
                }
            }
        }

        //11. In hóa đơn
        private void btnIn_Click(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();

            Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];
            Excel.Range exRange = (Excel.Range)exSheet.Cells[1, 1];
            exRange.Font.Size = 15;
            exRange.Font.Bold = true;
            exRange.Font.Color = Color.Blue;
            exRange.Value = "HÓA ĐƠN BÁN HÀNG";

            Excel.Range dc = (Excel.Range)exSheet.Cells[2, 1];
            dc.Font.Size = 13;
            dc.Font.Color = Color.Blue;
            dc.Value = "ĐHGTVT - Hà Nội";

            //In chữ Hóa đơn bán
            exSheet.Range["D4"].Font.Size = 20;
            exSheet.Range["D4"].Font.Bold = true;
            exSheet.Range["D4"].Font.Color = Color.Red;
            exSheet.Range["D4"].Value = "HÓA ĐƠN BÁN HÀNG";

            //In các Thông tin chung
            exSheet.Range["A5:A8"].Font.Size = 12;
            exSheet.Range["A5"].Value = "Mã hóa đơn: " + txtMaHD.Text;
            exSheet.Range["A6"].Value = "Khách hàng: " + cbbMaKH.SelectedValue.ToString() + "-" + txtTenKH.Text;
            exSheet.Range["A7"].Value = "Địa chỉ: " + txtDiaChi.Text;
            exSheet.Range["A8"].Value = "Điện thoại: " + txtSDT.Text;

            //In dòng tiêu đề
            exSheet.Range["A10:G10"].Font.Size = 12;
            exSheet.Range["A10:G10"].Font.Bold = true;
            exSheet.Range["C10"].ColumnWidth = 25;
            exSheet.Range["G10"].ColumnWidth = 25;
            exSheet.Range["E10"].ColumnWidth = 20;
            exSheet.Range["F10"].ColumnWidth = 20;
            exSheet.Range["A10"].Value = "STT";
            exSheet.Range["B10"].Value = "Mã hàng";
            exSheet.Range["C10"].Value = "Tên hàng";
            exSheet.Range["D10"].Value = "Số lượng";
            exSheet.Range["E10"].Value = "Đơn giá bán";
            exSheet.Range["F10"].Value = "Giảm giá";
            exSheet.Range["G10"].Value = "Thành tiền";

            //In ds chi tiết các mặt hàng trong hóa đơn
            int dong = 11;
            for(int i = 0; i < dgvHDBan.Rows.Count-1; i++)
            {
                exSheet.Range["A" + (dong + i).ToString()].Value = (i+1).ToString();
                exSheet.Range["B" + (dong + i).ToString()].Value = dgvHDBan.Rows[i].Cells[0].Value.ToString();
                exSheet.Range["C" + (dong + i).ToString()].Value = dgvHDBan.Rows[i].Cells[1].Value.ToString();
                exSheet.Range["D" + (dong + i).ToString()].Value = dgvHDBan.Rows[i].Cells[2].Value.ToString();
                exSheet.Range["E" + (dong + i).ToString()].Value = dgvHDBan.Rows[i].Cells[3].Value.ToString();
                exSheet.Range["F" + (dong + i).ToString()].Value = dgvHDBan.Rows[i].Cells[4].Value.ToString() + "%";
                exSheet.Range["G" + (dong + i).ToString()].Value = dgvHDBan.Rows[i].Cells[5].Value.ToString();
            }
            dong = dong + dgvHDBan.Rows.Count;
            exSheet.Range["F" + dong.ToString()].Value = "Tổng tiền: " + txtTongTien.Text + " đồng";
            exSheet.Name = txtMaHD.Text;
            exBook.Activate();

            //Lưu file
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Excel file Workbook|*.xls|Excel Workbook| *.xlsx|All Files|*.*";
            save.FilterIndex = 2;
            if (save.ShowDialog() == DialogResult.OK)
            {
                exBook.SaveAs(save.FileName.ToLower());
                MessageBox.Show("Đã lưu file thành công!");
            }
            exApp.Quit();
        }


        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
