using System.Threading.Tasks;
using AutoMapper;
using FitCenter.Data.Data.Interfaces;
using FitCenter.Models.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitCenter.API.Controllers
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserExercise(int userId)
        {
            //var userExercises = await _exerciseRepo.GetUserExercises(userId);
            //var exercisesToReturn = _mapper.Map<userExercisesForDetailedDto>(userExercises);

            return Ok();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddExerciseForUser(int userId, Exercise exerciseForCreationDto)
        {
            //var userFromRepo = await _userRepo.GetUser(userId);
            //var exerciseToCreate = new Exercise
            //{
            //    Name = exerciseForCreationDto.Name,
            //    MuscleGroup = exerciseForCreationDto.MuscleGroup,
            //    CaloriesPerMinute = exerciseForCreationDto.CaloriesPerMinute,
            //    User = userFromRepo,
            //    UserId = userId
            //};
            //var createExercise = await _exerciseRepo.CreateExercise(exerciseToCreate);

            return StatusCode(201);
        }
    }
}