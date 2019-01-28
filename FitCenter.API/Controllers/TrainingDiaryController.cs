using System.Security.Claims;
using System.Threading.Tasks;
using FitCenter.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrainingDiaryController : ControllerBase
    {
        private readonly ITrainingDiaryService _trainingDiaryService;

        public TrainingDiaryController(ITrainingDiaryService trainingDiaryService)
        {
            this._trainingDiaryService = trainingDiaryService;
        }

        [HttpGet("{trainingDiaryId}")]
        public async Task<IActionResult> GetAsync(int trainingDiaryId)
        {
            var result = await _trainingDiaryService.GetAsync(trainingDiaryId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
  
        [HttpGet("AllUserTrainingDiaries")]
        public async Task<IActionResult> GetAllAsync()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _trainingDiaryService.GetAllAsync(userId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddAsync(AddExerciseBindingModel bindingModel)
        //{
        //    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        //    var result = await _exerciseService.AddAsync(bindingModel, userId);
        //    if (result.ErrorOccurred)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}

        //[HttpGet("AllUserExercises")]
        //public async Task<IActionResult> GetAllAsync()
        //{
        //    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        //    var result = await _exerciseService.GetAllAsync(userId);
        //    if (result.ErrorOccurred)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}

        //[HttpGet("{exerciseId}")]
        //public async Task<IActionResult> GetAsync(int exerciseId)
        //{
        //    var result = await _exerciseService.GetAsync(exerciseId);
        //    if (result.ErrorOccurred)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}

        //[HttpDelete("{exerciseId}")]
        //public async Task<IActionResult> DeleteAsync(int exerciseId)
        //{
        //    var result = await _exerciseService.DeleteAsync(exerciseId);
        //    if (result.ErrorOccurred)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateAsync(UpdateExerciseBindingModel bindingModel)
        //{
        //    var result = await _exerciseService.UpdateAsync(bindingModel);
        //    if (result.ErrorOccurred)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}
    }
}