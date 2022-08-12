using AutoMapper;
using ModsenTask.Services.Auth.Abstractions.Models;

namespace ModsenTask.Web.Models.Request.Profiles;

public class RegistrationRequestProfile : Profile
{
    public RegistrationRequestProfile()
    {
        CreateMap<RegistrationRequestVm, RegisterUserDto>()
            .ForMember(dto => dto.DateOfBirth, opt =>
                opt.MapFrom(vm => DateTime.Parse(vm.DateOfBirth)));
    }
}