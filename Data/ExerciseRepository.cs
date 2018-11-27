using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly DataContext _context;
        public ExerciseRepository(DataContext context)
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

        public async Task<Exercise> GetExercise(int id)
        {
            var exercise = await _context.Exercises.Include(u => u.User).FirstOrDefaultAsync(e => e.Id == id);
            return exercise;
        }

        public async Task<IEnumerable<Exercise>> GetExercises()
        {
            var exercises = await _context.Exercises.Include(u => u.User).ToListAsync();
            return exercises;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}