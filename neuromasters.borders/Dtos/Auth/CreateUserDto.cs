namespace neuromasters.borders.Dtos.Auth;

public record CreateUserDto(
string FullName,
string Email,
string Password,
string PhoneNumber,
bool EmailConfirmed,
bool PhoneNumberConfirmed
);
