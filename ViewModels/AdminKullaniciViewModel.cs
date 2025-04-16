using BlogProject.Models;
using System.Collections.Generic;

namespace BlogProject.ViewModels
{
    public class AdminKullaniciViewModel
    {
        public List<AppUser> Kullanicilar { get; set; } = new();
    }
}
