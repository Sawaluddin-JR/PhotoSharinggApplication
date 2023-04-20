using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhotoSharinggApplication.Areas.Admin.Controllers
{
    [Authorize (Roles = "Admin")]
    [Area ("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FetchAPI() => View();
    }
}
