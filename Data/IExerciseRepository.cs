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
         Task<IEnumerable<Exercise>> GetExercises();
         Task<Exercise> GetExercise(int id);
         Task<Exercise> CreateExercise(Exercise exerciseToCreate);
    }
}