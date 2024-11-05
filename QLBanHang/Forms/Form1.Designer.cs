namespace QLBanHang
{
    partial class frmDangNhap
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            txtTenDN = new TextBox();
            txtMK = new TextBox();
            btnDN = new Button();
            btnThoat = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(177, 157);
            label1.Name = "label1";
            label1.Size = new Size(174, 32);
            label1.TabIndex = 0;
            label1.Text = "Tên đăng nhập";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(177, 275);
            label2.Name = "label2";
            label2.Size = new Size(115, 32);
            label2.TabIndex = 1;
            label2.Text = "Mật khẩu";
            // 
            // txtTenDN
            // 
            txtTenDN.Location = new Point(432, 154);
            txtTenDN.Name = "txtTenDN";
            txtTenDN.Size = new Size(487, 39);
            txtTenDN.TabIndex = 2;
            // 
            // txtMK
            // 
            txtMK.Location = new Point(432, 272);
            txtMK.Name = "txtMK";
            txtMK.PasswordChar = '*';
            txtMK.Size = new Size(487, 39);
            txtMK.TabIndex = 3;
            // 
            // btnDN
            // 
            btnDN.Location = new Point(356, 448);
            btnDN.Name = "btnDN";
            btnDN.Size = new Size(140, 60);
            btnDN.TabIndex = 4;
            btnDN.Text = "Đăng nhập";
            btnDN.UseVisualStyleBackColor = true;
            btnDN.Click += btnDN_Click;
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(681, 448);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(140, 60);
            btnThoat.TabIndex = 5;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // frmDangNhap
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1152, 586);
            Controls.Add(btnThoat);
            Controls.Add(btnDN);
            Controls.Add(txtMK);
            Controls.Add(txtTenDN);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 163);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtTenDN;
        private TextBox txtMK;
        private Button btnDN;
        private Button btnThoat;
    }
}
