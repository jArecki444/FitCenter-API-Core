using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FitCenter.Data.Data.Interfaces;
using FitCenter.Models.Model;
using Microsoft.EntityFrameworkCore;
using FitCenter.Data.DbContext;

namespace FitCenter.Data.Data
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(DataContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await SaveAsync();
        }

        public async Task<T> GetByAsync(Expression<Func<T, bool>> getBy, bool withTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (!withTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(getBy);
        }

        public async Task<IQueryable<T>> GetAllAsync(bool withTracking = false, params Expression<Func<T, object>>[] includes)
        {
            return await Task.Run(() => GetAll(withTracking, includes));
        }

        public async Task<IQueryable<T>> GetAllByAsync(Expression<Func<T, bool>> getBy, bool withTracking = true)
        {

            return await Task.Run(() => GetAll(withTracking).Where(getBy).AsQueryable());
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            var result = await SaveAsync();
            return result;
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> getBy)
        {
            return await _dbSet.AnyAsync(getBy);
        }

        public async Task<bool> IsEmptyAsync()
        {
            return !await _dbSet.AnyAsync();
        }

        public void Detach(Entity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        public IQueryable<T> GetAll(bool withTracking = false, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (!withTracking)
            {
                query = query.AsNoTracking();
            }
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        private async Task<bool> SaveAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}