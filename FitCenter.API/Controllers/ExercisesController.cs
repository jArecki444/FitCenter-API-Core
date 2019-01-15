using System.Security.Claims;
using System.Threading.Tasks;
using FitCenter.Models.BindingModels.Exercise;
using FitCenter.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitCenter.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExercisesController(IExerciseService exerciseService)
        {
            this._exerciseService = exerciseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddExerciseBindingModel bindingModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _exerciseService.AddAsync(bindingModel, userId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("AllUserExercises")]
        public async Task<IActionResult> GetAllAsync()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _exerciseService.GetAllAsync(userId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{exerciseId}")]
        public async Task<IActionResult> GetAsync(int exerciseId)
        {
            var result = await _exerciseService.GetAsync(exerciseId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{exerciseId}")]
        public async Task<IActionResult> DeleteAsync(int exerciseId)
        {
            var result = await _exerciseService.DeleteAsync(exerciseId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}