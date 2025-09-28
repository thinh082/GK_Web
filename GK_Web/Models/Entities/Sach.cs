using System;
using System.Collections.Generic;

namespace GK_Web.Models.Entities;

public partial class Sach
{
    public int Id { get; set; }

    public string? TenSach { get; set; }

    public string? HinhAnh { get; set; }

    public string? MoTa { get; set; }

    public decimal? Gia { get; set; }

    public virtual ICollection<ChiTietDatSach> ChiTietDatSaches { get; set; } = new List<ChiTietDatSach>();

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();
}
