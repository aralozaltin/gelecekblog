using System.ComponentModel.DataAnnotations;

namespace BlogProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; }

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre doğrulama zorunludur.")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        [Display(Name = "Şifre Doğrula")]
        public string ConfirmPassword { get; set; }
    }
}
