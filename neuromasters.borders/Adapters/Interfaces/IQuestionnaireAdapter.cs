using neuromasters.borders.Dtos.Questionnaires;
using neuromasters.borders.Entities.Questionnaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Adapters.Interfaces;
public interface IQuestionnaireAdapter
{
    QuestionnaireDetailDto EntityToDetailDto(Questionnaire entity);
    Questionnaire UpdateRequestToEntity(UpdateQuestionnaireRequest request, Questionnaire existingEntity);
    Questionnaire CreateRequestToEntity(CreateQuestionnaireRequest request);
}
