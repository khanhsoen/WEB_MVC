CREATE DATABASE KT0720_MVC_61133801
GO
USE KT0720_MVC_61133801

GO
CREATE TABLE LOP
(
	MaLop NVARCHAR(10) PRIMARY KEY,
	TenLop NVARCHAR(20) NOT NULL
)

CREATE TABLE SINHVIEN
(
	MaSV NVARCHAR(10) PRIMARY KEY,
	HoSV NVARCHAR(50) NOT NULL,
	TenSV NVARCHAR(10) NOT NULL,
	NgaySinh DATE NOT NULL,
	GioiTinh BIT DEFAULT(1) NOT NULL,
	AnhSV NVARCHAR(50) ,
	DiaChi NVARCHAR(100) NOT NULL,
	MaLop NVARCHAR(10) NOT NULL FOREIGN KEY REFERENCES LOP(MaLop)
)

GO
INSERT INTO LOP VALUES (N'TA', N'Tiếng Anh'),(N'VL', N'Vật Lý'), (N'LT', N'Lập Trình')

GO
INSERT INTO SINHVIEN VALUES (N'SV01', N'Trần Văn', N'Khánh', '2001-10-23', 1, N'employee.png', N'Nha Trang, Khánh Hoà', N'TA')
INSERT INTO SINHVIEN VALUES (N'SV02', N'Nguyễn', N'An', '2001-11-01', 0, N'employee.png', N'Nha Trang, Khánh Hoà', N'VL')
INSERT INTO SINHVIEN VALUES (N'SV03', N'Trần Đại', N'Thanh', '2001-02-23', 1, N'employee.png', N'Nha Trang, Khánh Hoà', N'LT')
INSERT INTO SINHVIEN VALUES (N'SV04', N'Nguyễn Trung', N'Khánh', '2001-02-10', 1, N'employee.png', N'Nha Trang, Khánh Hoà', N'TA')
INSERT INTO SINHVIEN VALUES (N'SV05', N'Hồ Văn', N'Kiên', '2001-12-23', 1, N'employee.png', N'Nha Trang, Khánh Hoà', N'LT')
INSERT INTO SINHVIEN VALUES (N'SV06', N'Trần Nguyễn Khánh', N'An', '2001-08-23', 0, N'employee.png', N'Nha Trang, Khánh Hoà', N'TA')
INSERT INTO SINHVIEN VALUES (N'SV07', N'Trần Nguyễn ', N'An', CAST(N'1995-11-23' AS Date), 0, N'employee.png', N'Nha Trang, Khánh Hoà', N'TA')