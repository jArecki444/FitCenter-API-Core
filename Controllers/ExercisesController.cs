using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseRepository _exerciseRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        
        public ExercisesController(IExerciseRepository exerciseRepo, IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _exerciseRepo = exerciseRepo;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddExerciseForUser(int userId, ExerciseForCreationDto exerciseForCreationDto)
        {
            var userFromRepo = await _userRepo.GetUser(userId);
            var exerciseToCreate = new Exercise
            {
                Name = exerciseForCreationDto.Name,
                MuscleGroup = exerciseForCreationDto.MuscleGroup,
                CaloriesPerMinute = exerciseForCreationDto.CaloriesPerMinute,
                User = userFromRepo,
                UserId = userId
            };
            var createExercise = await _exerciseRepo.CreateExercise(exerciseToCreate);

            return StatusCode(201);
        }
    }
}