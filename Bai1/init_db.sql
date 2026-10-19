USE master;
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'QL_ChiTieu')
BEGIN
    ALTER DATABASE QL_ChiTieu SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QL_ChiTieu;
END
GO

CREATE DATABASE QL_ChiTieu;
GO

USE QL_ChiTieu;
GO

CREATE TABLE ChiTieu (
    MaCT INT IDENTITY(1,1) PRIMARY KEY,
    NgayChi DATE NOT NULL,
    NoiDung NVARCHAR(100) NOT NULL,
    LoaiChi NVARCHAR(30) NOT NULL,
    SoTien DECIMAL(18, 0) NOT NULL,
    GhiChu NVARCHAR(200) NULL
);
GO

-- Chèn 10 dòng dữ liệu mẫu
INSERT INTO ChiTieu (NgayChi, NoiDung, LoaiChi, SoTien, GhiChu) VALUES
('2026-10-01', N'Cơm trưa', N'Ăn uống', 35000, N'Ăn tiệm'),
('2026-10-02', N'Xăng xe', N'Đi lại', 60000, N'Đầy bình'),
('2026-10-03', N'Photo giáo trình', N'Học tập', 25000, N'Môn LTTQ'),
('2026-10-04', N'Xem phim', N'Giải trí', 90000, N'CGV'),
('2026-10-05', N'Trà sữa', N'Ăn uống', 40000, N'Gong Cha'),
('2026-10-06', N'Mua sắm', N'Cá nhân', 150000, N'Áo thun'),
('2026-10-07', N'Tiền điện', N'Hóa đơn', 500000, N'Tháng 9'),
('2026-10-08', N'Ăn sáng', N'Ăn uống', 25000, N'Bánh mì'),
('2026-10-09', N'Vé xe buýt', N'Đi lại', 7000, N'Tháng'),
('2026-10-10', N'Bút bi', N'Học tập', 15000, N'Thi HK1');
GO
