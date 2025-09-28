using System;
using System.Collections.Generic;

namespace GK_Web.Models.Entities;

public partial class ChiTietDatSach
{
    public int Id { get; set; }

    public int? IdSach { get; set; }

    public int? IdDatSach { get; set; }

    public virtual DatSach? IdDatSachNavigation { get; set; }

    public virtual Sach? IdSachNavigation { get; set; }
}
