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
using FitCenter.Models.ModelDto.Product;
using FitCenter.Services.Interfaces;
using Paneleo.Services;

namespace FitCenter.Services.Services
{
    public class MealService : IMealService
    {
        private readonly IRepository<Meal> _mealRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<MealProducts> _mealProductsRepository;

        private readonly IProductService _productService;

        private readonly IMapper _mapper;

        public MealService(IRepository<Meal> mealRepository, IRepository<User> userRepository, IRepository<MealProducts> mealProductsRepository, IProductService productService, IMapper mapper)
        {
            _mealRepository = mealRepository;
            _userRepository = userRepository;
            _mealProductsRepository = mealProductsRepository;

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
    }
}
