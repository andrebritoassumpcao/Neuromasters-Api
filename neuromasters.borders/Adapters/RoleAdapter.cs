using neuromasters.borders.Adapters.Interfaces;
using neuromasters.borders.Dtos.Roles;
using neuromasters.borders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Adapters
{
    public class RoleAdapter : IRoleAdapter
    {
        public RoleAssignmentDto ToRoleAssignmentDto(UserDto user, string roleName)
        {
            return new RoleAssignmentDto(
                user.Id,
                user.Email,
                roleName,
                DateTime.UtcNow
            );
        }
    }
}
