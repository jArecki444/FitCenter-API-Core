using FitCenter.Models.ModelDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FitCenter.Models.BindingModels.Product;
using FitCenter.Models.ModelDto.Product;

namespace FitCenter.Services.Interfaces
{
    public interface IProductService
    {
        Task<Response<DetailsProductDto>> GetAsync(int productId);
        Task<Response<AddProductDto>> AddAsync(AddProductBindingModel bindingModel, int userId);
        Task<Response<ICollection<DetailsProductDto>>> GetAllAsync(int userId);
    }
}
