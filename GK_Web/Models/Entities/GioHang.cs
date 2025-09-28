using System;
using System.Collections.Generic;

namespace GK_Web.Models.Entities;

public partial class GioHang
{
    public int Id { get; set; }

    public int? IdTaiKhoan { get; set; }

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual TaiKhoan? IdTaiKhoanNavigation { get; set; }
}
