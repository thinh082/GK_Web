using GK_Web.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GK_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SachController : ControllerBase
    {
        private readonly GkContext _context;
        public SachController(GkContext context)
        {
            _context = context;
        }
        [HttpGet("LayDanhSachSach")]
        public IActionResult LayDanhSachSach()
        {
            var saches = _context.Saches.ToList();
            return Ok(saches);
        }
    }
}
