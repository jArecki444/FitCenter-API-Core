using AutoMapper;
using FitCenter.Models.BindingModels.Exercise;
using FitCenter.Models.BindingModels.Meal;
using FitCenter.Models.BindingModels.Product;
using FitCenter.Models.BindingModels.TrainingDiary;
using FitCenter.Models.BindingModels.User;
using FitCenter.Models.Model;
using FitCenter.Models.ModelDto.Exercise;
using FitCenter.Models.ModelDto.Meal;
using FitCenter.Models.ModelDto.Product;
using FitCenter.Models.ModelDto.User;
using FitCenter.Models.ModelDto.UserExerciseResults;

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

            CreateMap<User, DetailsUserDto>();
            CreateMap<UpdateUserBindingModel, User>();

            CreateMap<UserExerciseResults, DetailsUserExerciseResults>();

            CreateMap<AddTrainingDiaryBindingModel, TrainingDiary>();
            CreateMap<DetailsUserExerciseResults, UserExerciseResults>();

    }
}
} 