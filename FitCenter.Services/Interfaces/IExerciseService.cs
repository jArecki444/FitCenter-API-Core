using FitCenter.Models.ModelDto;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitCenter.Models.BindingModels.Exercise;
using FitCenter.Models.ModelDto.Exercise;

namespace FitCenter.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<Response<DetailsExerciseDto>> GetAsync(int ExerciseId);
        Task<Response<AddExerciseDto>> AddAsync(AddExerciseBindingModel bindingModel, int userId);
        Task<Response<ICollection<DetailsExerciseDto>>> GetAllAsync(int userId);
    }
}
