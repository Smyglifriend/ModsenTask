using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;

namespace ModsenTask.Web.Models.Request.Profiles;

public class LoginRequestProfile : Profile
{
    public LoginRequestProfile()
    {
        CreateMap<LoginRequestVm, LoginDto>();
    }
}