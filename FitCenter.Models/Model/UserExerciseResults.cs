using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FitCenter.Models.Model
{
    public class UserExerciseResults : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AmountOfReps { get; set; }
        public int Weight { get; set; }
        public int Volume { get; set; }

        [ForeignKey(nameof(ExerciseId))]
        public int ExerciseId { get; set; }
        [ForeignKey(nameof(TrainingDiaryId))]
        public int TrainingDiaryId { get; set; }
    }
}
