using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModsenTask.Commans.Event.Abstractions.Inerfaces;
using ModsenTask.Commans.Event.Commands.AddNewEvent;
using ModsenTask.Commans.Event.Commands.DeleteEvent;
using ModsenTask.Commans.Event.Commands.UpdateDateEvent;
using ModsenTask.DataAccess;
using ModsenTask.Queries.Event.Abstrations.Interfaces;
using ModsenTask.Queries.Event.Queries;
using ModsenTask.Queries.Event.Queries.GetAllEvents;
using ModsenTask.Queries.Event.Queries.GetEventById;
using ModsenTask.Web.Core.Controllers;
using ModsenTask.Web.Event.Models.Request;
using ModsenTask.Web.Event.Models.Response;

namespace ModsenTask.Web.Event.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class EventController : BaseWebConroller
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EventController(
        IMapper mapper,
        IQueryDispatcher queryDispatcher,
        IHttpContextAccessor httpContextAccessor,
        ICommandDispatcher commandDispatcher)
        : base(mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = new QueryDispatcher(_httpContextAccessor.HttpContext.RequestServices);
    }


    [HttpGet]
    public async Task<ActionResult<List<EventResponseVm>>> GetAllEvents()
    {
        var response = (await _queryDispatcher
            .Send(new GetAllEventsQuery()))
            .Select(e=>_mapper.Map<EventResponseVm>(e))
            .ToList();

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<EventResponseVm>> GetEventById(long id)
    {
        var response = (await _queryDispatcher
            .Send(new GetEventByIdQuery { Id = id }))
            .FirstOrDefault();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddNewEvent([FromBody] EventRequestVm model)
    {
        await _commandDispatcher.Send(_mapper.Map<AddNewEventCommand>(model));

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteEvent(long id)
    {
        await _commandDispatcher.Send(new DeleteEventCommand{ Id = id});

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventRequestVm model)
    {
        await _commandDispatcher.Send(_mapper.Map<UpdateEventCommand>(model));

        return Ok();
    }
}