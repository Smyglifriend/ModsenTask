using ModsenTask.Commans.Event.Abstractions.Inerfaces;

namespace ModsenTask.Commans.Event.Commands.AddNewEvent;

public class AddNewEventCommand : ICommand
{
    public string Topic { get; set; }

    public string Description { get; set; }

    public string Sponsor { get; set; }

    public string Speaker { get; set; }

    public DateTime EventDate { get; set; }

    public string Location { get; set; }
}