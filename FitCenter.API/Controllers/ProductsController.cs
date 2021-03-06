﻿using System.Security.Claims;
using System.Threading.Tasks;
using FitCenter.Models.BindingModels.Product;
using FitCenter.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateProductBindingModel bindingModel)
        {
            var result = await _productService.UpdateAsync(bindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteAsync(int productId)
        {
            var result = await _productService.DeleteAsync(productId);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
