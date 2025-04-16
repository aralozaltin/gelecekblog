using BlogProject.Models;

namespace BlogProject.Repositories.Interfaces
{
    public interface IYorumRepository : IGenericRepository<Yorum>
    {
        Task<List<Yorum>> GetYorumlarWithUserAsync(int postId);

        // Eğer özel işlem varsa bırak, yoksa IGenericRepository'deki Delete zaten yeterlidir.
        Task DeleteAsync(Yorum yorum);
    }
}