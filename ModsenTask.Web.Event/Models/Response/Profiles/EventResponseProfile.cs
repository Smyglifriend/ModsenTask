using AutoMapper;
using ModsenTask.Queries.Event.Abstrations.Models;

namespace ModsenTask.Web.Event.Models.Response.Profiles;

public class EventResponseProfile : Profile
{
    public EventResponseProfile()
    {
        CreateMap<EventDto, EventResponseVm>();
    }
}