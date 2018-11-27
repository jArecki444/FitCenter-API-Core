using AutoMapper;
using backend.Dtos;
using Backend.Models;

namespace backend.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForDetailedDto>();
            CreateMap<Exercise, ExerciseForDetailedDto>();
            CreateMap<Meal, MealForDetailedDto>();
        }
    }
}