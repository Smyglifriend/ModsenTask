using AestheticLife.Auth.Services.Abstractions.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModsenTask.Services.Auth.Abstractions.Interfaces;
using ModsenTask.Services.Auth.Abstractions.Models;
using ModsenTask.Web.Core.Controllers;
using ModsenTask.Web.Models.Request;
using ModsenTask.Web.Models.Response;

namespace ModsenTask.Web.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class AccountController : BaseWebConroller
{
    private readonly IAuthService _authService;


    public AccountController(
        IMapper mapper,
        IAuthService authService)
        : base(mapper)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Registration([FromBody] RegistrationRequestVm model)
    {
        await _authService.RegisterAsync(_mapper.Map<RegisterUserDto>(model));

        return Ok();
    }
    
    [HttpPost]
    public async Task<ActionResult<LoginResponseVm>> Login([FromBody] LoginRequestVm model)
        => _mapper.Map<LoginResponseVm>(await _authService.LoginAsync(_mapper.Map<LoginDto>(model)));

}