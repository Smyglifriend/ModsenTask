using AutoMapper;
using ModsenTask.Commans.Event.Commands.AddNewEvent;

namespace ModsenTask.Commans.Event.Profiles;

public class AddEventCommandProfile : Profile
{
    public AddEventCommandProfile()
    {
        CreateMap<AddNewEventCommand, DataAccess.Domain.Models.Event>();
    }
}