using neuromasters.borders.Dtos.Questionnaires.Forms;
using neuromasters.borders.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Dtos.Questionnaires
{
    public record UpdateQuestionnaireRequest(
          int Id,
          string Name,
          string? Description,
          QuestionnaireStatusEnum Status,
          IEnumerable<UpdateFormSectionRequest> Sections
      );
}
