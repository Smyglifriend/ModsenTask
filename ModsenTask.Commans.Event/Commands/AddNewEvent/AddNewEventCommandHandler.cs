using AutoMapper;
using ModsenTask.Commans.Event.Abstractions.Inerfaces;
using ModsenTask.DataAccess.Abstractions.Repostories;

namespace ModsenTask.Commans.Event.Commands.AddNewEvent;

public class AddNewEventCommandHandler : ICommandHandler<AddNewEventCommand>
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;


    public AddNewEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task Handle(AddNewEventCommand command)
    {
        var eventEntity = _mapper.Map<DataAccess.Domain.Models.Event>(command);
        await _unitOfWork
            .GetReadWriteRepository<DataAccess.Domain.Models.Event>().SaveAsync(eventEntity);
    }
}