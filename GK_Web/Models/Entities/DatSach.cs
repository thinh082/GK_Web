using System;
using System.Collections.Generic;

namespace GK_Web.Models.Entities;

public partial class DatSach
{
    public int Id { get; set; }

    public int? IdTaiKhoan { get; set; }

    public string? DiaChi { get; set; }

    public decimal? TongTien { get; set; }

    public DateTime? NgayDat { get; set; }

    public string? SoDienThoai { get; set; }

    public virtual ICollection<ChiTietDatSach> ChiTietDatSaches { get; set; } = new List<ChiTietDatSach>();

    public virtual TaiKhoan? IdTaiKhoanNavigation { get; set; }
}
