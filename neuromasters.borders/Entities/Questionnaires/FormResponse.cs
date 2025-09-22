using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neuromasters.borders.Entities.Questionnaires;

[Table("neuro_form_response")]
public class FormResponse
{
    [Key]
    [Column("response_id")]
    public int Id { get; set; }

    [Column("form_id")]
    public int FormId { get; set; }

    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [Column("question_id")]
    public int QuestionId { get; set; }

    [Column("degree_code")]
    public int DegreeCode { get; set; }

    [Column("observacao")]
    public string? Observation { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Questionnaire? Form { get; set; }
    public FormQuestion? Question { get; set; }
    public DegreeCompetence? Degree { get; set; }
}
