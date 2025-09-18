using neuromasters.borders.Dtos.Roles;
using neuromasters.borders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Adapters.Interfaces
{
    public interface IRoleAdapter
    {
        RoleAssignmentDto ToRoleAssignmentDto(UserDto user, string roleName);
    }
}
