using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlogProject.Models;

namespace BlogProject.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { 
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Yorum -> Post ilişkisi için cascade delete davranışını kapatıyoruz.
            builder.Entity<Yorum>()
                .HasOne(y => y.Post)
                .WithMany(p => p.Yorumlar)
                .HasForeignKey(y => y.PostId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
