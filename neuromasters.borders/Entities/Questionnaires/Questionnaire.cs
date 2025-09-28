using neuromasters.borders.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neuromasters.borders.Entities.Questionnaires;

[Table("neuro_form")]
public class Questionnaire
{
    [Key]
    [Column("form_id")]
    public int Id { get; set; }

    [Column("form_name")]
    public string Name { get; set; } = string.Empty;

    [Column("form_desc")]
    public string? Description { get; set; }

    [Column("form_status")]
    public QuestionnaireStatusEnum Status { get; set; } = QuestionnaireStatusEnum.Draft;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    public ICollection<FormSection> Sections { get; set; } = new List<FormSection>();
    public ICollection<FormResponse> Responses { get; set; } = new List<FormResponse>();
    public ICollection<DefaultAnswer> DefaultAnswers { get; set; } = new List<DefaultAnswer>();
}