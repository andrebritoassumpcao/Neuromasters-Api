using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neuromasters.borders.Entities.Questionnaires;

[Table("neuro_form_question")]
public class FormQuestion
{
    [Key]
    [Column("question_id")]
    public int Id { get; set; }

    [Column("section_id")]
    public int SectionId { get; set; }

    [Column("question_text")]
    public string Text { get; set; } = string.Empty;

    [Column("neuro_cod_comp")]
    public int? CompetenceCode { get; set; }

    [Column("observations")]
    public string? Observations { get; set; }

    [Column("seq_order")]
    public int Order { get; set; }

    public FormSection? Section { get; set; }
    public Competence? Competence { get; set; }
    public ICollection<FormResponse> Responses { get; set; } = new List<FormResponse>();
}
