using System;
using System.Collections.Generic;
using FitCenter.Models.Model;
using FitCenter.Models.ModelDto.UserExerciseResults;

namespace FitCenter.Models.ModelDto.TrainingDiary
{
    public class DetailsTrainingDiaryDto : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Volume { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<DetailsUserExerciseResults> UserExerciseResults = new List<DetailsUserExerciseResults>();

    }
}
