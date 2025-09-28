using neuromasters.borders.Dtos.Questionnaires.DefaultAswers;
using neuromasters.borders.Entities.Questionnaires;

namespace neuromasters.borders.Adapters.Interfaces
{
    public interface IDefaultAnswerAdapter
    {
        DefaultAnswer CreateRequestToEntity(CreateDefaultAnswerRequest request);

        DefaultAnswerDto EntityToDto(DefaultAnswer entity);
    }
}
