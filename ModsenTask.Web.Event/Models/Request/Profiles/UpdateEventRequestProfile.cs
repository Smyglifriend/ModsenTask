using AutoMapper;
using ModsenTask.Commans.Event.Commands.UpdateDateEvent;

namespace ModsenTask.Web.Event.Models.Request.Profiles;

public class UpdateEventRequestProfile : Profile
{
    public UpdateEventRequestProfile()
    {
        CreateMap<UpdateEventRequestVm, UpdateEventCommand>();
    }
}