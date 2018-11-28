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

        public async Task<Exercise> CreateExercise(Exercise exerciseToCreate)
        {

            await _context.Exercises.AddAsync(exerciseToCreate);
            await _context.SaveChangesAsync();

            return exerciseToCreate;
        }        

        public async Task<User> GetUserExercises(int userId)
        {
            // var exercise = await _context.Exercises.Include(u => u.User).FirstOrDefaultAsync(e => e.Id == userId);
            var exercise = await _context.Users.Include(e => e.Exercises).FirstOrDefaultAsync(u => u.Id == userId);
            return exercise;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}