using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neuromasters.borders.Entities.Questionnaires;

[Table("neuro_gra_com")]
public class DegreeCompetence
{
    [Key]
    [Column("neuro_cod_gra")]
    public int Code { get; set; }

    [Column("neuro_cod_grp")]
    public string? GroupCode { get; set; }

    [Column("neuro_des_gra")]
    public string? Description { get; set; }
}
