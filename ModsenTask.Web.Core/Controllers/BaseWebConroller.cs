using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ModsenTask.Web.Core.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BaseWebConroller : ControllerBase
{
    protected readonly IMapper _mapper;


    public BaseWebConroller(IMapper mapper)
    {
        _mapper = mapper;
    }
}