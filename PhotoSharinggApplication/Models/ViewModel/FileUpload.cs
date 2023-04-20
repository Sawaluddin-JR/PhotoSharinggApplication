namespace PhotoSharinggApplication.Models.ViewModel
{
    public class FileUpload
    {
        public string NamaPengunggah { get; set; }

        public string JudulPhoto { get; set; }

        public DateTime TanggalUpload { get; set; }

        public string Deskripsi { get; set; }
        
        public string Status { get; set; }

        public string ?Photo { get; set; }
    }
}
