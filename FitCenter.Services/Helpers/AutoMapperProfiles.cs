using AutoMapper;
using FitCenter.Models.BindingModels.Meal;
using FitCenter.Models.BindingModels.Product;
using FitCenter.Models.Model;
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

            CreateMap<AddMealBindingModel, Meal>();
            CreateMap<Meal, AddMealDto>();

        }
    }
} 