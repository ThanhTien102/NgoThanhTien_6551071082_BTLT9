using Bai1.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Data;

namespace Bai1
{
    public partial class FormChiTieu : Form
    {
        private readonly QLChiTieuContext _context;
        private int _selectedMaCt = -1;

        public FormChiTieu(QLChiTieuContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private async void FormChiTieu_Load(object sender, EventArgs e)
        {
            try
            {
                // Load loại chi vào combobox lọc
                cboLocLoaiChi.Items.Add("Tất cả");
                cboLocLoaiChi.Items.AddRange(new object[] { "Ăn uống", "Đi lại", "Học tập", "Giải trí", "Khác" });
                cboLocLoaiChi.SelectedIndex = 0;

                // Cấu hình mặc định cho ngày lọc
                dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpDenNgay.Value = DateTime.Now;

                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi khi tải dữ liệu ban đầu");
                MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu. Vui lòng kiểm tra cấu hình.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var query = _context.ChiTieus.AsQueryable();

                // Lọc theo nội dung
                if (!string.IsNullOrWhiteSpace(txtTimNoiDung.Text))
                {
                    query = query.Where(c => c.NoiDung.Contains(txtTimNoiDung.Text));
                }

                // Lọc theo loại chi
                if (cboLocLoaiChi.SelectedIndex > 0 && cboLocLoaiChi.SelectedItem != null)
                {
                    string loaiChi = cboLocLoaiChi.SelectedItem.ToString() ?? "";
                    query = query.Where(c => c.LoaiChi == loaiChi);
                }

                // Lọc theo khoảng thời gian
                DateOnly tuNgay = DateOnly.FromDateTime(dtpTuNgay.Value);
                DateOnly denNgay = DateOnly.FromDateTime(dtpDenNgay.Value);
                query = query.Where(c => c.NgayChi >= tuNgay && c.NgayChi <= denNgay);

                var list = await query.OrderByDescending(c => c.NgayChi).ToListAsync();

                dgvDanhSach.DataSource = list;
                FormatGrid();
                UpdateTotal(list);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi khi truy vấn dữ liệu chi tiêu");
                MessageBox.Show("Đã xảy ra lỗi khi tải danh sách chi tiêu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            if (dgvDanhSach.Columns["MaCt"] != null) dgvDanhSach.Columns["MaCt"].HeaderText = "Mã";
            if (dgvDanhSach.Columns["NgayChi"] != null) dgvDanhSach.Columns["NgayChi"].HeaderText = "Ngày chi";
            if (dgvDanhSach.Columns["NoiDung"] != null) dgvDanhSach.Columns["NoiDung"].HeaderText = "Nội dung";
            if (dgvDanhSach.Columns["LoaiChi"] != null) dgvDanhSach.Columns["LoaiChi"].HeaderText = "Loại chi";
            if (dgvDanhSach.Columns["SoTien"] != null)
            {
                dgvDanhSach.Columns["SoTien"].HeaderText = "Số tiền";
                dgvDanhSach.Columns["SoTien"].DefaultCellStyle.Format = "N0";
            }
            if (dgvDanhSach.Columns["GhiChu"] != null) dgvDanhSach.Columns["GhiChu"].HeaderText = "Ghi chú";
        }

        private void UpdateTotal(List<ChiTieu> list)
        {
            decimal total = list.Sum(c => c.SoTien);
            lblTongChi.Text = $"Tổng chi: {total:N0} đ";
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            _ = LoadDataAsync();
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvDanhSach.Rows[e.RowIndex];
                if (row.Cells["MaCt"].Value != null)
                    _selectedMaCt = (int)row.Cells["MaCt"].Value;
                
                if (row.Cells["NgayChi"].Value != null)
                    dtpNgayChi.Value = ((DateOnly)row.Cells["NgayChi"].Value).ToDateTime(TimeOnly.MinValue);
                
                txtNoiDung.Text = row.Cells["NoiDung"].Value?.ToString() ?? "";
                cboLoaiChi.SelectedItem = row.Cells["LoaiChi"].Value?.ToString();
                
                if (row.Cells["SoTien"].Value != null)
                    numSoTien.Value = (decimal)row.Cells["SoTien"].Value;
                
                txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString() ?? "";
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Nội dung không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (numSoTien.Value <= 0)
            {
                MessageBox.Show("Số tiền phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dtpNgayChi.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Ngày chi không được lớn hơn ngày hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboLoaiChi.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại chi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                var item = new ChiTieu
                {
                    NgayChi = DateOnly.FromDateTime(dtpNgayChi.Value),
                    NoiDung = txtNoiDung.Text.Trim(),
                    LoaiChi = cboLoaiChi.SelectedItem?.ToString() ?? "Khác",
                    SoTien = numSoTien.Value,
                    GhiChu = txtGhiChu.Text.Trim()
                };

                _context.ChiTieus.Add(item);
                await _context.SaveChangesAsync();

                Log.Information("Đã thêm khoản chi mới: MaCT={MaCT}, NoiDung={NoiDung}", item.MaCt, item.NoiDung);
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                await LoadDataAsync();
                ClearInputs();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi khi thêm mới chi tiêu");
                MessageBox.Show("Lỗi khi lưu dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedMaCt == -1)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput()) return;

            try
            {
                var item = await _context.ChiTieus.FindAsync(_selectedMaCt);
                if (item != null)
                {
                    item.NgayChi = DateOnly.FromDateTime(dtpNgayChi.Value);
                    item.NoiDung = txtNoiDung.Text.Trim();
                    item.LoaiChi = cboLoaiChi.SelectedItem?.ToString() ?? "Khác";
                    item.SoTien = numSoTien.Value;
                    item.GhiChu = txtGhiChu.Text.Trim();

                    await _context.SaveChangesAsync();

                    Log.Information("Đã cập nhật khoản chi: MaCT={MaCT}", item.MaCt);
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    await LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi khi sửa chi tiêu MaCT={MaCT}", _selectedMaCt);
                MessageBox.Show("Lỗi khi cập nhật dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedMaCt == -1)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa khoản chi này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    var item = await _context.ChiTieus.FindAsync(_selectedMaCt);
                    if (item != null)
                    {
                        _context.ChiTieus.Remove(item);
                        await _context.SaveChangesAsync();

                        Log.Information("Đã xóa khoản chi: MaCT={MaCT}", _selectedMaCt);
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        await LoadDataAsync();
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Lỗi khi xóa chi tiêu MaCT={MaCT}", _selectedMaCt);
                    MessageBox.Show("Lỗi khi xóa dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
            txtTimNoiDung.Clear();
            cboLocLoaiChi.SelectedIndex = 0;
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;
            _ = LoadDataAsync();
        }

        private void ClearInputs()
        {
            _selectedMaCt = -1;
            dtpNgayChi.Value = DateTime.Now;
            txtNoiDung.Clear();
            cboLoaiChi.SelectedIndex = -1;
            numSoTien.Value = 0;
            txtGhiChu.Clear();
        }
    }
}
