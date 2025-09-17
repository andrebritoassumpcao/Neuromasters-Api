namespace neuromasters.borders.Shared;

public enum UseCaseResponseKind
{
    Success,
    Ok,
    DataPersisted,
    DataAccepted,
    InternalServerError,
    RequestValidationError,
    ForeignKeyViolationError,
    UniqueViolationError,
    NotFound,
    Unauthorized,
    Forbidden,
    BadRequest,
    BadGateway,
    Unavailable,
    UnprocssableEntity
}
