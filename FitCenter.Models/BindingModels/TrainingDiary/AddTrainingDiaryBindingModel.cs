using System;
using System.Collections.Generic;
using System.Text;
using FitCenter.Models.Model;
using FitCenter.Models.ModelDto.UserExerciseResults;

namespace FitCenter.Models.BindingModels.TrainingDiary
{
    public class AddTrainingDiaryBindingModel
    {
        public string Name { get; set; }
        public int Volume { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<DetailsUserExerciseResults> UserExerciseResults { get; set; }
    }
}
