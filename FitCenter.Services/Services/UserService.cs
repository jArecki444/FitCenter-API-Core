using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitCenter.Data.Data.Interfaces;
using FitCenter.Models.BindingModels.User;
using FitCenter.Models.Model;
using FitCenter.Models.ModelDto;
using FitCenter.Models.ModelDto.User;
using FitCenter.Services.Interfaces;
using Paneleo.Services;

namespace FitCenter.Services.Services
{
    public class UserService : IUserService
    {

        private readonly IRepository<User> _userRepository;


        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<DetailsUserDto>> GetAsync(int userId)
        {
            var response = new Response<DetailsUserDto>();

            var user = await _userRepository.GetByAsync(x => x.Id == userId);

            if (user == null)
            {
                response.AddError(Key.User, Error.NotExist);
                return response;
            }

            var userDto = _mapper.Map<DetailsUserDto>(user);

            response.SuccessResult = userDto;
            return response;
        }

        public async Task<Response<DeleteUserDto>> DeleteAsync(int userId)
        {
            var response = new Response<DeleteUserDto>();
            var user = await _userRepository.GetByAsync(x => x.Id == userId);
            if (user == null)
            {
                response.AddError(Key.User, Error.NotExist);
                return response;
            }

            bool deleteSucceed = await _userRepository.RemoveAsync(user);
            if (!deleteSucceed)
            {
                response.AddError(Key.User, Error.NotExist);
                return response;
            }

            var userDto = _mapper.Map<DeleteUserDto>(user);
            response.SuccessResult = userDto;
            return response;
        }

        public async Task<Response<object>> UpdateAsync(UpdateUserBindingModel bindingModel, int userId)
        {
            var response = await ValidateUpdateViewModel(userId);
            if (response.ErrorOccurred)
            {
                return response;
            }

            var user = await _userRepository.GetByAsync(x => x.Id == userId);

            _userRepository.Detach(user);

            var updatedUser = _mapper.Map<User>(bindingModel);
            updatedUser.Id = user.Id;
            bool updateSucceed = await _userRepository.UpdateAsync(updatedUser);
            if (!updateSucceed)
            {
                response.AddError(Key.User, Error.UpdateError);
            }

            response.SuccessResult = bindingModel;
            return response;
        }

        private async Task<Response<object>> ValidateUpdateViewModel(int userId)
        {
            var response = new Response<object>();
            bool userExists = await _userRepository.ExistAsync(x => x.Id == userId);
            if (!userExists)
            {
                response.AddError(Key.User, Error.NotExist);
            }

            return response;
        }
    }
}
