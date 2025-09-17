namespace neuromasters.borders.Shared;
public interface IUseCaseResponseOnly<TResponse>
{
    Task<UseCaseResponse<TResponse>> Execute();
}
