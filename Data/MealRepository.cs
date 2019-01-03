using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
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

        public async Task<User> GetUserMeals(int userId)
        {
            var meals = await _context.Users.Include(m => m.Meals).FirstOrDefaultAsync(u => u.Id == userId);
            return meals;
        }
        public async Task<Meal> CreateMeal(Meal mealToCreate)
        {

            await _context.Meals.AddAsync(mealToCreate);
            await _context.SaveChangesAsync();

            return mealToCreate;
        }                

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}