using neuromasters.borders.Dtos.Questionnaires.Forms;
using neuromasters.borders.Entities.Enums;

namespace neuromasters.borders.Dtos.Questionnaires;

public record CreateQuestionnaireRequest(
       string Name,
       string? Description,
       QuestionnaireStatusEnum Status,
       IEnumerable<CreateFormSectionRequest> Sections
   );
