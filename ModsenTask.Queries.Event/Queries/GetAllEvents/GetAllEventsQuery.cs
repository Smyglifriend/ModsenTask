using ModsenTask.Queries.Event.Abstrations.Interfaces;

namespace ModsenTask.Queries.Event.Queries.GetAllEvents;

public class GetAllEventsQuery : IQuery
{
    public long Id { get; set; }

    public string Topic { get; set; }

    public string Description { get; set; }

    public string Sponsor { get; set; }

    public string Speaker { get; set; }

    public DateTime EventDate { get; set; }

    public string Location { get; set; }
}