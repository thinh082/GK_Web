using GK_Web.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GK_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangController : ControllerBase
    {
        private readonly GkContext _context;
        public GioHangController(GkContext gkContext)
        {
            _context = gkContext;
        }
        [HttpGet("LayGioHang/{idTaiKhoan}")]
        public IActionResult LayGioHang(int idTaiKhoan)
        {
            var gioHang = _context.GioHangs
                .Where(r => r.IdTaiKhoan == idTaiKhoan)
                .SelectMany(r => r.ChiTietGioHangs
                    .Select(c => new
                    {
                        IdSach = c.IdSach,
                        TenSach = c.IdSachNavigation.TenSach,
                        Gia = c.IdSachNavigation.Gia,
                        SoLuong = c.SoLuong
                    }))
                .ToList();

            if (gioHang == null || !gioHang.Any())
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Không tìm thấy giỏ hàng"
                });
            }

            return Ok(gioHang);
        }


        [HttpPost("ThemGioHang")]
            public IActionResult ThemGioHang([FromBody] ChiTietGioHangModel model, int idTaiKhoan)
            {
                var gioHang = _context.GioHangs.FirstOrDefault(gh => gh.IdTaiKhoan == idTaiKhoan);
                if (gioHang == null)
                {
                    return BadRequest(new { Success = false, Message = "Không tìm thấy giỏ hàng" });
                }
                var existingChiTiet = _context.ChiTietGioHangs.FirstOrDefault(ct => ct.IdGioHang == gioHang.Id && ct.IdSach == model.IdSach);
                if (existingChiTiet != null)
                {
                    existingChiTiet.SoLuong += model.SoLuong ?? 1;
                }
                else
                {
                    var newChiTiet = new ChiTietGioHang
                    {
                        IdGioHang = gioHang.Id,
                        IdSach = model.IdSach,
                        SoLuong = model.SoLuong ?? 1
                    };
                    _context.ChiTietGioHangs.Add(newChiTiet);
                }
                _context.SaveChanges();
                return Ok(new { Message = "Thêm vào giỏ hàng thành công" });
            }
            [HttpPost("CapNhatGioHang")]
            public IActionResult CapNhatGioHang([FromBody] ChiTietGioHangModel model, int idTaiKhoan)
            {
                var gioHang = _context.GioHangs.FirstOrDefault(gh => gh.IdTaiKhoan == idTaiKhoan);
                if (gioHang == null)
                {
                    return BadRequest(new { Success = false, Message = "Không tìm thấy giỏ hàng" });
                }
                var chiTiet = _context.ChiTietGioHangs.FirstOrDefault(ct => ct.IdGioHang == gioHang.Id && ct.IdSach == model.IdSach);
                if (chiTiet == null)
                {
                    return BadRequest(new { Success = false, Message = "Không tìm thấy chi tiết giỏ hàng" });
                }
                chiTiet.SoLuong = model.SoLuong ?? chiTiet.SoLuong;
                _context.SaveChanges();
                return Ok(new { Message = "Cập nhật giỏ hàng thành công" });
            }
            [HttpPost("XoaGioHang")]
            public IActionResult XoaGioHang(int idTaiKhoan, int idSach)
            {
                var gioHang = _context.GioHangs.FirstOrDefault(gh => gh.IdTaiKhoan == idTaiKhoan);
                if (gioHang == null)
                {
                    return BadRequest(new { Success = false, Message = "Không tìm thấy giỏ hàng" });
                }
                var chiTiet = _context.ChiTietGioHangs.FirstOrDefault(ct => ct.IdGioHang == gioHang.Id && ct.IdSach == idSach);
                if (chiTiet == null)
                {
                    return BadRequest(new { Success = false, Message = "Không tìm thấy chi tiết giỏ hàng" });
                }
                _context.ChiTietGioHangs.Remove(chiTiet);
                _context.SaveChanges();
                return Ok(new { Message = "Xóa khỏi giỏ hàng thành công" });
            }

    }
    public class ChiTietGioHangModel
    {
        public int? IdSach { get; set; }

        public int? SoLuong { get; set; }
    }
}
