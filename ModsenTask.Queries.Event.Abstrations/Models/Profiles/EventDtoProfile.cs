using AutoMapper;

namespace ModsenTask.Queries.Event.Abstrations.Models.Profiles;

public class EventDtoProfile : Profile
{
    public EventDtoProfile()
    {
        CreateMap<DataAccess.Domain.Models.Event, EventDto>();
    }
}