namespace neuromasters.borders.Dtos;

public record UserDto(
string Id,
string FullName,
string Email,
string PhoneNumber,
bool EmailConfirmed,
bool PhoneNumberConfirmed,
DateTime CreatedAt
);
