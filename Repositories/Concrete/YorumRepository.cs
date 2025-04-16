using BlogProject.Data;
using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogProject.Repositories.Concrete
{
    public class YorumRepository : IYorumRepository
    {
        private readonly AppDbContext _context;

        public YorumRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Yorum entity)
        {
            await _context.Yorumlar.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(Yorum entity)
        {
            _context.Yorumlar.Remove(entity);
        }

        public async Task<List<Yorum>> FindAsync(Expression<Func<Yorum, bool>> predicate)
        {
            return await _context.Yorumlar.Where(predicate).ToListAsync();
        }

        public async Task<List<Yorum>> GetAllAsync()
        {
            return await _context.Yorumlar.ToListAsync();
        }

        public async Task<Yorum?> GetByIdAsync(int id)
        {
            return await _context.Yorumlar.FindAsync(id);
        }

        public async Task<List<Yorum>> GetYorumlarWithUserAsync(int postId)
        {
            return await _context.Yorumlar
                .Include(y => y.AppUser)
                .Where(y => y.PostId == postId)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Yorum entity)
        {
            _context.Yorumlar.Update(entity);
        }

        public async Task DeleteAsync(Yorum yorum)
        {
            _context.Yorumlar.Remove(yorum);
            await _context.SaveChangesAsync();
        }
    }
}
