using System.Threading.Tasks;
using AutoMapper;
using backend.Data;
using backend.Dtos;
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
    }
}