using System;
using System.Collections.Generic;

namespace FitCenter.Models.Model
{
    public class TrainingDiary : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Volume { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<UserExerciseResults> UserExerciseResults { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}