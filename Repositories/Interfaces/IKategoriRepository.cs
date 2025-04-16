using BlogProject.Models;

namespace BlogProject.Repositories.Interfaces
{
    public interface IKategoriRepository : IGenericRepository<Kategori>
    {
        Task<bool> HasPostsAsync(int kategoriId);
    }
}
