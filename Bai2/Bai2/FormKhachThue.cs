using System.Data;
using Microsoft.EntityFrameworkCore;
using Bai2.Models;
using Newtonsoft.Json;
using Serilog;

namespace Bai2
{
    public partial class FormKhachThue : Form
    {
        private readonly QlNhaTroContext _db = new QlNhaTroContext();
        private static readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
        private List<Province> _provinces = new List<Province>();
        private bool _isApiError = false;

        public FormKhachThue()
        {
            InitializeComponent();
            LoadData();
            LoadProvincesAsync();
            LoadSearchData();
            SetupEvents();
        }

        private void SetupEvents()
        {
            btnThem.Click += (s, e) => HandleAdd();
            btnSua.Click += (s, e) => HandleEdit();
            btnXoa.Click += (s, e) => HandleDelete();
            btnLamMoi.Click += (s, e) => ResetForm();
            btnSearch.Click += (s, e) => Search();
            btnBoLoc.Click += (s, e) => ResetFilter();
            dgvKhachThue.SelectionChanged += DgvKhachThue_SelectionChanged;
            cboTinh.SelectedIndexChanged += async (s, e) => await LoadWardsAsync();
            btnGayLoi.Click += (s, e) => throw new Exception("Lỗi thử nghiệm hệ thống!");
        }

        private void LoadData()
        {
            var data = _db.KhachThues
                .Include(k => k.MaPhongNavigation)
                .Select(k => new
                {
                    k.MaKhach,
                    k.HoTen,
                    k.SoDienThoai,
                    TenPhong = k.MaPhongNavigation != null ? k.MaPhongNavigation.TenPhong : "",
                    GiaThue = k.MaPhongNavigation != null ? k.MaPhongNavigation.GiaThue : 0,
                    QueQuan = $"{k.TenPhuongXa} - {k.TenTinh}",
                    k.DaDangKyTamTru
                }).ToList();

            dgvKhachThue.DataSource = data;
            dgvKhachThue.Columns["MaKhach"].Visible = false;
            dgvKhachThue.Columns["HoTen"].HeaderText = "Họ tên";
            dgvKhachThue.Columns["SoDienThoai"].HeaderText = "SĐT";
            dgvKhachThue.Columns["TenPhong"].HeaderText = "Phòng";
            dgvKhachThue.Columns["GiaThue"].HeaderText = "Giá thuê";
            dgvKhachThue.Columns["QueQuan"].HeaderText = "Quê quán";
            dgvKhachThue.Columns["DaDangKyTamTru"].HeaderText = "Tạm trú";

            LoadRoomCombos();
        }

        private void LoadRoomCombos()
        {
            // For editing/adding: only empty rooms OR rooms currently having tenants
            // Actually, the requirement says "liệt kê các phòng hợp lệ": 
            // - When adding: Empty rooms.
            // - When editing: Empty rooms + current room of the tenant.
            
            var selectedMaKhach = dgvKhachThue.CurrentRow?.Cells["MaKhach"].Value as int?;
            int? currentMaPhong = null;
            if (selectedMaKhach.HasValue)
            {
                currentMaPhong = _db.KhachThues.Find(selectedMaKhach.Value)?.MaPhong;
            }

            var availableRooms = _db.PhongTros
                .Where(p => p.TinhTrang == "Trống" || p.MaPhong == currentMaPhong)
                .ToList();

            cboPhong.DataSource = availableRooms;
            cboPhong.DisplayMember = "TenPhong";
            cboPhong.ValueMember = "MaPhong";
        }

        private void LoadSearchData()
        {
            var rooms = _db.PhongTros.ToList();
            var searchRooms = new List<PhongTro> { new PhongTro { MaPhong = -1, TenPhong = "Tất cả" } };
            searchRooms.AddRange(rooms);
            cboSearchPhong.DataSource = searchRooms;
            cboSearchPhong.DisplayMember = "TenPhong";
            cboSearchPhong.ValueMember = "MaPhong";

            var searchProvinces = new List<Province> { new Province { code = -1, name = "Tất cả" } };
            // Provinces will be added after API load
        }

