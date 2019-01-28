using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FitCenter.Data.Data.Interfaces;
using FitCenter.Models.Model;
using FitCenter.Models.ModelDto;
using FitCenter.Models.ModelDto.TrainingDiary;
using FitCenter.Models.ModelDto.UserExerciseResults;
using FitCenter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitCenter.Services.Services
{
    public class TrainingDiaryService : ITrainingDiaryService
    {
        private readonly IRepository<TrainingDiary> _trainingDiaryRepository;
        private readonly IRepository<UserExerciseResults> _userExerciseResultsRepository;
        private readonly IRepository<Exercise> _exerciseRepository;
        private readonly IMapper _mapper;

        public TrainingDiaryService(
            IRepository<TrainingDiary> trainingDiaryRepository,
            IRepository<UserExerciseResults> userExerciseResultsRepository,
            IRepository<Exercise> exerciseRepository,
            IMapper mapper)
        {
            _trainingDiaryRepository = trainingDiaryRepository;
            _userExerciseResultsRepository = userExerciseResultsRepository;
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<Response<DetailsTrainingDiaryDto>> GetAsync(int trainingDiaryId)
        {
            var response = new Response<DetailsTrainingDiaryDto>();
            var trainingDiary = (await _trainingDiaryRepository.GetByAsync(x => x.Id == trainingDiaryId));

            if (trainingDiary == null)
            {
                response.AddError(Key.TrainingDiary, Error.NotExist);
                return response;
            }

            var trainingDiaryDto = _mapper.Map<DetailsTrainingDiaryDto>(trainingDiary);

            var trainingExercisesList = await _userExerciseResultsRepository.GetAllByAsync(x => x.TrainingDiaryId == trainingDiaryDto.Id).Result.ToListAsync();
            foreach (var element in trainingExercisesList)
            {
                var exercisesDetails = await _exerciseRepository.GetByAsync(x => x.Id == element.ExerciseId);
                element.Name = exercisesDetails.Name;
                trainingDiaryDto.UserExerciseResults.Add(_mapper.Map<DetailsUserExerciseResults>(element));
            }

            response.SuccessResult = trainingDiaryDto;
            return response;
        }

        public async Task<Response<ICollection<DetailsTrainingDiaryDto>>> GetAllAsync(int userId)
        {
            var response = new Response<ICollection<DetailsTrainingDiaryDto>>();
            var trainingDiaries= await _trainingDiaryRepository.GetAllByAsync(u => u.UserId == userId);

            if (trainingDiaries == null)
            {
                response.AddError(Key.TrainingDiary, Error.NotExist);
                return response;
            }

            var trainingDiariesDto = _mapper.Map<ICollection<DetailsTrainingDiaryDto>>(trainingDiaries);

            //Dla każdego dziennika treiningowego pobierz liste cwiczen i dodaj ja do trainingDiaresDto.UserExercisesResults
            foreach (var training in trainingDiariesDto)
            {
                var trainingExercisesList = await _userExerciseResultsRepository.GetAllByAsync(x => x.TrainingDiaryId == training.Id).Result.ToListAsync();
                foreach (var element in trainingExercisesList)
                {
                    var exercisesDetails = await _exerciseRepository.GetByAsync(x => x.Id == element.ExerciseId);
                    element.Name = exercisesDetails.Name;
                    training.UserExerciseResults.Add(_mapper.Map<DetailsUserExerciseResults>(element));
                }
            }
            response.SuccessResult = trainingDiariesDto;
            return response;
        }
    }
}
