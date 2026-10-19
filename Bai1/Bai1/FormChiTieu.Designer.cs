namespace Bai1
{
    partial class FormChiTieu
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
            groupBox1 = new GroupBox();
            txtGhiChu = new TextBox();
            label5 = new Label();
            numSoTien = new NumericUpDown();
            label4 = new Label();
            cboLoaiChi = new ComboBox();
            label3 = new Label();
            txtNoiDung = new TextBox();
            label2 = new Label();
            dtpNgayChi = new DateTimePicker();
            label1 = new Label();
            groupBox2 = new GroupBox();
            btnTim = new Button();
            dtpDenNgay = new DateTimePicker();
            label9 = new Label();
            dtpTuNgay = new DateTimePicker();
            label8 = new Label();
            cboLocLoaiChi = new ComboBox();
            label7 = new Label();
            txtTimNoiDung = new TextBox();
            label6 = new Label();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            dgvDanhSach = new DataGridView();
            lblTongChi = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSoTien).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDanhSach).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtGhiChu);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(numSoTien);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(cboLoaiChi);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtNoiDung);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(dtpNgayChi);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(547, 182);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin khoản chi";
            // 
            // txtGhiChu
            // 
            txtGhiChu.Location = new Point(81, 118);
            txtGhiChu.Multiline = true;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(446, 50);
            txtGhiChu.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 121);
            label5.Name = "label5";
            label5.Size = new Size(48, 15);
            label5.TabIndex = 8;
            label5.Text = "Ghi chú";
            // 
            // numSoTien
            // 
            numSoTien.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            numSoTien.Location = new Point(350, 75);
            numSoTien.Maximum = new decimal(new int[] { 1410065407, 2, 0, 0 });
            numSoTien.Name = "numSoTien";
            numSoTien.Size = new Size(177, 23);
            numSoTien.TabIndex = 7;
            numSoTien.ThousandsSeparator = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(289, 78);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 6;
            label4.Text = "Số tiền";
            // 
            // cboLoaiChi
            // 
            cboLoaiChi.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiChi.FormattingEnabled = true;
            cboLoaiChi.Items.AddRange(new object[] { "Ăn uống", "Đi lại", "Học tập", "Giải trí", "Khác" });
            cboLoaiChi.Location = new Point(81, 75);
            cboLoaiChi.Name = "cboLoaiChi";
            cboLoaiChi.Size = new Size(177, 23);
            cboLoaiChi.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 78);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 4;
            label3.Text = "Loại chi";
            // 
            // txtNoiDung
            // 
            txtNoiDung.Location = new Point(81, 35);
            txtNoiDung.Name = "txtNoiDung";
            txtNoiDung.Size = new Size(177, 23);
            txtNoiDung.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 38);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 2;
            label2.Text = "Nội dung";
            // 
            // dtpNgayChi
            // 
            dtpNgayChi.Format = DateTimePickerFormat.Short;
            dtpNgayChi.Location = new Point(350, 35);
            dtpNgayChi.Name = "dtpNgayChi";
            dtpNgayChi.Size = new Size(177, 23);
            dtpNgayChi.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(289, 38);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 0;
            label1.Text = "Ngày chi";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnTim);
            groupBox2.Controls.Add(dtpDenNgay);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(dtpTuNgay);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(cboLocLoaiChi);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(txtTimNoiDung);
            groupBox2.Controls.Add(label6);
            groupBox2.Location = new Point(565, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(407, 182);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Tìm kiếm";
            // 
            // btnTim
            // 
            btnTim.Location = new Point(316, 145);
            btnTim.Name = "btnTim";
            btnTim.Size = new Size(75, 23);
            btnTim.TabIndex = 8;
            btnTim.Text = "Tìm";
            btnTim.UseVisualStyleBackColor = true;
            btnTim.Click += btnTim_Click;
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.Format = DateTimePickerFormat.Short;
            dtpDenNgay.Location = new Point(78, 145);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(147, 23);
            dtpDenNgay.TabIndex = 7;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 151);
            label9.Name = "label9";
            label9.Size = new Size(57, 15);
            label9.TabIndex = 6;
            label9.Text = "Đến ngày";
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.Format = DateTimePickerFormat.Short;
            dtpTuNgay.Location = new Point(78, 110);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(147, 23);
            dtpTuNgay.TabIndex = 5;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 116);
            label8.Name = "label8";
            label8.Size = new Size(49, 15);
            label8.TabIndex = 4;
            label8.Text = "Từ ngày";
            // 
            // cboLocLoaiChi
            // 
            cboLocLoaiChi.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocLoaiChi.FormattingEnabled = true;
            cboLocLoaiChi.Location = new Point(78, 70);
            cboLocLoaiChi.Name = "cboLocLoaiChi";
            cboLocLoaiChi.Size = new Size(313, 23);
            cboLocLoaiChi.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 73);
            label7.Name = "label7";
            label7.Size = new Size(48, 15);
            label7.TabIndex = 2;
            label7.Text = "Loại chi";
            // 
            // txtTimNoiDung
            // 
            txtTimNoiDung.Location = new Point(78, 30);
            txtTimNoiDung.Name = "txtTimNoiDung";
            txtTimNoiDung.PlaceholderText = "Nhập nội dung...";
            txtTimNoiDung.Size = new Size(313, 23);
            txtTimNoiDung.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 33);
            label6.Name = "label6";
            label6.Size = new Size(51, 15);
            label6.TabIndex = 0;
            label6.Text = "TextBox";
            // 
            // btnThem
            // 
            btnThem.Location = new Point(93, 210);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(75, 23);
            btnThem.TabIndex = 2;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(201, 210);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(75, 23);
            btnSua.TabIndex = 3;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(310, 210);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(75, 23);
            btnXoa.TabIndex = 4;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(418, 210);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(75, 23);
            btnLamMoi.TabIndex = 5;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // dgvDanhSach
            // 
            dgvDanhSach.AllowUserToAddRows = false;
            dgvDanhSach.AllowUserToDeleteRows = false;
            dgvDanhSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDanhSach.Location = new Point(12, 250);
            dgvDanhSach.MultiSelect = false;
            dgvDanhSach.Name = "dgvDanhSach";
            dgvDanhSach.ReadOnly = true;
            dgvDanhSach.RowHeadersVisible = false;
            dgvDanhSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSach.Size = new Size(960, 260);
            dgvDanhSach.TabIndex = 6;
            dgvDanhSach.CellClick += dgvDanhSach_CellClick;
            // 
            // lblTongChi
            // 
            lblTongChi.AutoSize = true;
            lblTongChi.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTongChi.ForeColor = Color.Red;
            lblTongChi.Location = new Point(750, 520);
            lblTongChi.Name = "lblTongChi";
            lblTongChi.Size = new Size(130, 21);
            lblTongChi.TabIndex = 7;
            lblTongChi.Text = "Tổng chi: 0 đ";
            // 
            // FormChiTieu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(lblTongChi);
            Controls.Add(dgvDanhSach);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FormChiTieu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sổ chi tiêu cá nhân";
            Load += FormChiTieu_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSoTien).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDanhSach).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private DateTimePicker dtpNgayChi;
        private Label label2;
        private TextBox txtNoiDung;
        private Label label3;
        private ComboBox cboLoaiChi;
        private Label label4;
        private NumericUpDown numSoTien;
        private Label label5;
        private TextBox txtGhiChu;
        private GroupBox groupBox2;
        private TextBox txtTimNoiDung;
        private Label label6;
        private ComboBox cboLocLoaiChi;
        private Label label7;
        private Label label8;
        private DateTimePicker dtpTuNgay;
        private Label label9;
        private DateTimePicker dtpDenNgay;
        private Button btnTim;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private DataGridView dgvDanhSach;
        private Label lblTongChi;
    }
}