        private async void LoadProvincesAsync()
        {
            try
            {
                cboTinh.Enabled = false;
                cboTinh.Text = "Đang tải...";
                
                var response = await _httpClient.GetStringAsync("https://provinces.open-api.vn/api/v2/p/");
                _provinces = JsonConvert.DeserializeObject<List<Province>>(response);
                
                cboTinh.DataSource = new BindingSource(_provinces, null);
                cboTinh.DisplayMember = "name";
                cboTinh.ValueMember = "code";

                var searchProvinces = new List<Province> { new Province { code = -1, name = "Tất cả" } };
                searchProvinces.AddRange(_provinces);
                cboSearchTinh.DataSource = searchProvinces;
                cboSearchTinh.DisplayMember = "name";
                cboSearchTinh.ValueMember = "code";

                _isApiError = false;
                cboPhuongXa.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                _isApiError = true;
                Log.Warning(ex, "Không tải được danh sách tỉnh");
                MessageBox.Show("Không tải được danh sách địa chỉ, vui lòng kiểm tra mạng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTinh.Text = "";
                cboPhuongXa.DropDownStyle = ComboBoxStyle.DropDown; // Allow manual input
            }
            finally
            {
                cboTinh.Enabled = true;
            }
        }

        private async Task LoadWardsAsync()
        {
            if (cboTinh.SelectedValue == null || !(cboTinh.SelectedValue is int provinceCode) || provinceCode == -1) return;

            try
            {
                cboPhuongXa.Enabled = false;
                cboPhuongXa.Text = "Đang tải...";

                var response = await _httpClient.GetStringAsync($"https://provinces.open-api.vn/api/v2/p/{provinceCode}?depth=2");
                var provinceDetail = JsonConvert.DeserializeObject<ProvinceDetail>(response);
                
                var wards = provinceDetail.districts.SelectMany(d => d.wards).ToList();
                
                cboPhuongXa.DataSource = new BindingSource(wards, null);
                cboPhuongXa.DisplayMember = "name";
                cboPhuongXa.ValueMember = "code";
                _isApiError = false;
            }
            catch (Exception ex)
            {
                _isApiError = true;
                Log.Warning(ex, "Không tải được danh sách phường/xã");
                cboPhuongXa.Text = "";
                cboPhuongXa.DataSource = null;
            }
            finally
            {
                cboPhuongXa.Enabled = true;
            }
        }

        private void DgvKhachThue_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKhachThue.CurrentRow == null) return;

            var maKhach = (int)dgvKhachThue.CurrentRow.Cells["MaKhach"].Value;
            var khach = _db.KhachThues.Find(maKhach);
            if (khach == null) return;

            txtHoTen.Text = khach.HoTen;
            rbNam.Checked = khach.GioiTinh == "Nam";
            rbNu.Checked = khach.GioiTinh == "Nữ";
            dtpNgaySinh.Value = khach.NgaySinh.ToDateTime(TimeOnly.MinValue);
            txtSoDienThoai.Text = khach.SoDienThoai;
            chkTamTru.Checked = khach.DaDangKyTamTru ?? false;

            LoadRoomCombos();
            cboPhong.SelectedValue = khach.MaPhong;
            
            if (!_isApiError)
            {
                cboTinh.SelectedValue = khach.MaTinh;
                // Wards will load via event, need to set value after load
                // For simplicity in this basic app, we'll just set the text if it fails
                cboPhuongXa.Text = khach.TenPhuongXa;
            }
            else
            {
                cboTinh.Text = khach.TenTinh;
                cboPhuongXa.Text = khach.TenPhuongXa;
            }
        }

        private void HandleAdd()
        {
            try
            {
                var khach = new KhachThue
                {
                    HoTen = txtHoTen.Text,
                    GioiTinh = rbNam.Checked ? "Nam" : "Nữ",
                    NgaySinh = DateOnly.FromDateTime(dtpNgaySinh.Value),
                    SoDienThoai = txtSoDienThoai.Text,
                    MaPhong = (int?)cboPhong.SelectedValue,
                    DaDangKyTamTru = chkTamTru.Checked,
                    MaTinh = _isApiError ? null : (int?)cboTinh.SelectedValue,
                    TenTinh = cboTinh.Text,
                    MaPhuongXa = _isApiError ? null : (int?)cboPhuongXa.SelectedValue,
                    TenPhuongXa = cboPhuongXa.Text
                };

                _db.KhachThues.Add(khach);
                
                // Update room status
                if (khach.MaPhong.HasValue)
                {
                    var phong = _db.PhongTros.Find(khach.MaPhong.Value);
                    if (phong != null) phong.TinhTrang = "Đang thuê";
                }

                _db.SaveChanges();
                LoadData();
                ResetForm();
                MessageBox.Show("Thêm khách thuê thành công!");
            }
            catch (Exception ex)
            {
                throw ex; // Let global handler catch it
            }
        }

