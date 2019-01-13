using System.Threading.Tasks;
using FitCenter.Models.Model;

namespace FitCenter.Data.Data.Interfaces
{
    public interface IUserRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<User> GetUser(int id);
    }
}