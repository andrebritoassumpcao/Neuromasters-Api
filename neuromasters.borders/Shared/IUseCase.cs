namespace neuromasters.borders.Shared;
public interface IUseCase<TRequest, TResponse>
{
    Task<UseCaseResponse<TResponse>> Execute(TRequest request);
}
