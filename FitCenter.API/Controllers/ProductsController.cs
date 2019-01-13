using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FitCenter.Models.BindingModels.Product;
using FitCenter.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetAsync(int productId)
        {
            var result = await _productService.GetAsync(productId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("AllUserProducts")]
        public async Task<IActionResult> GetAllAsync()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _productService.GetAllAsync(userId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddProductBindingModel bindingModel)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _productService.AddAsync(bindingModel, userId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll([FromQuery]SearchParamsBindingModel searchParams)
        //{
        //    var result = await _productService.GetAllAsync(searchParams);
        //    if (result.ErrorOccurred)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}



        //[HttpPut]
        //public async Task<IActionResult> UpdateAsync(UpdateProductBindingModel bindingModel)
        //{
        //    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        //    var result = await _productService.UpdateAsync(bindingModel, userId);
        //    if (result.ErrorOccurred)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}

        //[HttpDelete("{productName}")]
        //public async Task<IActionResult> DeleteAsync(string productName)
        //{
        //    var result = await _productService.DeleteAsync(productName);
        //    if (result.ErrorOccurred)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}

    }
}
