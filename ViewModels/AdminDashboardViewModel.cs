using BlogProject.Models;

namespace BlogProject.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int KullaniciSayisi { get; set; }
        public int PostSayisi { get; set; }
        public int YorumSayisi { get; set; }

        public List<Post> SonPostlar { get; set; } = new();
        public List<Yorum> SonYorumlar { get; set; } = new();
    }
}
