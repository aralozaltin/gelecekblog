using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlogProject.Models
{
    public class AppUser : IdentityUser
    {
        public string AdSoyad { get; set; }

        // Navigation properties
        public ICollection<Post> Postlar { get; set; }
        public ICollection<Yorum> Yorumlar { get; set; }
        public bool IsDeleted { get; set; } = false;
        
    }
}
