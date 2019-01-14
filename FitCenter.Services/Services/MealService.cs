using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitCenter.Data.Data.Interfaces;
using FitCenter.Models.BindingModels.Meal;
using FitCenter.Models.BindingModels.Product;
using FitCenter.Models.Model;
using FitCenter.Models.ModelDto;
using FitCenter.Models.ModelDto.Meal;
using FitCenter.Models.ModelDto.Product;
using FitCenter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Paneleo.Services;

namespace FitCenter.Services.Services
{
    public class MealService : IMealService
    {
        private readonly IRepository<Meal> _mealRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<MealProducts> _mealProductsRepository;

        private readonly IProductService _productService;

        private readonly IMapper _mapper;

        public MealService(IRepository<Meal> mealRepository, IRepository<Product> productRepository, IRepository<User> userRepository, IRepository<MealProducts> mealProductsRepository, IProductService productService, IMapper mapper)
        {
            _mealRepository = mealRepository;
            _userRepository = userRepository;
            _mealProductsRepository = mealProductsRepository;
            _productRepository = productRepository;

            _productService = productService;
            _mapper = mapper;
        }

        public async Task<Response<AddMealDto>> AddAsync(AddMealBindingModel bindingModel, int userId)
        {
            var response = new Response<AddMealDto>();
            var mealProductsTmp = new List<MealProducts>();
            
            var user = await _userRepository.GetByAsync(x => x.Id == userId);

            if (user == null)
            {
                response.AddError(Key.User, Error.NotExist);
                return response;
            }

            var meal = _mapper.Map<Meal>(bindingModel);

            meal.User = user;
            meal.UserId = userId;

            var mealAddSuccess = await _mealRepository.AddAsync(meal);

            if (!mealAddSuccess)
            {
                response.AddError(Key.Meal, Error.AddError);
                return response;
            }

            foreach (var productId in bindingModel.ProductsIds)
            {
                var mealProduct = new MealProducts()
                {
                    MealId = meal.Id,
                    ProductId = productId,
                    UserId = userId
                };
                var addMealProductSuccess = await _mealProductsRepository.AddAsync(mealProduct);
                if (!addMealProductSuccess)
                {
                    response.AddError(Key.MealProducts, Error.AddError);
                    return response;
                }
                mealProductsTmp.Add(mealProduct);
            }

            meal.MealProducts = mealProductsTmp;

            var mealDto = _mapper.Map<AddMealDto>(meal);
            mealDto.Products = new List<DetailsProductDto>();

            foreach (var mealProduct in meal.MealProducts)
            {
                var productResponse = await _productService.GetAsync(mealProduct.ProductId);

                if (productResponse.ErrorOccurred)
                {
                    response.AddError(Key.Product, Error.NotExist);
                    return response;
                }
                mealDto.Products.Add(productResponse.SuccessResult);
            }

            response.SuccessResult = mealDto;

            return response;
        }

        //Pobieranie wszystkich posiłków użytkownika wraz z tablicą ich produktów
        public async Task<Response<ICollection<DetailsMealsDto>>> GetAllAsync(int userId)
        {
            var response = new Response<ICollection<DetailsMealsDto>>();
            var meals = (await _mealRepository.GetAllByAsync(u => u.UserId == userId).Result.ToListAsync());

            if (meals == null || meals.Count == 0) 
            {
                response.AddError(Key.Meal, Error.NotExist);
                return response;
            }
            var mealsDto = _mapper.Map<ICollection<DetailsMealsDto>>(meals);

            foreach (var meal in mealsDto)
            {
                var productsIds = await _mealProductsRepository.GetAllByAsync(x => x.MealId == meal.Id);
                
                foreach (var product in productsIds)
                {
                    var productTmp = await _productRepository.GetByAsync(x => x.Id == product.ProductId);
                    meal.Products.Add(_mapper.Map<DetailsProductDto>(productTmp));
                }
            }
            response.SuccessResult = mealsDto;
            return response;
        }

        public async Task<Response<DetailsMealsDto>> GetAsync(int mealId)
        {
            var response = new Response<DetailsMealsDto>();
            var meal = (await _mealRepository.GetByAsync(x => x.Id == mealId));

            if (meal == null)
            {
                response.AddError(Key.Meal, Error.NotExist);
                return response;
            }

            var mealDto = _mapper.Map<DetailsMealsDto>(meal);

            //mealDto.Products = new List<DetailsProductDto>();

            var productsIds = await _mealProductsRepository.GetAllByAsync(x => x.MealId == mealId);
            foreach (var product in productsIds)
            {
                var productTmp = await _productRepository.GetByAsync(x => x.Id == product.ProductId);
                var productDto = _mapper.Map<DetailsProductDto>(productTmp);
                mealDto.Products.Add(productDto);
            }

            response.SuccessResult = mealDto;
            return response;
        }
    }
}
