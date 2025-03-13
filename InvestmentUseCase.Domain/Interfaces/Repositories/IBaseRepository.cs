using System.Linq.Expressions;

namespace InvestmentUseCase.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetByExpressionAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllAsync();
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
    }
}
