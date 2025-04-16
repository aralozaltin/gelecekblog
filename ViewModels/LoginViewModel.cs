// ViewModels klasöründe yeni dosya oluştur: LoginViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace BlogProject.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
