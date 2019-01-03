using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;

namespace backend.Data
{
    public interface IExerciseRepository
    {
        void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<User> GetUserExercises(int userId);
         Task<Exercise> CreateExercise(Exercise exerciseToCreate);
    }
}