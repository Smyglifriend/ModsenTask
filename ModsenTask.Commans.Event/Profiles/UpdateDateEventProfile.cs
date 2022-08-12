using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ModsenTask.Commans.Event.Commands.UpdateDateEvent;

namespace ModsenTask.Commans.Event.Profiles;

public class UpdateDateEventProfile : Profile
{
    public UpdateDateEventProfile()
    {
        CreateMap<UpdateEventCommand, DataAccess.Domain.Models.Event>();
    }
}