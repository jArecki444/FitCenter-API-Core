using System.Security.Claims;
using System.Threading.Tasks;
using FitCenter.Models.BindingModels.Meal;
using FitCenter.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitCenter.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealsController(IMealService mealService)
        {
            this._mealService = mealService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddMealBindingModel bindingModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mealService.AddAsync(bindingModel, userId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("AllUserMeals")]
        public async Task<IActionResult> GetAllAsync()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mealService.GetAllAsync(userId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{mealId}")]
        public async Task<IActionResult> GetAsync(int mealId)
        {
            var result = await _mealService.GetAsync(mealId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
  
        [HttpDelete("{mealId}")]
        public async Task<IActionResult> DeleteAsync(int mealId)
        {
            var result = await _mealService.DeleteAsync(mealId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateMealBindingModel bindingModel)
        {
            var result = await _mealService.UpdateAsync(bindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}