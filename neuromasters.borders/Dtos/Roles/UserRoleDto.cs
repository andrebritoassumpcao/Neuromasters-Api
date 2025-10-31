namespace neuromasters.borders.Dtos.Roles;

public record UserRoleDto(string UserId, string UserEmail, string UserName,string RoleID, string RoleName);
public record UserRolesDto(string UserId, string UserEmail, string UserName, IReadOnlyList<RoleDto> Roles);
