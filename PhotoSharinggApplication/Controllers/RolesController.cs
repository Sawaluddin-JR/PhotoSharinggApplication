using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoSharinggApplication.Data;

namespace PhotoSharinggApplication.Controllers
{
    public class RolesController : Controller
    {
        private readonly AppDbContext _context;
        public RolesController(
         AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var role = _context.Roles.ToList();
            return View(role);
        }
    }
}
