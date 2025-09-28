using GK_Web.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GK_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly GkContext _context;
        public TaiKhoanController(GkContext gkContext)
        {
            _context = gkContext;
        }
        [HttpPost("DangNhap")]
        public IActionResult DangNhap([FromBody] Request request)
        {
            if (request.TaiKhoan == null || request.MatKhau == null)
            {
                return BadRequest(new { Success = false, Message = "Tài khoản và mật khẩu không được để trống" });
            }

            var check = _context.TaiKhoans
                .FirstOrDefault(tk => tk.TenTaiKhoan == request.TaiKhoan && tk.MatKhau == request.MatKhau);

            if (check == null)
            {
                return BadRequest(new { Success = false, Message = "Tài khoản hoặc mật khẩu không đúng" });
            }

            return Ok(new
            {
                Success = true,
                Message = "Đăng nhập thành công",
                TaiKhoanId = check.Id
            });
        }

        [HttpPost("DangKy")]
        public IActionResult DangKy([FromBody] Request request)
        {
            if (string.IsNullOrEmpty(request.TaiKhoan) || string.IsNullOrEmpty(request.MatKhau))
            {
                return BadRequest(new { Success = false, Message = "Tài khoản và mật khẩu không được để trống" });
            }

            var check = _context.TaiKhoans.FirstOrDefault(tk => tk.TenTaiKhoan == request.TaiKhoan);
            if (check != null)
            {
                return BadRequest(new { Success = false, Message = "Tài khoản đã tồn tại" });
            }

            // 👉 Tạo tài khoản
            var newTaiKhoan = new TaiKhoan
            {
                TenTaiKhoan = request.TaiKhoan,
                MatKhau = request.MatKhau
            };

            _context.TaiKhoans.Add(newTaiKhoan);
            _context.SaveChanges(); // Lưu để DB sinh Id cho tài khoản

            // 👉 Tạo giỏ hàng gắn với tài khoản vừa tạo
            var gioHang = new GioHang
            {
                IdTaiKhoan = newTaiKhoan.Id
            };

            _context.GioHangs.Add(gioHang);
            _context.SaveChanges();

            return Ok(new { Success = true, Message = "Đăng ký thành công" });
        }

    }
    public class Request
    {
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
    }
}
