using neuromasters.borders.Dtos.Questionnaires.DefaultAswers;
using neuromasters.borders.Entities.Questionnaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Repositories.Questionnaires;

public interface IDefaultAnswerRepository
{
    Task<DefaultAnswer> AddAsync(DefaultAnswer entity);
    Task<IEnumerable<DefaultAnswerDto>> GetByQuestionnaireIdAsync(int questionnaireId);
    Task<DefaultAnswerDto?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> DeleteAsync(int id);
}
