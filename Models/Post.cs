using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Baslik { get; set; }

        [Required]
        public string Icerik { get; set; }

        public DateTime OlusturmaTarihi { get; set; }

        [Required]
        public string GorselYolu { get; set; }

        // Foreign Keys
        public string AppUserId { get; set; }
        public int KategoriId { get; set; }

        public bool IsDeleted { get; set; }

        // Navigation properties
        public AppUser Kullanici { get; set; }
        public Kategori Kategori { get; set; }
        public ICollection<Yorum> Yorumlar { get; set; }
    }
}
