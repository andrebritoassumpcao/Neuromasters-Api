using neuromasters.borders.Dtos.Questionnaires.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Dtos.Questionnaires
{
    public record CreateQuestionnaireRequest(
           string Name,
           string? Description,
           IEnumerable<CreateFormSectionRequest> Sections
       );
}
