using GK_Web.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GK_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatSachController : ControllerBase
    {
        private readonly GkContext _context;
        public DatSachController(GkContext context)
        {
            _context = context;
        }
        [HttpPost("DatSach")]
        public IActionResult DatSach([FromBody] DatSachModel model)
        {
            var taiKhoan = _context.TaiKhoans.FirstOrDefault(tk => tk.Id == model.IdTaiKhoan);
            if (taiKhoan == null)
            {
                return BadRequest(new { Success = false, Message = "Tài khoản không tồn tại" });
            }

            // 👉 Tạo đơn đặt sách mới
            var newDatSach = new DatSach
            {
                IdTaiKhoan = model.IdTaiKhoan,
                DiaChi = model.DiaChi,
                TongTien = model.TongTien,
                NgayDat = DateTime.Now,
                SoDienThoai = model.SoDienThoai
            };

            _context.DatSaches.Add(newDatSach);

            // 👉 Lấy tất cả chi tiết giỏ hàng theo tài khoản
            var gioHang = _context.GioHangs
                .Where(gh => gh.IdTaiKhoan == model.IdTaiKhoan)
                .SelectMany(gh => gh.ChiTietGioHangs) // lấy thẳng entity
                .ToList();

            if (gioHang.Any())
            {
                _context.ChiTietGioHangs.RemoveRange(gioHang);
            }

            _context.SaveChanges();

            return Ok(new { Success = true, Message = "Đặt sách thành công, giỏ hàng đã được xóa" });
        }


    }
    public class DatSachModel
    {
        public int IdTaiKhoan { get; set; }
        public string DiaChi { get; set; }
        public decimal? TongTien { get; set; }

        public string? SoDienThoai { get; set; }
    }
}
