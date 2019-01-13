using System.Collections.Generic;
using System.Threading.Tasks;
using FitCenter.Models.Model;

namespace FitCenter.Data.Data.Interfaces
{
    public interface IMealRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<User> GetUserProducts(int userId);
        Task<Product> GetProduct(int userId);

        Task<User> GetMealsByUserId(int userId);
        // Przy wyciaganiu z tabeli User

        // Task<Meal> GetMealsByUserId(int userId);
        //Przy wyciąganiu z tabeli Meal
        Task<int> GetMealProductId(int mealId);
        Task<Product> CreateProduct(Product productToCreate);
        Task<Meal> CreateMeal(Meal mealToCreate);
        Task<MealProducts> InsertMealProducts(MealProducts mealProduct);

    }
}