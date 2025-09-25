using neuromasters.borders.Dtos.Questionnaires.Forms;
using neuromasters.borders.Entities.Enums;

namespace neuromasters.borders.Dtos.Questionnaires;

public record QuestionnaireDetailDto(
     int Id,
     string Name,
     string? Description,
     QuestionnaireStatusEnum Status,
     DateTime CreatedAt,
     IEnumerable<FormSectionDto> Sections
 );
