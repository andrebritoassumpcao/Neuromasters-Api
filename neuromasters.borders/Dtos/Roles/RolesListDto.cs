using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Dtos.Roles;

public record RolesListDto(IEnumerable<RoleDto> Roles, int TotalCount);
