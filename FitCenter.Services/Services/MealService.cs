using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using FitCenter.Data.Data.Interfaces;
using FitCenter.Models.BindingModels.Meal;
using FitCenter.Models.Model;
using FitCenter.Models.ModelDto;
using FitCenter.Models.ModelDto.Meal;
using FitCenter.Models.ModelDto.Product;
using FitCenter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public MealService(IRepository<Meal> mealRepository, IRepository<Product> productRepository,
            IRepository<User> userRepository, IRepository<MealProducts> mealProductsRepository,
            IProductService productService, IMapper mapper)
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

            var mealAddSuccess = await _mealRepository.AddAsync(meal); //Dodanie posilku bez produktow

            if (!mealAddSuccess)
            {
                response.AddError(Key.Meal, Error.AddError);
                return response;
            }

            //Dla kazdego podanego id produktu dodaj wpis w mealProducts
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

            //Pobranie produktów z tab Product na podstawie id zapisanych w junctionTable
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

            //Zliczalnie kalorycznosci
            //foreach (var product in mealDto.Products)
            //{
            //    mealDto.Kcal += product.Kcal;
            //}

            //mealDto to posiłki z produktami do wyswietlenia na zwrotce
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
                //Pobranie wszystkich id produktów z junction table
                var productsIds = await _mealProductsRepository.GetAllByAsync(x => x.MealId == meal.Id);

                //Dla każdego id produktu pobierz jego dane z Products i dodaj je do meal.Products
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

        public async Task<Response<DeleteMealDto>> DeleteAsync(int mealId)
        {
            var response = new Response<DeleteMealDto>();
            var meal = await _mealRepository.GetByAsync(x => x.Id == mealId);
            if (meal == null)
            {
                response.AddError(Key.Meal, Error.NotExist);
                return response;
            }

            bool deleteSucceed = await _mealRepository.RemoveAsync(meal);
            if (!deleteSucceed)
            {
                response.AddError(Key.Meal, Error.NotExist);
                return response;
            }

            var mealDto = _mapper.Map<DeleteMealDto>(meal);
            response.SuccessResult = mealDto;
            return response;
        }

        public async Task<Response<object>> UpdateAsync(UpdateMealBindingModel bindingModel)
        {
            //sprawdz czy posilek istnieje
            var response = await ValidateUpdateViewModel(bindingModel);
            var mealProductsTmp = new List<MealProducts>();
            if (response.ErrorOccurred)
            {
                return response;
            }

            //pobierz dane posilku do aktualizacji
            var meal = await _mealRepository.GetByAsync(x => x.Id == bindingModel.Id);
            
            //odłącz posiłek z Meal
            _mealRepository.Detach(meal);

            //Dla każdego podanego id produktu w posilku dodaj wpis w MealProducts
            foreach (var productId in bindingModel.ProductsIds)
            {
                var updatedMealProduct = new MealProducts()
                {
                    MealId = meal.Id,
                    ProductId = productId,
                    UserId = meal.UserId,
                    
                };
          
                //Dodaj wpis do listy która posłuży do aktualizacji mealProducts
                mealProductsTmp.Add(updatedMealProduct);
            }


            //Wyciągnięcie istniejącego wpisu produktów aktualizowanego posiłku
            var mealProducts = await _mealProductsRepository.GetAllByAsync(x => x.MealId == meal.Id).Result.ToListAsync();

            //Usunięcie istniejących produktów w posiłku
            foreach (var element in mealProducts)
            {
                bool deleteSucceed = await _mealProductsRepository.RemoveAsync(element);
                if (!deleteSucceed)
                {
                    response.AddError(Key.MealProducts, Error.NotExist);
                    return response;
                }
            }

            var updatedMeal = _mapper.Map<Meal>(bindingModel);
            updatedMeal.MealProducts = mealProductsTmp;
            updatedMeal.UserId = meal.UserId;
            updatedMeal.User = meal.User;
            updatedMeal.Id = meal.Id;

            //Aktualizacja posiłku z produktami
            bool updateSucceed = await _mealRepository.UpdateAsync(updatedMeal);
            if (!updateSucceed) response.AddError(Key.Meal, Error.UpdateError);

            response.SuccessResult = bindingModel;
            return response;
        }

            private async Task<Response<object>> ValidateUpdateViewModel(UpdateMealBindingModel bindingModel)
            {
                var response = new Response<object>();
                bool mealExists = await _mealRepository.ExistAsync(x => x.Id == bindingModel.Id);
                if (!mealExists)
                {
                    response.AddError(Key.Meal, Error.NotExist);
                }

                return response;
            }
    }
}
