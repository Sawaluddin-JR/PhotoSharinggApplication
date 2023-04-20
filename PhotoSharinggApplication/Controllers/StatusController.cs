using Microsoft.AspNetCore.Mvc;
using PhotoSharinggApplication.Data;

namespace PhotoSharinggApplication.Controllers
{
    public class StatusController : Controller
    {
        private readonly AppDbContext _context;
        public StatusController(
            AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var status = _context.Statuses.ToList();
            return View(status);
        }
    }
}
