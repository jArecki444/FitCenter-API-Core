using System.Collections.Generic;

namespace backend.Dtos
{
    public class userExercisesForDetailedDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<ExerciseForDetailedDto> Exercises { get; set; }
        
    }
}