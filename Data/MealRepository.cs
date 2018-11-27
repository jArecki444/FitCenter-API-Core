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

        public async Task<Meal> GetMeal(int id)
        {
            var meal = await _context.Meals.Include(u => u.User).FirstOrDefaultAsync(m => m.Id == id);
            return meal;
        }

        public async Task<IEnumerable<Meal>> GetMeals()
        {
            var meals = await _context.Meals.Include(u => u.User).ToListAsync();
            return meals;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}