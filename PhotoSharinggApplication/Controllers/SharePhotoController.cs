using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Notice;
using Newtonsoft.Json;
using PhotoSharinggApplication.Data;
using PhotoSharinggApplication.Helper;
using PhotoSharinggApplication.Models;
using PhotoSharinggApplication.Models.ViewModel;
using PhotoSharinggApplication.Services;

namespace PhotoSharinggApplication.Controllers
{
    [Authorize]
    public class SharePhotoController : Controller
    {
        ISharePhotoService _sharePhotoService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SharePhotoController(
            AppDbContext context,
            IWebHostEnvironment env)
        {
           // _sharePhotoService = sharePhotoService;
            _context = context;
            _env = env;
        }

        public IActionResult Index(string search)
        {
            var share = _context.SharePhoto
                .Include(x => x.Status)
                .ToList();

            if (!String.IsNullOrEmpty(search))
            {
                share = _context.SharePhoto.Where(x => x.JudulPhoto.ToLower().Contains(search)).ToList();
            }
            return View(share);
        }
        public IActionResult Detail(int id)
        {

            var share = _context.SharePhoto.Include(x => x.Status).ToList().FirstOrDefault(x => x.Id == id);
            return View(share);
        }
        public IActionResult Download(int id)
        {
            var share = _context.SharePhoto.FirstOrDefault(x => x.Id == id);
            var filepath = Path.Combine(_env.WebRootPath, "upload", share.Photo);
            return File(
                System.IO.File.ReadAllBytes(filepath), "image/png",
                Path.GetFileName(filepath));
        }
        public IActionResult Create()
        {
            ViewBag.Roles = _context.Roles.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            ViewBag.Statuses = _context.Statuses.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] FileUpload data, IFormFile Photo)
        {
            if (Photo.Length > 1000000)
            {
                ModelState.AddModelError(nameof(data.Photo), "Potonya kegedaan coy!!");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            var filename = "photo_" + data.JudulPhoto + Path.GetExtension(Photo.FileName);


            var filepath = Path.Combine(_env.WebRootPath, "upload",filename);
     
            using (var stream = System.IO.File.Create(filepath))
            {
               Photo.CopyTo(stream);
            }
            data.Photo = filename;
            var status = _context.Statuses.FirstOrDefault(x => x.Id == data.Status);
            var share = new SharePhoto()
            {
                NamaPengunggah = data.NamaPengunggah,
                JudulPhoto = data.JudulPhoto,
                TanggalUpload = data.TanggalUpload,
                Deskripsi = data.Deskripsi,
                Photo = filename,
                Status = status
            };

            _context.SharePhoto.Add(share);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Status = _context.Statuses.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            var share = _context.SharePhoto.Include(x => x.Status).FirstOrDefault(x => x.Id == id);
            return View(share);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] SharePhoto data)
        {
            _context.SharePhoto.Update(data);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var share = _context.SharePhoto.FirstOrDefault(x => x.Id == id);
            _context.SharePhoto.Remove(share);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
