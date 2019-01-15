using AutoMapper;
using FitCenter.Models.BindingModels.Exercise;
using FitCenter.Models.BindingModels.Meal;
using FitCenter.Models.BindingModels.Product;
using FitCenter.Models.Model;
using FitCenter.Models.ModelDto.Exercise;
using FitCenter.Models.ModelDto.Meal;
using FitCenter.Models.ModelDto.Product;

namespace FitCenter.Services.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, DetailsProductDto>();
            CreateMap<AddProductBindingModel, Product>();
            CreateMap<Product, AddProductDto>();

            CreateMap<Meal, DetailsMealsDto>();
            CreateMap<AddMealBindingModel, Meal>();
            CreateMap<Meal, AddMealDto>();

            CreateMap<Exercise, DetailsExerciseDto>();
            CreateMap<AddExerciseBindingModel, Exercise>();
            CreateMap<Exercise, AddExerciseDto>();

        }
    }
} 