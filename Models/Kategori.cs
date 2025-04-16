using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public class Kategori
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı boş olamaz.")]
        public string Ad { get; set; } = null!;

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
