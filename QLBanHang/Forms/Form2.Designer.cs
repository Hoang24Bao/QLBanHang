namespace QLBanHang.Forms
{
    partial class frmHoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            groupBox1 = new GroupBox();
            btnThemKH = new Button();
            txtMaHD = new TextBox();
            cbbMaKH = new ComboBox();
            cbbMaNV = new ComboBox();
            dtpNgayBan = new DateTimePicker();
            txtSDT = new TextBox();
            txtDiaChi = new TextBox();
            txtTenKH = new TextBox();
            txtTenNV = new TextBox();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            groupBox2 = new GroupBox();
            btnDong = new Button();
            btnIn = new Button();
            btnHuy = new Button();
            btnLuu = new Button();
            btnThem = new Button();
            txtTongTien = new TextBox();
            label18 = new Label();
            label17 = new Label();
            label16 = new Label();
            dgvHDBan = new DataGridView();
            txtThanhTien = new TextBox();
            txtDonGia = new TextBox();
            label15 = new Label();
            label14 = new Label();
            txtGiamGia = new TextBox();
            txtTenHang = new TextBox();
            label13 = new Label();
            label12 = new Label();
            cbbMaHang = new ComboBox();
            txtSoLuong = new TextBox();
            label11 = new Label();
            label10 = new Label();
            label19 = new Label();
            cbbTKMaHD = new ComboBox();
            btnTK = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHDBan).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(566, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(358, 45);
            label1.TabIndex = 0;
            label1.Text = "HÓA ĐƠN BÁN HÀNG";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnThemKH);
            groupBox1.Controls.Add(txtMaHD);
            groupBox1.Controls.Add(cbbMaKH);
            groupBox1.Controls.Add(cbbMaNV);
            groupBox1.Controls.Add(dtpNgayBan);
            groupBox1.Controls.Add(txtSDT);
            groupBox1.Controls.Add(txtDiaChi);
            groupBox1.Controls.Add(txtTenKH);
            groupBox1.Controls.Add(txtTenNV);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(42, 57);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1372, 242);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin chung";
            // 
            // btnThemKH
            // 
            btnThemKH.Location = new Point(1246, 182);
            btnThemKH.Name = "btnThemKH";
            btnThemKH.Size = new Size(112, 46);
            btnThemKH.TabIndex = 11;
            btnThemKH.Text = "Thêm mới";
            btnThemKH.UseVisualStyleBackColor = true;
            btnThemKH.Click += btnThemKH_Click;
            // 
            // txtMaHD
            // 
            txtMaHD.Location = new Point(191, 49);
            txtMaHD.Name = "txtMaHD";
            txtMaHD.Size = new Size(394, 34);
            txtMaHD.TabIndex = 3;
            // 
            // cbbMaKH
            // 
            cbbMaKH.FormattingEnabled = true;
            cbbMaKH.Location = new Point(859, 49);
            cbbMaKH.Name = "cbbMaKH";
            cbbMaKH.Size = new Size(357, 36);
            cbbMaKH.TabIndex = 7;
            cbbMaKH.SelectedIndexChanged += cbbMaKH_SelectedIndexChanged;
            // 
            // cbbMaNV
            // 
            cbbMaNV.FormattingEnabled = true;
            cbbMaNV.Location = new Point(191, 146);
            cbbMaNV.Name = "cbbMaNV";
            cbbMaNV.Size = new Size(394, 36);
            cbbMaNV.TabIndex = 5;
            // 
            // dtpNgayBan
            // 
            dtpNgayBan.Format = DateTimePickerFormat.Short;
            dtpNgayBan.Location = new Point(191, 96);
            dtpNgayBan.Name = "dtpNgayBan";
            dtpNgayBan.Size = new Size(394, 34);
            dtpNgayBan.TabIndex = 4;
            // 
            // txtSDT
            // 
            txtSDT.BackColor = Color.White;
            txtSDT.Location = new Point(859, 194);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(357, 34);
            txtSDT.TabIndex = 10;
            txtSDT.TextChanged += txtSDT_TextChanged;
            txtSDT.KeyDown += txtSDT_KeyDown;
            // 
            // txtDiaChi
            // 
            txtDiaChi.BackColor = Color.White;
            txtDiaChi.Location = new Point(859, 146);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(357, 34);
            txtDiaChi.TabIndex = 9;
            // 
            // txtTenKH
            // 
            txtTenKH.BackColor = Color.White;
            txtTenKH.Location = new Point(859, 98);
            txtTenKH.Name = "txtTenKH";
            txtTenKH.Size = new Size(357, 34);
            txtTenKH.TabIndex = 8;
            // 
            // txtTenNV
            // 
            txtTenNV.BackColor = Color.FromArgb(255, 255, 192);
            txtTenNV.Location = new Point(191, 194);
            txtTenNV.Name = "txtTenNV";
            txtTenNV.ReadOnly = true;
            txtTenNV.Size = new Size(394, 34);
            txtTenNV.TabIndex = 6;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(685, 197);
            label9.Name = "label9";
            label9.Size = new Size(102, 28);
            label9.TabIndex = 7;
            label9.Text = "Điện thoại";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(685, 148);
            label8.Name = "label8";
            label8.Size = new Size(71, 28);
            label8.TabIndex = 6;
            label8.Text = "Địa chỉ";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(685, 101);
            label7.Name = "label7";
            label7.Size = new Size(146, 28);
            label7.TabIndex = 5;
            label7.Text = "Tên khách hàng";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(685, 52);
            label6.Name = "label6";
            label6.Size = new Size(145, 28);
            label6.TabIndex = 4;
            label6.Text = "Mã khách hàng";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(34, 197);
            label5.Name = "label5";
            label5.Size = new Size(130, 28);
            label5.TabIndex = 3;
            label5.Text = "Tên nhân viên";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(34, 148);
            label4.Name = "label4";
            label4.Size = new Size(129, 28);
            label4.TabIndex = 2;
            label4.Text = "Mã nhân viên";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(34, 101);
            label3.Name = "label3";
            label3.Size = new Size(97, 28);
            label3.TabIndex = 1;
            label3.Text = "Ngày bán";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 52);
            label2.Name = "label2";
            label2.Size = new Size(118, 28);
            label2.TabIndex = 0;
            label2.Text = "Mã hóa đơn";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnDong);
            groupBox2.Controls.Add(btnIn);
            groupBox2.Controls.Add(btnHuy);
            groupBox2.Controls.Add(btnLuu);
            groupBox2.Controls.Add(btnThem);
            groupBox2.Controls.Add(txtTongTien);
            groupBox2.Controls.Add(label18);
            groupBox2.Controls.Add(label17);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(dgvHDBan);
            groupBox2.Controls.Add(txtThanhTien);
            groupBox2.Controls.Add(txtDonGia);
            groupBox2.Controls.Add(label15);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(txtGiamGia);
            groupBox2.Controls.Add(txtTenHang);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(cbbMaHang);
            groupBox2.Controls.Add(txtSoLuong);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(label10);
            groupBox2.Location = new Point(12, 305);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1436, 552);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Thông tin các mặt hàng";
            // 
            // btnDong
            // 
            btnDong.Location = new Point(1024, 487);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(120, 50);
            btnDong.TabIndex = 28;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            btnDong.Click += btnDong_Click;
            // 
            // btnIn
            // 
            btnIn.Location = new Point(818, 487);
            btnIn.Name = "btnIn";
            btnIn.Size = new Size(120, 50);
            btnIn.TabIndex = 27;
            btnIn.Text = "In";
            btnIn.UseVisualStyleBackColor = true;
            btnIn.Click += btnIn_Click;
            // 
            // btnHuy
            // 
            btnHuy.Location = new Point(620, 487);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(120, 50);
            btnHuy.TabIndex = 26;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(424, 487);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(120, 50);
            btnLuu.TabIndex = 25;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(221, 487);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(120, 50);
            btnThem.TabIndex = 24;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // txtTongTien
            // 
            txtTongTien.Location = new Point(1058, 430);
            txtTongTien.Name = "txtTongTien";
            txtTongTien.Size = new Size(344, 34);
            txtTongTien.TabIndex = 23;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(921, 433);
            label18.Name = "label18";
            label18.Size = new Size(95, 28);
            label18.TabIndex = 22;
            label18.Text = "Tổng tiền";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(16, 470);
            label17.Name = "label17";
            label17.Size = new Size(0, 28);
            label17.TabIndex = 21;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(16, 422);
            label16.Name = "label16";
            label16.Size = new Size(0, 28);
            label16.TabIndex = 20;
            // 
            // dgvHDBan
            // 
            dgvHDBan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHDBan.Location = new Point(6, 156);
            dgvHDBan.Name = "dgvHDBan";
            dgvHDBan.RowHeadersWidth = 62;
            dgvHDBan.Size = new Size(1424, 253);
            dgvHDBan.TabIndex = 19;
            dgvHDBan.CellDoubleClick += dgvHDBan_CellDoubleClick;
            // 
            // txtThanhTien
            // 
            txtThanhTien.BackColor = Color.FromArgb(255, 255, 192);
            txtThanhTien.Location = new Point(1126, 95);
            txtThanhTien.Name = "txtThanhTien";
            txtThanhTien.ReadOnly = true;
            txtThanhTien.Size = new Size(262, 34);
            txtThanhTien.TabIndex = 18;
            // 
            // txtDonGia
            // 
            txtDonGia.BackColor = Color.FromArgb(255, 255, 192);
            txtDonGia.Location = new Point(1126, 44);
            txtDonGia.Name = "txtDonGia";
            txtDonGia.ReadOnly = true;
            txtDonGia.Size = new Size(262, 34);
            txtDonGia.TabIndex = 17;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(987, 98);
            label15.Name = "label15";
            label15.Size = new Size(103, 28);
            label15.TabIndex = 16;
            label15.Text = "Thành tiền";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(987, 47);
            label14.Name = "label14";
            label14.Size = new Size(81, 28);
            label14.TabIndex = 15;
            label14.Text = "Đơn giá";
            // 
            // txtGiamGia
            // 
            txtGiamGia.Location = new Point(620, 95);
            txtGiamGia.Name = "txtGiamGia";
            txtGiamGia.Size = new Size(254, 34);
            txtGiamGia.TabIndex = 14;
            txtGiamGia.TextChanged += txtGiamGia_TextChanged;
            // 
            // txtTenHang
            // 
            txtTenHang.BackColor = Color.FromArgb(255, 255, 192);
            txtTenHang.Location = new Point(620, 44);
            txtTenHang.Name = "txtTenHang";
            txtTenHang.ReadOnly = true;
            txtTenHang.Size = new Size(254, 34);
            txtTenHang.TabIndex = 13;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(484, 98);
            label13.Name = "label13";
            label13.Size = new Size(111, 28);
            label13.TabIndex = 14;
            label13.Text = "Giảm giá %";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(484, 47);
            label12.Name = "label12";
            label12.Size = new Size(90, 28);
            label12.TabIndex = 13;
            label12.Text = "Tên hàng";
            // 
            // cbbMaHang
            // 
            cbbMaHang.FormattingEnabled = true;
            cbbMaHang.Location = new Point(150, 44);
            cbbMaHang.Name = "cbbMaHang";
            cbbMaHang.Size = new Size(241, 36);
            cbbMaHang.TabIndex = 11;
            cbbMaHang.SelectedIndexChanged += cbbMaHang_SelectedIndexChanged;
            // 
            // txtSoLuong
            // 
            txtSoLuong.Location = new Point(150, 95);
            txtSoLuong.Name = "txtSoLuong";
            txtSoLuong.Size = new Size(241, 34);
            txtSoLuong.TabIndex = 12;
            txtSoLuong.TextChanged += txtSoLuong_TextChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(32, 98);
            label11.Name = "label11";
            label11.Size = new Size(92, 28);
            label11.TabIndex = 1;
            label11.Text = "Số lượng";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(32, 47);
            label10.Name = "label10";
            label10.Size = new Size(89, 28);
            label10.TabIndex = 0;
            label10.Text = "Mã hàng";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(28, 873);
            label19.Name = "label19";
            label19.Size = new Size(118, 28);
            label19.TabIndex = 3;
            label19.Text = "Mã hóa đơn";
            // 
            // cbbTKMaHD
            // 
            cbbTKMaHD.DropDownHeight = 80;
            cbbTKMaHD.FormattingEnabled = true;
            cbbTKMaHD.IntegralHeight = false;
            cbbTKMaHD.Location = new Point(174, 870);
            cbbTKMaHD.Name = "cbbTKMaHD";
            cbbTKMaHD.Size = new Size(275, 36);
            cbbTKMaHD.TabIndex = 20;
            // 
            // btnTK
            // 
            btnTK.Location = new Point(487, 862);
            btnTK.Name = "btnTK";
            btnTK.Size = new Size(120, 50);
            btnTK.TabIndex = 5;
            btnTK.Text = "Tìm kiếm";
            btnTK.UseVisualStyleBackColor = true;
            btnTK.Click += btnTK_Click;
            // 
            // frmHoaDon
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1458, 1007);
            Controls.Add(btnTK);
            Controls.Add(cbbTKMaHD);
            Controls.Add(label19);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 163);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmHoaDon";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HOA DON BAN";
            Load += frmHoaDon_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHDBan).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private ComboBox cbbMaNV;
        private DateTimePicker dtpNgayBan;
        private TextBox txtSDT;
        private TextBox txtDiaChi;
        private TextBox txtTenKH;
        private TextBox txtTenNV;
        private ComboBox cbbMaKH;
        private GroupBox groupBox2;
        private Label label15;
        private Label label14;
        private TextBox txtGiamGia;
        private TextBox txtTenHang;
        private Label label13;
        private Label label12;
        private ComboBox cbbMaHang;
        private TextBox txtSoLuong;
        private Label label11;
        private Label label10;
        private TextBox txtTongTien;
        private Label label18;
        private Label label17;
        private Label label16;
        private DataGridView dgvHDBan;
        private TextBox txtThanhTien;
        private TextBox txtDonGia;
        private Button btnDong;
        private Button btnIn;
        private Button btnHuy;
        private Button btnLuu;
        private Button btnThem;
        private Label label19;
        private ComboBox cbbTKMaHD;
        private Button btnTK;
        private TextBox txtMaHD;
        private Button btnThemKH;
    }
}