using FluentValidation;
using Microsoft.Extensions.Logging;

namespace neuromasters.borders.Shared;

public abstract class UseCase<TRequest, TResponse>(
ILogger<UseCaseBase<TResponse>> logger,
IValidator<TRequest> _validator) : UseCaseBase<TResponse>(logger, new ErrorMessage("0", "")), IUseCase<TRequest, TResponse>
{

    public async Task<UseCaseResponse<TResponse>> Execute(TRequest request)
    {
        return await SafeExecute(async () =>
        {
            if (_validator is not null)
                await _validator.ValidateAndThrowAsync(request);

            return await OnExecute(request);
        });
    }

    protected abstract Task<UseCaseResponse<TResponse>> OnExecute(TRequest request);
}
