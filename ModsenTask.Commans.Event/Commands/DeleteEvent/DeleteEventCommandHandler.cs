using AutoMapper;
using ModsenTask.Commans.Event.Abstractions.Inerfaces;
using ModsenTask.DataAccess.Abstractions.Repostories;

namespace ModsenTask.Commans.Event.Commands.DeleteEvent;

public class DeleteEventCommandHandler : ICommandHandler<DeleteEventCommand>
{
    private IUnitOfWork _unitOfWork;


    public DeleteEventCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task Handle(DeleteEventCommand command)
    {
        var eventRepository = _unitOfWork.GetReadWriteRepository<DataAccess.Domain.Models.Event>();
        var eventEntity = await eventRepository.GetFirstOrDefaultAsync(e=>e.Id == command.Id);
        if (eventEntity is null)
            throw new Exception("This event does not exist");
        await eventRepository.RemoveAsync(eventEntity);
    }
}