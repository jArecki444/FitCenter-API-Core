﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitCenter.Data.Data.Interfaces;
using FitCenter.Models.BindingModels.Product;
using FitCenter.Models.Model;
using FitCenter.Models.ModelDto;
using FitCenter.Models.ModelDto.Product;
using FitCenter.Services.Interfaces;
using Paneleo.Services;

namespace FitCenter.Services.Services
{
    public class ProductService : IProductService
    {

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<User> _userRepository;


        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository, IRepository<User> userRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;

            _mapper = mapper;
        }

        public async Task<Response<DetailsProductDto>> GetAsync(int productId)
        {
            var response = new Response<DetailsProductDto>();

            var product = await _productRepository.GetByAsync(x => x.Id == productId);

            if (product == null)
            {
                response.AddError(Key.Product, Error.NotExist);
                return response;
            }

            var productDto = _mapper.Map<DetailsProductDto>(product);

            response.SuccessResult = productDto;

            return response;
        }

        public async Task<Response<AddProductDto>> AddAsync(AddProductBindingModel bindingModel, int userId)
        {
            var response = new Response<AddProductDto>();

            var user = await _userRepository.GetByAsync(x => x.Id == userId);

            if (user == null)
            {
                response.AddError(Key.User, Error.NotExist);
                return response;
            }

            var product = _mapper.Map<Product>(bindingModel);

            product.User = user;
            product.UserId = userId;

            var productAddSuccess = await _productRepository.AddAsync(product);

            if (!productAddSuccess)
            {
                response.AddError(Key.Product, Error.AddError);
                return response;
            }

            var productDto = _mapper.Map<AddProductDto>(product);

            response.SuccessResult = productDto;

            return response;
        }

        public async Task<Response<ICollection<DetailsProductDto>>> GetAllAsync(int userId)
        {
            var response = new Response<ICollection<DetailsProductDto>> ();
            var products = await _productRepository.GetAllByAsync(u => u.UserId == userId);
            if (products == null)
            {
                response.AddError(Key.Product,Error.NotExist);
                return response;
            }

            var productsDto = _mapper.Map<ICollection<DetailsProductDto>>(products);
            response.SuccessResult = productsDto;
            return response;
        }
    }
}
