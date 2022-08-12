using AutoMapper;
using ModsenTask.DataAccess.Abstractions.Repostories;
using ModsenTask.Queries.Event.Abstrations.Interfaces;
using ModsenTask.Queries.Event.Abstrations.Models;

namespace ModsenTask.Queries.Event.Queries.GetEventById;

public class GetEventByIdQueryHandler : IQueryHandler<GetEventByIdQuery>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; 

    public GetEventByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<IEnumerable<IModelDto>> Handle(GetEventByIdQuery query)
    {
        var events = (await  _unitOfWork
            .GetReadonlyRepository<DataAccess.Domain.Models.Event>()
            .GetWhereAsync(e =>e.Id == query.Id))
            .Select(e => _mapper.Map<EventDto>(e))
            .ToList();
        if (events is null)
            throw new Exception("Event does not exists");

        return events;
    }
}