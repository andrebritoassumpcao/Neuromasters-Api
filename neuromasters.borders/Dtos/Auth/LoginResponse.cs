namespace neuromasters.borders.Dtos.Auth;

public record LoginResponse(string Token, DateTime Expiration, UserDto User);
