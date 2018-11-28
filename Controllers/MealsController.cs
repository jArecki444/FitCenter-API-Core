using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IMealRepository _mealRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public MealsController(IMealRepository mealRepo, IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _mealRepo = mealRepo;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserMeals(int userId)
        {
            var userMeals = await _mealRepo.GetUserMeals(userId);
            var mealsToReturn = _mapper.Map<userMealsForDetailedDto>(userMeals);
            return Ok(mealsToReturn);
        }
        
        [HttpPost("Add")]
        public async Task<IActionResult> AddMealForUser(int userId, MealForCreationDto mealForCreationDto)
        {
            var userFromRepo = await _userRepo.GetUser(userId);
            var mealToCreate = new Meal 
            {
                Name = mealForCreationDto.Name,
                Kcal = mealForCreationDto.Kcal,
                Proteins = mealForCreationDto.Proteins,
                Carbohydrates = mealForCreationDto.Carbohydrates,
                Fat = mealForCreationDto.Fat,
                User = userFromRepo,
                UserId = userId                    
            };
            var createMeal = await _mealRepo.CreateMeal(mealToCreate);
            return StatusCode(201);
        }
    }
}