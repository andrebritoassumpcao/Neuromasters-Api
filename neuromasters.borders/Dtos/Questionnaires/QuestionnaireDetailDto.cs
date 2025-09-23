using neuromasters.borders.Dtos.Questionnaires.Forms;

namespace neuromasters.borders.Dtos.Questionnaires;

public record QuestionnaireDetailDto(
     int Id,
     string Name,
     string? Description,
     string Status,
     DateTime CreatedAt,
     IEnumerable<FormSectionDto> Sections
 );
