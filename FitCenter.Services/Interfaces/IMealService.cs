using FitCenter.Models.ModelDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FitCenter.Models.BindingModels.Meal;
using FitCenter.Models.BindingModels.Product;
using FitCenter.Models.ModelDto.Product;

namespace FitCenter.Services.Interfaces
{
    public interface IMealService
    {
        Task<Response<AddMealDto>> AddAsync(AddMealBindingModel bindingModel, int userId);
    }
}
