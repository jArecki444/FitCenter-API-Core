using FitCenter.Models.ModelDto;
using System.Threading.Tasks;
using FitCenter.Models.BindingModels.Product;
using FitCenter.Models.BindingModels.User;
using FitCenter.Models.ModelDto.User;

namespace FitCenter.Services.Interfaces
{
    public interface IUserService
    {
        Task<Response<DetailsUserDto>> GetAsync(int userId);
        Task<Response<DeleteUserDto>> DeleteAsync(int userId);
        Task<Response<object>> UpdateAsync(UpdateUserBindingModel bindingModel, int userId);

    }
}
