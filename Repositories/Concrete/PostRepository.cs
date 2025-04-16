using BlogProject.Data;
using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogProject.Repositories.Concrete
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Post entity)
        {
            await _context.Posts.AddAsync(entity);
        }

        public void Delete(Post entity)
        {
            _context.Posts.Remove(entity);
        }

        public async Task<List<Post>> FindAsync(Expression<Func<Post, bool>> predicate)
        {
            return await _context.Posts.Where(predicate).ToListAsync();
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<List<Post>> GetPostsWithKategoriAsync()
        {
            return await _context.Posts
                .Include(p => p.Kategori)
                .Include(p => p.Kullanici)
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.OlusturmaTarihi)
                .ToListAsync();
        }

        public async Task<Post?> GetPostWithDetailsAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.Kategori)
                .Include(p => p.Kullanici)
                .Include(p => p.Yorumlar)
                    .ThenInclude(y => y.AppUser)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Post entity)
        {
            _context.Posts.Update(entity);
        }

        public async Task<List<Post>> GetAllVisibleWithRelationsAsync()
        {
            return await _context.Posts
                .Where(p => !p.IsDeleted)
                .Include(p => p.Kategori)
                .Include(p => p.Kullanici)
                .OrderByDescending(p => p.OlusturmaTarihi)
                .ToListAsync();
        }
        

        public async Task<Post?> GetPostWithFullDetailsAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.Kategori)
                .Include(p => p.Kullanici)
                .Include(p => p.Yorumlar).ThenInclude(y => y.AppUser)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Yorum yorum)
        {
            _context.Yorumlar.Remove(yorum);
            await _context.SaveChangesAsync();
        }
    }
}
