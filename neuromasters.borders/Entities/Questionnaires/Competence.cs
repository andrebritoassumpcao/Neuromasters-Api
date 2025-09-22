using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neuromasters.borders.Entities.Questionnaires;

[Table("neuro_com_hab")]
public class Competence
{
    [Key]
    [Column("neuro_cod_comp")]
    public int Code { get; set; }

    [Column("neuro_cod_grp")]
    public string? GroupCode { get; set; }

    [Column("neuro_des_comp")]
    public string? Description { get; set; }

    [Column("neuro_seq_comp")]
    public int? Sequence { get; set; }

}
