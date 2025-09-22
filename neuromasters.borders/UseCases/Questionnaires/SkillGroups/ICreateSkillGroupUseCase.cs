using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Dtos.Questionnaires;
using neuromasters.borders.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.UseCases.Questionnaires.SkillGroups;

public interface ICreateSkillGroupUseCase : IUseCase<CreateSkillGroupRequest, SkillGroupDto>;

