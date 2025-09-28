using neuromasters.borders.Dtos.Questionnaires.DefaultAswers;
using neuromasters.borders.Shared;

namespace neuromasters.borders.UseCases.Questionnaires.DefaultAswers;

public interface IGetDefaultAnswerUseCase : IUseCase<GetDefaultAnswerRequest, DefaultAnswerDto?>;
