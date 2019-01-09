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
        public async Task<ActionResult> GetUserProducts(int userId)
        {
            var userProducts = await _mealRepo.GetUserProducts(userId);
            var productsToReturn = _mapper.Map<userProductsForDetailedDto>(userProducts);
            return Ok(productsToReturn);
        }
        
        // [HttpPost("{userId}")]
        // public async Task<IActionResult> AddMealForUser(int userId, MealForCreationDto mealForCreationDto)
        // {
        //     var mealToCreate = new Meal 
        //     {
        //         MealName = mealForCreationDto.Name,
        //         Kcal = mealForCreationDto.Kcal,
        //         Proteins = mealForCreationDto.Proteins,
        //         Carbohydrates = mealForCreationDto.Carbohydrates,
        //         Fat = mealForCreationDto.Fat,
        //         Products = mealForCreationDto.Product[],
        //         UserId = userId                    
        //     };
        //     var createMeal = await _mealRepo.CreateMeal(mealToCreate);
        //     return StatusCode(201);
        // }
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddMealForUser(int userId, Meal mealForCreationDto)
        {
            var mealToCreate = new Meal 
            {
                MealName = mealForCreationDto.MealName,
                Kcal = mealForCreationDto.Kcal,
                Proteins = mealForCreationDto.Proteins,
                Carbohydrates = mealForCreationDto.Carbohydrates,
                Fat = mealForCreationDto.Fat,
                Products = mealForCreationDto.Products,
                UserId = userId                    
            };
            var createMeal = await _mealRepo.CreateMeal(mealToCreate);
            return StatusCode(201);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProductForUser(int userId, ProductForCreationDto productForCreationDto)
        {
            var userFromRepo = await _userRepo.GetUser(userId);
            var productToCreate = new Product 
            {
                Name = productForCreationDto.Name,
                Kcal = productForCreationDto.Kcal,
                Proteins = productForCreationDto.Proteins,
                Carbohydrates = productForCreationDto.Carbohydrates,
                Fat = productForCreationDto.Fat,
                User = userFromRepo,
                UserId = userId                    
            };
            var createProduct = await _mealRepo.CreateProduct(productToCreate);
            return StatusCode(201);
        }
    }
}