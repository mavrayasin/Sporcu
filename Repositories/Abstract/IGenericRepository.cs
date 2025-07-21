using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Sporcu.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        // Örnek metotlar
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        T? GetById(int id);
        IQueryable<T> Find(Expression<Func<T, bool>> expression);

        bool Any(Expression<Func<T, bool>> expression);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        T? FirstOrDefault(Expression<Func<T, bool>> expression);

        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);

        IQueryable<T> FindWithIncludes(Expression<Func<T, bool>> filter,
                                      //int skip = 0,
                                      //int take = int.MaxValue,
                                      //Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                      bool tracking = false);
        bool AnyWithIncludes(Expression<Func<T, bool>> filter,
                                            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        T? FirstOrDefaultWithIncludes(Expression<Func<T, bool>> filter,
                                              Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<T?> FirstOrDefaultWithIncludesAsync(Expression<Func<T, bool>> filter,
                                             Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        IQueryable<T> GetAllWithIncludes(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void DeleteByFilter(Expression<Func<T, bool>> filter);
        T? DeleteById(int id);
        Task<T?> DeleteByIdAsync(int id);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
    }

}
