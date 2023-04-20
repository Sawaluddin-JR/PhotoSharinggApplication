using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PhotoSharinggApplication.Models.ViewModel
{
    public class UserForm
    {
        [Key]
        [Required]
        [RegularExpression(@"^[a-z A-Z]*$", ErrorMessage = "Inputan Hanya Huruf")]
        public string Name { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 4, ErrorMessage = "kalimatnya max 12 minimal 4")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Roles { get; set; }
    }
}
