using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sporcu.Repositories.Abstract;
using System.Linq.Expressions;

namespace Sporcu.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }


        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public T? FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().FirstOrDefault(expression);
        }

        public async  Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
           return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }
        public bool AnyWithIncludes(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            var _resetSet = filter != null ? _context.Set<T>().Where(filter).AsQueryable() : _context.Set<T>().AsQueryable();
            if (include != null)
            {
                _resetSet = include(_resetSet);
            }

            return _resetSet.Any();
        }
        public IQueryable<T> FindWithIncludes(Expression<Func<T, bool>> filter,
                                       //int skip = 0,
                                       //int take = int.MaxValue,
                                       //Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                       bool tracking = false)
        {
            var _resetSet = filter != null ? _context.Set<T>().Where(filter).AsQueryable() : _context.Set<T>().AsQueryable();
            if (include != null)
            {
                _resetSet = include(_resetSet);
            }
            if (!tracking)
            {
                _resetSet = _resetSet.AsNoTracking();
            }
            return _resetSet;
        }
        public T? FirstOrDefaultWithIncludes(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            var _resetSet = filter != null ? _context.Set<T>().Where(filter).AsQueryable() : _context.Set<T>().AsQueryable();

            if (include != null)
            {
                _resetSet = include(_resetSet);
            }
            return _resetSet.FirstOrDefault();
        }

        public async Task<T?> FirstOrDefaultWithIncludesAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            var _resetSet = filter != null ? _context.Set<T>().Where(filter).AsQueryable() : _context.Set<T>().AsQueryable();

            if (include != null)
            {
                _resetSet = include(_resetSet);
            }
            return await _resetSet.FirstOrDefaultAsync();
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> GetAllWithIncludes(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            var _resetSet = _context.Set<T>().AsQueryable();

            if (include != null)
            {
                _resetSet = include(_resetSet);
            }

            return _resetSet.AsQueryable();
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
          return _dbSet.AddRangeAsync(entities);
        }

        public void DeleteByFilter(Expression<Func<T, bool>> filter)
        {
            var _resetSet = _context.Set<T>().Where(filter).AsQueryable();

            foreach (var item in _resetSet)
            {
                _context.Set<T>().Remove(item);
            }
        }

        public T? DeleteById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
                return _context.Set<T>().Remove(entity).Entity;
            else return null;
        }

        public async Task<T?> DeleteByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
                return _context.Set<T>().Remove(entity).Entity;
            else return null;
        }
            
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
