using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.ViewModels
{
    public class PostCreateViewModel
    {
        [Required]
        public string Baslik { get; set; }

        [Required]
        public string Icerik { get; set; }

        [Required]
        public int KategoriId { get; set; }

        [Required]
        public IFormFile Gorsel { get; set; }

    }
}
