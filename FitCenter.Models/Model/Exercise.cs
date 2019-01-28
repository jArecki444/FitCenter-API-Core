using System.Collections.Generic;

namespace FitCenter.Models.Model
{
    public class Exercise : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<UserExerciseResults> UserExerciseResults { get; set; }
    }
}