
namespace PhotoSharinggApplication.Services
{
    public class FileService
    {
        IWebHostEnvironment _alat;
        public FileService(IWebHostEnvironment a)
        {
            _alat = a;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            string namaFolder = "File";

            if (file == null)
            {
                return string.Empty;
            }

            var savePath = Path.Combine(_alat.WebRootPath, namaFolder);

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            var namaFile = file.FileName;
            var alamatFile = Path.Combine(savePath, namaFile);

            using (var stream = new FileStream(alamatFile, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("/" + namaFolder + "/", namaFile);
        }
    }
}
