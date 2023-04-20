using PhotoSharinggApplication.Models;

namespace PhotoSharinggApplication.Services
{
    public interface ISharePhotoService
    {
       SharePhoto Save(SharePhoto photo);
        SharePhoto GetSaveSharePhoto();
    }
}
