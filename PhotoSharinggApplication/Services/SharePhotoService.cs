using PhotoSharinggApplication.Data;
using PhotoSharinggApplication.Models;

namespace PhotoSharinggApplication.Services
{
    public class SharePhotoService : ISharePhotoService
    {
        private readonly AppDbContext _context;

        public SharePhotoService(AppDbContext context)
        {
            _context = context;
        }
        public SharePhoto GetSaveSharePhoto()
        {
            return _context.SharePhoto.SingleOrDefault();
        }

        public SharePhoto Save(SharePhoto photo)
        {
            _context.SharePhoto.Add(photo);
            _context.SaveChanges();
            return photo;
        }
    }
}
