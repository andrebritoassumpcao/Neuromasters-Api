using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Dtos.Questionnaires
{
    public record QuestionnaireListDto(IEnumerable<QuestionnaireDto> Questionnaires, int TotalCount);

}
