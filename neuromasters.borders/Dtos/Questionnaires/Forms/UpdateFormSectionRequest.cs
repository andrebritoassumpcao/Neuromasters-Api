using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Dtos.Questionnaires.Forms;

public record UpdateFormSectionRequest(
              int Id,
              string Name,
              int Order,
              IEnumerable<UpdateFormQuestionRequest>? Questions
              );

