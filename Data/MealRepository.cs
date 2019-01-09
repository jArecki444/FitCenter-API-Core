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

        public async Task<User> GetUserProducts(int userId)
        {
            var products = await _context.Users.Include(m => m.Products).FirstOrDefaultAsync(u => u.Id == userId);
            return products;
        }
        public async Task<Product> CreateProduct(Product productToCreate)
        {

            await _context.Products.AddAsync(productToCreate);
            await _context.SaveChangesAsync();

            return productToCreate;
        }                

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}