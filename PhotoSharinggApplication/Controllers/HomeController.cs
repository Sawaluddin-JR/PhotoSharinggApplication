using PhotoSharinggApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using PhotoSharinggApplication.Services;
using PhotoSharinggApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace PhotoSharinggApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var photos = _context.SharePhoto.Include(x => x.Status).Where(x => x.Status.Name == "Published").ToList();

            if (photos == null)
            {
                return View(new List<SharePhoto>());
            }
            return View(photos);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
