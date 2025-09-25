using neuromasters.borders.Dtos.Questionnaires;
using neuromasters.borders.Entities.Questionnaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Repositories.Questionnaires
{
    public interface IQuestionnaireRepository
    {
        Task<Questionnaire> AddAsync(Questionnaire entity);

        Task<Questionnaire> UpdateAsync(Questionnaire entity);

        Task<IEnumerable<QuestionnaireDto>> GetAllAsync();

        Task<Questionnaire?> GetByIdWithDetailsAsync(int id);

        Task<QuestionnaireDto?> GetByIdAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<bool> DeleteAsync(int id);
    }
}
