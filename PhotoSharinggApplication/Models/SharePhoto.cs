using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PhotoSharinggApplication.Models
{
    public class SharePhoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string NamaPengunggah { get; set; }
        [Required]
        public string JudulPhoto { get; set; }
        [Required]
        public DateTime TanggalUpload { get; set; }
        [Required]
        public string? Photo { get; set; }
        [Required]
        public string Deskripsi { get; set; }
        [Required]
        public Status Status { get; set; }
    }
}
