using ModsenTask.Commans.Event.Abstractions.Inerfaces;

namespace ModsenTask.Commans.Event.Commands.DeleteEvent;

public class DeleteEventCommand : ICommand
{
    public long Id { get; set; }
}