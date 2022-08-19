using System.Linq.Expressions;

namespace ExamTask.Data.IRepositories
{
    public interface IGenericRepository<T>
        where T : class
    {
        T Update(T entity);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null);
        Task<T> CreateAsync(T entity);
    }
}
