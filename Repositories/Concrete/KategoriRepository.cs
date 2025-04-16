using BlogProject.Data;
using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogProject.Repositories.Concrete
{
    public class KategoriRepository : IKategoriRepository
    {
        private readonly AppDbContext _context;

        public KategoriRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Kategori entity)
        {
            await _context.Kategoriler.AddAsync(entity);
        }

        public void Delete(Kategori entity)
        {
            _context.Kategoriler.Remove(entity);
        }

        public async Task<List<Kategori>> FindAsync(Expression<Func<Kategori, bool>> predicate)
        {
            return await _context.Kategoriler.Where(predicate).ToListAsync();
        }

        public async Task<List<Kategori>> GetAllAsync()
        {
            return await _context.Kategoriler.ToListAsync();
        }

        public async Task<Kategori?> GetByIdAsync(int id)
        {
            return await _context.Kategoriler.FindAsync(id);
        }

        public async Task<bool> HasPostsAsync(int kategoriId)
        {
            return await _context.Posts.AnyAsync(p => p.KategoriId == kategoriId);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Kategori entity)
        {
            _context.Kategoriler.Update(entity);
        }
    }
}
