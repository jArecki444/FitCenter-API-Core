using System.Collections.Generic;
using System.Threading.Tasks;
using FitCenter.Data.Data.Interfaces;
using FitCenter.Data.DbContext;
using FitCenter.Models.Model;
using Microsoft.EntityFrameworkCore;

namespace FitCenter.Data.Data
{
    public class MealRepository : IMealRepository
    {
        private readonly DataContext _context;
        public MealRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUserProducts(int userId)
        {
            var products = await _context.Users.Include(m => m.Products).FirstOrDefaultAsync(u => u.Id == userId);
            return products;
        }

        public async Task<Product> GetProduct(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(u => u.Id == productId);
            return product;
        }

        //Przy wyciąganiu z tabeli User
        public async Task<User> GetMealsByUserId(int userId)
        {
            var meals = await _context.Users.Include(m => m.Meals).FirstOrDefaultAsync(u => u.Id == userId);
            return meals;
        }

        // Przy wyciaganiu z tabeli Meal
        // public async Task<Meal> GetMealsByUserId(int userId)
        // {
        //     var meals = await _context.Meals.FirstOrDefaultAsync(u => u.UserId == userId); //zwróci tylko jeden posiłek uzytkownika
        //     return meals;
        // }

        public async Task<int> GetMealProductId(int mealId)
        {
            ;
            var productId = await _context.MealProducts.Include(p => p.ProductId).FirstOrDefaultAsync(m => m.MealId == mealId);
            // _mapper.Map<UserMealsForDetailedDto>(productId);
            return productId.Id;
        }

        public async Task<Product> CreateProduct(Product productToCreate)
        {

            await _context.Products.AddAsync(productToCreate);
            await _context.SaveChangesAsync();

            return productToCreate;
        }
        public async Task<Meal> CreateMeal(Meal mealToCreate)
        {

            await _context.Meals.AddAsync(mealToCreate);
            await _context.SaveChangesAsync();

            return mealToCreate;
        }
        public async Task<MealProducts> InsertMealProducts(MealProducts mealProduct)
        {
            await _context.MealProducts.AddAsync(mealProduct);
            await _context.SaveChangesAsync();

            return mealProduct;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}