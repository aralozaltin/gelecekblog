using System.ComponentModel.DataAnnotations;


namespace BlogProject.Models
{
    public class Yorum
    {
        public int Id { get; set; }

        [Required]
        public string Metin { get; set; }

        public DateTime Tarih { get; set; } = DateTime.Now;

        // Foreign keys
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
        
        public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
    }

}