using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neuromasters.borders.Entities.Questionnaires;

[Table("neuro_form_section")]
public class FormSection
{
    [Key]
    [Column("section_id")]
    public int Id { get; set; }

    [Column("form_id")]
    public int FormId { get; set; }

    [Column("sec_name")]
    public string Name { get; set; } = string.Empty;

    [Column("neuro_cod_grp")]
    public string? SkillGroupCode { get; set; }

    [Column("seq_order")]
    public int Order { get; set; }

    public Questionnaire? Form { get; set; }
    public SkillGroup? SkillGroup { get; set; }
    public ICollection<FormQuestion> Questions { get; set; } = new List<FormQuestion>();
}
