using neuromasters.borders.Dtos.Roles;
using neuromasters.borders.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.UseCases.Roles;

public interface IGetUserRoleUseCase : IUseCase<GetUserRolesRequest, UserRoleDto>;

