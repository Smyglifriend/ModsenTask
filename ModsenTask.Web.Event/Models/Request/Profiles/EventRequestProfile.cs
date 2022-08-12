using AutoMapper;
using ModsenTask.Commans.Event.Commands.AddNewEvent;
using ModsenTask.Commans.Event.Commands.DeleteEvent;
using ModsenTask.Commans.Event.Commands.UpdateDateEvent;

namespace ModsenTask.Web.Event.Models.Request.Profiles;

public class EventRequestProfile : Profile
{
    public EventRequestProfile()
    {
        CreateMap<EventRequestVm, AddNewEventCommand>();
        CreateMap<EventRequestVm, UpdateEventCommand>();
    }
}