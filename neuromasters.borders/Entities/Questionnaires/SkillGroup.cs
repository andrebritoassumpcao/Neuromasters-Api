using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neuromasters.borders.Entities.Questionnaires;

[Table("neuro_grp_hab")]
public class SkillGroup
{
    [Key]
    [Column("neuro_cod_grp")]
    public string Code { get; set; }

    [Column("neuro_des_grp")]
    public string Description { get; set; }

    [Column("neuro_des_grp")]
    public ICollection<Competence> Competences { get; set; }
}
