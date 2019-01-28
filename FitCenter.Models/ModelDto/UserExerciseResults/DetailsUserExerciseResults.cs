using System;
using System.Collections.Generic;
using System.Text;
using FitCenter.Models.Model;

namespace FitCenter.Models.ModelDto.UserExerciseResults
{
    public class DetailsUserExerciseResults : Entity
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public int AmountOfReps { get; set; }
        public int Weight { get; set; }
        public int Volume { get;set; }
    }
}
