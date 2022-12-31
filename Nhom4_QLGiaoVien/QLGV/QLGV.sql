CREATE DATABASE QLGV
GO
USE [QLGV]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanSu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](50) NULL,
	[SoCCCD_CMT] [nvarchar](50) NULL,
	[GioiTinh] [nchar](10) NULL,
	[NgaySinh] [date] NULL,
	[Email] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[Phone] [nvarchar](20) NULL,
	[PhongBan] [nvarchar] (50) NULL,
	[ChucVu] [nvarchar] (50) NULL,
 CONSTRAINT [PK_NhanSu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[TaiKhoan](
	[tendangnhap] [nvarchar](50) NOT NULL,
	[TenGV] [nvarchar](50) NULL,
	[matkhau] [nvarchar](50) NULL,
	[IDPer] [int] NOT NULL,
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[tendangnhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[KyLuat](
	[MaKL] [int] NOT NULL,
	[TenGV] [nvarchar](50) NOT NULL,
	[LiDo] [nvarchar](50) NULL,
	[NgayKyLuat] [date] NULL,
	[Phat] [nvarchar](50) NULL,
 CONSTRAINT [PK_KyLuat] PRIMARY KEY CLUSTERED 
(
	[MaKL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Thuong](
	[MaThuong] [int] NOT NULL,
	[TenGV] [nvarchar](50) NULL,
	[LyDo] [nvarchar](50) NULL,
	[NgayKhenThuong] [date] NULL,
	[Thuong] [nvarchar](50) NULL,
 CONSTRAINT [PK_Thuong] PRIMARY KEY CLUSTERED 
(
	[MaThuong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[LopHoc](
	[TenLop] [nvarchar](10) NOT NULL,
	[QSo] [int] NOT NULL,
 CONSTRAINT [PK_LopHoc] PRIMARY KEY CLUSTERED 
(
	[TenLop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MonHoc](
	[TenMon] [nvarchar](20) NOT NULL,
	[SoHocPhan] [int] NOT NULL,
	[ThoiGian] [date] NULL
 CONSTRAINT [PK_MonHoc] PRIMARY KEY CLUSTERED 
(
	[TenMon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[TKB](
	[MaTKB] [int] NOT NULL,
	[TenGV] [nvarchar](50) NOT NULL,
	[TenLop] [nvarchar](10) NOT NULL,
	[MonHoc] [nvarchar](20) NOT NULL,
	[Ngay] [date] NOT NULL,
	[SoTiet] [int] NOT NULL,
	[TietBatDau] [int] NOT NULL,
	[Phong] [nvarchar](20) NOT NULL
 CONSTRAINT [PK_TKB] PRIMARY KEY CLUSTERED 
(
	[MaTKB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Luong](
	[MaL] [int] NOT NULL,
	[TenGV] [nvarchar](50) NOT NULL,
	[LuongCoSo] [int] not NULL,
	[HeSoLuong] [float] NULL,
	[Phat] [int] NULL,
	[Thuong] [int] null,
	[PhuCap] [int] null,
	[Luong] [int] null
 CONSTRAINT [PK_Luong] PRIMARY KEY CLUSTERED 
(
	[MaL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO









--dang nhap
create proc [dbo].[Login2]
@Username nvarchar(20),
@Password nvarchar(20)
as
begin
    if exists (select * from TaiKhoan where tendangnhap = @Username and matkhau = @Password and IDPer = 1)
        select 1 as code
	else if exists (select * from TaiKhoan where tendangnhap = @Username and matkhau = @Password and IDPer = 0)
        select 0 as code
    else if exists(select * from TaiKhoan where tendangnhap = @Username and matkhau != @Password and IDPer = 0) 
        select 2 as code
    else select 3 as code
end


--Xoa GV
create proc [dbo].[XoaGV]
@ID int
as
begin
	delete NhanSu where ID = @ID
end
go
--Xóa KL
create proc [dbo].[XoaKL1]
@MaKL int
as
begin
	delete KyLuat where MaKL = @MaKL
end
go
--Xóa thưởng
create proc [dbo].[XoaThuong1]
@MaThuong int
as
begin
	delete Thuong where MaThuong = @MaThuong
end
go
--Xóa tkb
create proc [dbo].[XoaTKB]
@MaTKB int
as
begin
	delete TKB where MaTKB = @MaTKB
end
go
--Xóa tài khoản
create proc [dbo].[XoaTK]
@tendangnhap nvarchar(50)
as
begin
	delete TaiKhoan where tendangnhap = @tendangnhap
end
go
--Xóa lương
create proc [dbo].[XoaL]
@MaL nvarchar(50)
as
begin
	delete Luong where MaL = @MaL
end
go



--Them 
create proc [dbo].[ThemNV]
@HoTen nvarchar(50),
@SoCCCD_CMT nvarchar(50),
@GioiTinh nchar(10),
@NgaySinh date,
@Email nvarchar(50),
@DiaChi nvarchar(50),
@Phone nvarchar(20),
@PhongBan nvarchar(50),
@ChucVu nvarchar(50)
as
begin
	insert into NhanSu values (@HoTen,@SoCCCD_CMT, @GioiTinh, @NgaySinh, @Email, @DiaChi, @Phone,
	@PhongBan,@ChucVu)
end
go
--Them KL
create proc [dbo].[ThemKL1]
@MaKL int,
@TenGV nvarchar(50),
@Lido nvarchar(50),
@NgayKyLuat date,
@Phat nvarchar(50)
as
begin
	insert into KyLuat values (@MaKL,@TenGV,@Lido,@NgayKyLuat,@Phat)
end
go
--Them Thuong
create proc [dbo].[ThemThuong]
@MaThuong int,
@TenGV nvarchar(50),
@Lydo nvarchar(50),
@NgayKhenThuong date,
@Thuong nvarchar(50)
as
begin
	insert into Thuong values (@MaThuong,@TenGV,@Lydo,@NgayKhenThuong,@Thuong)
end
go
--THÊM TKB
create proc [dbo].[ThemTKB]
@MaTKB int,
@TenGV nvarchar(50),
@TenLop nvarchar(50),
@MonHoc nvarchar(50),
@Ngay date,
@SoTiet int,
@TietBatDau int,
@Phong nvarchar(50)
as
begin
	insert into TKB values (@MaTKB,@TenGV,@TenLop,@MonHoc,@Ngay,@SoTiet,@TietBatDau,@Phong)
end
go
--thêm tài khoản
create proc [dbo].[ThemTK]
@tendangnhap nvarchar(50),
@TenGV nvarchar(50),
@matkhau nvarchar(50),
@IDPer int
as
begin
	insert into TaiKhoan values (@tendangnhap,@TenGV,@matkhau,@IDPer)
end
go
--Thêm Lương
create proc [dbo].[ThemL]
@MaL int,
@TenGV nvarchar(50),
@LuongCoSo int,
@HeSoLuong float,
@Phat int,
@Thuong int,
@PhuCap int,
@Luong int
as
begin
	insert into Luong values (@MaL,@TenGV,@LuongCoSo,@HeSoLuong,@Phat,@Thuong,@PhuCap,@Luong)
end
go



--Sua 
create proc [dbo].[SuaGV]
@ID int,
@HoTen nvarchar(50),
@SoCCCD_CMT nvarchar(50),
@GioiTinh nchar(10),
@NgaySinh date,
@Email nvarchar(50),
@DiaChi nvarchar(50),
@Phone nvarchar(20),
@PhongBan nvarchar(50),
@ChucVu nvarchar(50)
as
begin
	update NhanSu set
	HoTen = @HoTen,
	SoCCCD_CMT = @SoCCCD_CMT,
	GioiTinh = @GioiTinh,
	NgaySinh = @NgaySinh,
	Email = @Email,
	DiaChi = @DiaChi,
	Phone = @Phone,
	PhongBan = @PhongBan,
	ChucVu = @ChucVu
	where ID = @ID
end
go
--Sua KL
create proc [dbo].[SuaKL]
@MaKL int,
@TenGV nvarchar(50),
@LiDo nvarchar(50),
@NgayKyLuat date,
@Phat nvarchar(50)
as
begin
	update KyLuat set
	MaKL = @MaKL,
	TenGV = @TenGV,
	LiDo =@LiDo,
	NgayKyLuat = @NgayKyLuat,
	Phat = @Phat
	where MaKL = @MaKL
end
go
--Sửa thưởng
create proc [dbo].[SuaThuong1]
@MaThuong int,
@TenGV nvarchar(50),
@LyDo nvarchar(50),
@NgayKhenThuong date,
@Thuong nvarchar(50)
as
begin
	update Thuong set
	MaThuong = @MaThuong,
	TenGV = @TenGV,
	LyDo =@LyDo,
	NgayKhenThuong = @NgayKhenThuong,
	Thuong = @Thuong		
	where MaThuong = @MaThuong
end
go
--Sửa tkb
create proc [dbo].[SuaTKB]
@MaTKB int,
@TenGV nvarchar(50),
@TenLop nvarchar(50),
@MonHoc nvarchar(50),
@Ngay date,
@SoTiet int,
@TietBatDau int,
@Phong nvarchar(50)
as
begin
	update TKB set
	MaTKB = @MaTKB,
	TenGV = @TenGV,
	TenLop =@TenLop,
	MonHoc = @MonHoc,
	Ngay = @Ngay,
	SoTiet = @SoTiet,
	TietBatDau = @TietBatDau,
	Phong = @Phong
	where MaTKB = @MaTKB
end
go
--sửa tài khoản
create proc [dbo].[SuaTK]
@tendangnhap nvarchar(50),
@TenGV nvarchar(50),
@matkhau nvarchar(50),
@IDPer int
as
begin
	update TaiKhoan set
	tendangnhap = @tendangnhap,
	TenGV = @TenGV,
	matkhau =@matkhau,
	IDPer = @IDPer		
	where tendangnhap = @tendangnhap
end
go
--Sửa Lương
create proc [dbo].[SuaL]
@MaL int,
@TenGV nvarchar(50),
@LuongCoSo int,
@HeSoLuong float,
@Phat int,
@Thuong int,
@PhuCap int,
@Luong int
as
begin
	update Luong set
	MaL = @MaL,
	TenGV = @TenGV,
	LuongCoSo =@LuongCoSo,
	HeSoLuong = @HeSoLuong,
	Phat = @Phat,
	Thuong = @Thuong,
	PhuCap = @PhuCap,
	Luong = @Luong
	where MaL = @MaL
end
go

--Lay DS 
create proc [dbo].[LayDSNS]
as
begin
	select * from NhanSu
end
go

create proc [dbo].[LayDSKL]
as
begin
	select * from KyLuat
end
go

create proc [dbo].[LayDST]
as
begin
	select * from Thuong
end
go

create proc [dbo].[LayDSTKB]
as
begin
	select * from TKB
end
go

create proc [dbo].[LayDSTK]
as
begin
	select * from TaiKhoan
end
go

create proc [dbo].[LayDSL]
as
begin
	select * from Luong
end
go


