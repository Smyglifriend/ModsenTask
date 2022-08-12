using ModsenTask.Commans.Event.Abstractions.Inerfaces;

namespace ModsenTask.Commans.Event.Commands.UpdateDateEvent;

public class UpdateEventCommand : ICommand
{
    public long Id { get; set; }

    public string Topic { get; set; }

    public string Description { get; set; }

    public string Sponsor { get; set; }

    public string Speaker { get; set; }

    public DateTime EventDate { get; set; }

    public string Location { get; set; }
}