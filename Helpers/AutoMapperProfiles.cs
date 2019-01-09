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
            CreateMap<User, userExercisesForDetailedDto>();
            CreateMap<User, userProductsForDetailedDto>();
            CreateMap<Exercise, ExerciseForDetailedDto>();
            CreateMap<Meal, userProductsForDetailedDto>();
        }
    } 
}