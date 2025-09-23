using neuromasters.borders.Dtos.Questionnaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Repositories.Questionnaires
{
    public interface IQuestionnaireRepository
    {
        Task<QuestionnaireDetailDto> AddAsync(CreateQuestionnaireRequest request);

        Task<IEnumerable<QuestionnaireDto>> GetAllAsync();

        Task<QuestionnaireDetailDto?> GetByIdWithDetailsAsync(int id);

        Task<QuestionnaireDto?> GetByIdAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<bool> DeleteAsync(int id);
    }
}
