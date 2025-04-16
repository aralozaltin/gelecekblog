using BlogProject.Models;

namespace BlogProject.Repositories.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetPostsWithKategoriAsync();
        Task<Post?> GetPostWithDetailsAsync(int id);
        Task<List<Post>> GetAllVisibleWithRelationsAsync();
        Task<Post?> GetPostWithFullDetailsAsync(int id);
        Task UpdateAsync(Post post);
        Task DeleteAsync(Yorum yorum);    
    }
}
