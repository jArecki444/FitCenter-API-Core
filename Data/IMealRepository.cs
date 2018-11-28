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
         Task<User> GetUserMeals(int userId);
        Task<Meal> CreateMeal(Meal mealtoCreate);

    }
}