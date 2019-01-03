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
            CreateMap<User, userMealsForDetailedDto>();
            CreateMap<Exercise, ExerciseForDetailedDto>();
            CreateMap<Meal, userMealsForDetailedDto>();
            CreateMap<UserForEditDto, User>();
        }
    } 
}