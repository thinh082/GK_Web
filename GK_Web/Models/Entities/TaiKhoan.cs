using System;
using System.Collections.Generic;

namespace GK_Web.Models.Entities;

public partial class TaiKhoan
{
    public int Id { get; set; }

    public string? TenTaiKhoan { get; set; }

    public string? MatKhau { get; set; }

    public virtual ICollection<DatSach> DatSaches { get; set; } = new List<DatSach>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();
}
