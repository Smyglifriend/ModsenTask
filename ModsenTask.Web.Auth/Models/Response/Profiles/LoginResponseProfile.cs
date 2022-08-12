using AutoMapper;
using ModsenTask.Services.Auth.Abstractions.Models;

namespace ModsenTask.Web.Models.Response.Profiles;

public class LoginResponseProfile : Profile
{
    public LoginResponseProfile()
    {
        CreateMap<TokenDto, LoginResponseVm>();
    }
}