        private void HandleEdit()
        {
            if (dgvKhachThue.CurrentRow == null) return;
            try
            {
                var maKhach = (int)dgvKhachThue.CurrentRow.Cells["MaKhach"].Value;
                var khach = _db.KhachThues.Find(maKhach);
                if (khach == null) return;

                int? oldMaPhong = khach.MaPhong;
                int? newMaPhong = (int?)cboPhong.SelectedValue;

                khach.HoTen = txtHoTen.Text;
                khach.GioiTinh = rbNam.Checked ? "Nam" : "Nữ";
                khach.NgaySinh = DateOnly.FromDateTime(dtpNgaySinh.Value);
                khach.SoDienThoai = txtSoDienThoai.Text;
                khach.MaPhong = newMaPhong;
                khach.DaDangKyTamTru = chkTamTru.Checked;
                khach.MaTinh = _isApiError ? null : (int?)cboTinh.SelectedValue;
                khach.TenTinh = cboTinh.Text;
                khach.MaPhuongXa = _isApiError ? null : (int?)cboPhuongXa.SelectedValue;
                khach.TenPhuongXa = cboPhuongXa.Text;

                // Update old room status if changed
                if (oldMaPhong != newMaPhong)
                {
                    if (oldMaPhong.HasValue) UpdateRoomStatus(oldMaPhong.Value);
                    if (newMaPhong.HasValue)
                    {
                        var p = _db.PhongTros.Find(newMaPhong.Value);
                        if (p != null) p.TinhTrang = "Đang thuê";
                    }
                }

                _db.SaveChanges();
                LoadData();
                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void HandleDelete()
        {
            if (dgvKhachThue.CurrentRow == null) return;
            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                var maKhach = (int)dgvKhachThue.CurrentRow.Cells["MaKhach"].Value;
                var khach = _db.KhachThues.Find(maKhach);
                if (khach == null) return;

                int? maPhong = khach.MaPhong;
                _db.KhachThues.Remove(khach);
                _db.SaveChanges();

                if (maPhong.HasValue) UpdateRoomStatus(maPhong.Value);
                _db.SaveChanges();

                LoadData();
                ResetForm();
                MessageBox.Show("Xóa thành công!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateRoomStatus(int maPhong)
        {
            var room = _db.PhongTros.Find(maPhong);
            if (room == null) return;
            
            bool hasTenants = _db.KhachThues.Any(k => k.MaPhong == maPhong);
            room.TinhTrang = hasTenants ? "Đang thuê" : "Trống";
        }

        private void ResetForm()
        {
            txtHoTen.Clear();
            rbNam.Checked = true;
            dtpNgaySinh.Value = DateTime.Now;
            txtSoDienThoai.Clear();
            chkTamTru.Checked = false;
            if (cboPhong.Items.Count > 0) cboPhong.SelectedIndex = 0;
            if (cboTinh.Items.Count > 0) cboTinh.SelectedIndex = 0;
            LoadRoomCombos();
        }

        private void Search()
        {
            string keyword = txtSearch.Text.ToLower();
            int? maPhong = (int?)cboSearchPhong.SelectedValue;
            int? maTinh = (int?)cboSearchTinh.SelectedValue;

            var query = _db.KhachThues.Include(k => k.MaPhongNavigation).AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(k => k.HoTen.ToLower().Contains(keyword) || k.SoDienThoai.Contains(keyword));
            }

            if (maPhong.HasValue && maPhong != -1)
            {
                query = query.Where(k => k.MaPhong == maPhong);
            }

            if (maTinh.HasValue && maTinh != -1)
            {
                query = query.Where(k => k.MaTinh == maTinh);
            }

            dgvKhachThue.DataSource = query.Select(k => new
            {
                k.MaKhach,
                k.HoTen,
                k.SoDienThoai,
                TenPhong = k.MaPhongNavigation != null ? k.MaPhongNavigation.TenPhong : "",
                GiaThue = k.MaPhongNavigation != null ? k.MaPhongNavigation.GiaThue : 0,
                QueQuan = $"{k.TenPhuongXa} - {k.TenTinh}",
                k.DaDangKyTamTru
            }).ToList();
        }

        private void ResetFilter()
        {
            txtSearch.Clear();
            cboSearchPhong.SelectedIndex = 0;
            cboSearchTinh.SelectedIndex = 0;
            LoadData();
        }
    }

    // API Models
    public class Province
    {
        public int code { get; set; }
        public string name { get; set; }
    }

    public class ProvinceDetail
    {
        public int code { get; set; }
        public string name { get; set; }
        public List<District> districts { get; set; }
    }

    public class District
    {
        public int code { get; set; }
        public string name { get; set; }
        public List<Ward> wards { get; set; }
    }

    public class Ward
    {
        public int code { get; set; }
        public string name { get; set; }
    }
}
