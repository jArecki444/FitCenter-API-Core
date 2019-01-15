using FitCenter.Models.ModelDto;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitCenter.Models.BindingModels.Meal;
using FitCenter.Models.ModelDto.Meal;

namespace FitCenter.Services.Interfaces
{
    public interface IMealService
    {
        Task<Response<AddMealDto>> AddAsync(AddMealBindingModel bindingModel, int userId);
        Task<Response<ICollection<DetailsMealsDto>>> GetAllAsync(int userId);
        Task<Response<DetailsMealsDto>> GetAsync(int mealId);
        Task<Response<DeleteMealDto>> DeleteAsync(int mealId);
    }
}
