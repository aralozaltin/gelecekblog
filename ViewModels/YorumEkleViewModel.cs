using System.ComponentModel.DataAnnotations;

namespace BlogProject.ViewModels
{
    public class YorumEkleViewModel
    {
        [Required]
        public string Metin { get; set; } = null!;
        public int PostId { get; set; }
    }
}
