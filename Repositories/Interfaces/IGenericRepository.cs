using System.Linq.Expressions;

namespace BlogProject.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
