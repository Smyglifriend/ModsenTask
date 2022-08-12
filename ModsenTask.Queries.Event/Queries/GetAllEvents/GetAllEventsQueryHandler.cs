using AutoMapper;
using ModsenTask.DataAccess.Abstractions.Repostories;
using ModsenTask.Queries.Event.Abstrations.Interfaces;
using ModsenTask.Queries.Event.Abstrations.Models;

namespace ModsenTask.Queries.Event.Queries.GetAllEvents;

public class GetAllEventsQueryHandler : IQueryHandler<GetAllEventsQuery>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public GetAllEventsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<IEnumerable<IModelDto>> Handle(GetAllEventsQuery query)
    {
        var events = (await _unitOfWork
            .GetReadonlyRepository<DataAccess.Domain.Models.Event>()
            .GetAllAsync())
            .Select(e => _mapper.Map<EventDto>(e));
        if (events is null)
            throw new Exception("Event does not exists");

        return events;
    }
}