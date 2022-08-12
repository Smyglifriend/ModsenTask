using AutoMapper;
using ModsenTask.DataAccess.Domain.Models;

namespace ModsenTask.Services.Auth.Abstractions.Models.Profiles;

public class RegisterDtoProfile : Profile
{
    public RegisterDtoProfile()
    {
        CreateMap<RegisterUserDto, User>();
    }
}