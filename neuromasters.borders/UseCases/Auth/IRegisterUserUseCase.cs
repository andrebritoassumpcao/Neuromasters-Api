using neuromasters.borders.Dtos;
using neuromasters.borders.Dtos.Auth;
using neuromasters.borders.Shared;

namespace neuromasters.borders.UseCases.Auth;

public interface IRegisterUserUseCase : IUseCase<RegisterRequest, UserDto>;
