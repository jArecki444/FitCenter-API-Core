using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FitCenter.Models.Model;

namespace FitCenter.Data.Data.Interfaces
{
    public interface IRepository<T>
    {
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<T> GetByAsync(Expression<Func<T, bool>> getBy, bool withTracking = true);
        Task<IQueryable<T>> GetAllAsync(bool withTracking = false, params Expression<Func<T, object>>[] includes);
        Task<IQueryable<T>> GetAllByAsync(Expression<Func<T, bool>> getBy, bool withTracking = true);
        Task<bool> RemoveAsync(T entity);
        Task<bool> ExistAsync(Expression<Func<T, bool>> getBy);
        Task<bool> IsEmptyAsync();
        void Detach(Entity entity);
    }
}