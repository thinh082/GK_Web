using System;
using System.Collections.Generic;

namespace GK_Web.Models.Entities;

public partial class ChiTietGioHang
{
    public int Id { get; set; }

    public int? IdSach { get; set; }

    public int? SoLuong { get; set; }

    public int? IdGioHang { get; set; }

    public virtual GioHang? IdGioHangNavigation { get; set; }

    public virtual Sach? IdSachNavigation { get; set; }
}
