using PhotoSharinggApplication.Models;
using PhotoSharinggApplication.Services.AkunService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoSharinggApplication.Data;

namespace PhotoSharinggApplication.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IAkunService _akunServ;
        private readonly AppDbContext _context;

        public AdminController(IAkunService akunServ)
        {
            _akunServ = akunServ;
        }

        public IActionResult Index()
        {
            var all = new penggunaDashboard();

            all.user = _akunServ.AmbilSemuaUser();
            all.roles = _akunServ.AmbilSemuaRoles();

            return View(all);
        }
        public IActionResult Delete(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Name == id);
            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Admin/Index");
        }
    }
}
