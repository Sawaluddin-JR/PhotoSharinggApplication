using PhotoSharinggApplication.Data;
using PhotoSharinggApplication.Helper;
using PhotoSharinggApplication.Models;
using PhotoSharinggApplication.Services;
using PhotoSharinggApplication.Services.AkunService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PhotoSharinggApplication.Controllers
{
    public class AkunController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAkunService _akSer;
        private readonly EmailService _email;

        private static int KodeOTP;

        public AkunController(IAkunService akSer, AppDbContext context, EmailService e)
        {
            _akSer = akSer;
            _context = context;
            _email = e;
        }


        public IActionResult Index()
        {
            var users = _context.Users
                .Include(x => x.Roles)
                .ToList();

            return View(users);
        }

        public IActionResult DaftarAdmin()
        {
            ViewBag.Roles = _context.Roles.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View();
        }

        [HttpPost]
        public IActionResult DaftarAdmin(User data)
        {
                _akSer.DaftarAdmin(data);

                return Redirect("/Admin/Admin/Index");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User data)
        {
            _akSer.DaftarUser(data);

            return RedirectToAction("SignIn");
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(User parameter)
        {
            var cariUser = _context.Users
                .Where(x => x.Username == parameter.Username
                    && x.Password == parameter.Password)
                .Include(x => x.Roles)
                .FirstOrDefault();

            if (cariUser != null)
            {
                var claims = new List<Claim> {
                    new Claim("Username", cariUser.Username),
                    new Claim("Role", cariUser.Roles.Name)
                };

                await HttpContext.SignInAsync(new ClaimsPrincipal(
                    new ClaimsIdentity(claims, "Cookies", "Username", "Role")
                    ));

                if (cariUser.Roles.Id == "1")
                {
                    return Redirect("/Admin/Home");
                }
                else if (cariUser.Roles.Id == "2")
                {
                    return Redirect("/SharePhoto/Index");
                }

                return Redirect("/SignIn");
            }

            if (!string.IsNullOrEmpty(parameter.Username) && !string.IsNullOrEmpty(parameter.Password))
            {
                ViewBag.Pesan = "Pengguna tidak ditemukan";
            }

            return View(parameter);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [HttpPost]
        public string KirimEmailOTP(string EmailTujuan)
        {
            var searchEmail = _context.Users.FirstOrDefault(x => x.Email == EmailTujuan);

            if (searchEmail != null) return "Email tersebut sudah di gunakan";

            //BanyakBantuan _bantu = new();
            //KodeOTP = _bantu.BuatOtp();

            string subjeknya = "Konfirmasi Email Untuk Registrasi Akun";

            string isiEmail =
                "<h1>Kode OTP Registrasi Akun Anda <i style='color : red;'>"
                + KodeOTP.ToString()
                + "</i></h1>"
                + "<a href='mailto:dotnetlanjutan@gmail.com?subject=Bantuan&body=Halo'>Bantuan</a>";

            bool cek = _email.KirimEmail(EmailTujuan, subjeknya, isiEmail);

            if (cek) return "Kode OTP Telah Terkirim Ke " + EmailTujuan;

            return "Email " + EmailTujuan + "Tidak Ditemukan";
        }
    }
}
