using neuromasters.borders.Adapters.Interfaces;
using neuromasters.borders.Dtos.Questionnaires.DefaultAswers;
using neuromasters.borders.Entities.Questionnaires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Adapters
{
    public class DefaultAnswerAdapter : IDefaultAnswerAdapter
    {
        public DefaultAnswer CreateRequestToEntity(CreateDefaultAnswerRequest request)
        {
            return new DefaultAnswer
            {
                QuestionnaireId = request.QuestionnaireId,
                Label = request.Label,
                Color = request.Color
            };
        }

        public DefaultAnswerDto EntityToDto(DefaultAnswer entity)
        {
            return new DefaultAnswerDto(
                Id: entity.Id,
                QuestionnaireId: entity.QuestionnaireId,
                Label: entity.Label,
                Color: entity.Color
            );
        }
    }
}
