using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;

namespace backend.Data
{
    public interface IMealRepository
    {
        void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<Meal>> GetMeals();
         Task<Meal> GetMeal(int id);
    }
}