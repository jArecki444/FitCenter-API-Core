using System.Collections.Generic;
using System.Threading.Tasks;
using FitCenter.Models.ModelDto;
using FitCenter.Models.ModelDto.TrainingDiary;

namespace FitCenter.Services.Interfaces
{
    public interface ITrainingDiaryService
    {
        Task<Response<DetailsTrainingDiaryDto>> GetAsync(int trainingDiaryId);
        Task<Response<ICollection<DetailsTrainingDiaryDto>>> GetAllAsync(int userId);
    }
}
