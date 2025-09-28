using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Entities.Questionnaires
{
    [Table("neuro_default_answers")]
    public class DefaultAnswer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("form_id")]
        public int QuestionnaireId { get; set; }

        [Column("label")]
        public string Label { get; set; }
        
        [Column("color")]
        public string Color { get; set; }
    }
}
