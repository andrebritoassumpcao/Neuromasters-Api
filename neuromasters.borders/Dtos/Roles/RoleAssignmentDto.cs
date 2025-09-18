namespace neuromasters.borders.Dtos.Roles;

public record RoleAssignmentDto(
    string UserId,
    string UserEmail,
    string RoleName,
    DateTime AssignedAt
    );
