using System;
using System.Collections.Generic;
using System.Text;
using FitCenter.Models.Model;

namespace FitCenter.Models.ModelDto.Exercise
{
    public class AddExerciseDto : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
    }
}
