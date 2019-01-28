using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FitCenter.Data.Data.Interfaces;
using FitCenter.Models.BindingModels.Exercise;
using FitCenter.Models.Model;
using FitCenter.Models.ModelDto;
using FitCenter.Models.ModelDto.Exercise;
using FitCenter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitCenter.Services.Services
{
    public class ExerciseService : IExerciseService
    {

        private readonly IRepository<Exercise> _exerciseRepository;
        private readonly IRepository<User> _userRepository;


        private readonly IMapper _mapper;

        public ExerciseService(IRepository<Exercise> exerciseRepository, IRepository<User> userRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;

            _mapper = mapper;
        }

        public async Task<Response<DetailsExerciseDto>> GetAsync(int exerciseId)
        {
            var response = new Response<DetailsExerciseDto>();

            var exercise = await _exerciseRepository.GetByAsync(x => x.Id == exerciseId);

            if (exercise == null)
            {
                response.AddError(Key.Exercise, Error.NotExist);
                return response;
            }

            var exerciseDto = _mapper.Map<DetailsExerciseDto>(exercise);

            response.SuccessResult = exerciseDto;

            return response;
        }

        public async Task<Response<AddExerciseDto>> AddAsync(AddExerciseBindingModel bindingModel, int userId)
        {
            var response = new Response<AddExerciseDto>();

            var user = await _userRepository.GetByAsync(x => x.Id == userId);

            if (user == null)
            {
                response.AddError(Key.User, Error.NotExist);
                return response;
            }

            var exercise = _mapper.Map<Exercise>(bindingModel);

            exercise.User = user;
            exercise.UserId = userId;

            var exerciseAddSuccess = await _exerciseRepository.AddAsync(exercise);

            if (!exerciseAddSuccess)
            {
                response.AddError(Key.Exercise, Error.AddError);
                return response;
            }

            var exerciseDto = _mapper.Map<AddExerciseDto>(exercise);

            response.SuccessResult = exerciseDto;

            return response;
        }

        public async Task<Response<ICollection<DetailsExerciseDto>>> GetAllAsync(int userId)
        {
            var response = new Response<ICollection<DetailsExerciseDto>> ();
            var exercises = await _exerciseRepository.GetAllByAsync(u => u.UserId == userId).Result.ToListAsync();
            if (exercises == null || exercises.Count == 0)
            {
                response.AddError(Key.Exercise,Error.NotExist);
                return response;
            }

            var exercisesDto = _mapper.Map<ICollection<DetailsExerciseDto>>(exercises);
            response.SuccessResult = exercisesDto;
            return response;
        }

        public async Task<Response<DeleteExerciseDto>> DeleteAsync(int exerciseId)
        {
            var response = new Response<DeleteExerciseDto>();
            var exercise = await _exerciseRepository.GetByAsync(x => x.Id == exerciseId);
            if (exercise == null)
            {
                response.AddError(Key.Exercise, Error.NotExist);
                return response;
            }

            bool deleteSucceed = await _exerciseRepository.RemoveAsync(exercise);
            if (!deleteSucceed)
            {
                response.AddError(Key.Exercise, Error.NotExist);
                return response;
            }

            var exerciseDto = _mapper.Map<DeleteExerciseDto>(exercise);
            response.SuccessResult = exerciseDto;
            return response;
        }

        public async Task<Response<object>> UpdateAsync(UpdateExerciseBindingModel bindingModel)
        {
            var response = await ValidateUpdateViewModel(bindingModel);
            if (response.ErrorOccurred)
            {
                return response;
            }

            var exercise = await _exerciseRepository.GetByAsync(x => x.Id == bindingModel.Id);

            _exerciseRepository.Detach(exercise);

            var updatedExercise = _mapper.Map<Exercise>(bindingModel);
            updatedExercise.UserId = exercise.UserId;
            updatedExercise.User = exercise.User;
            updatedExercise.Id = exercise.Id;
            bool updateSucceed = await _exerciseRepository.UpdateAsync(updatedExercise);
            if (!updateSucceed)
            {
                response.AddError(Key.Exercise, Error.UpdateError);
            }
            response.SuccessResult = bindingModel;
            return response;
        }
        private async Task<Response<object>> ValidateUpdateViewModel(UpdateExerciseBindingModel bindingModel)
        {
            var response = new Response<object>();
            bool exerciseExists = await _exerciseRepository.ExistAsync(x => x.Id == bindingModel.Id);
            if (!exerciseExists)
            {
                response.AddError(Key.Exercise, Error.NotExist);
            }
            return response;
        }
    }
}
