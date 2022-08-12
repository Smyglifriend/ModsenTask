using ModsenTask.Queries.Event.Abstrations.Interfaces;

namespace ModsenTask.Queries.Event.Queries.GetEventById;

public class GetEventByIdQuery : IQuery
{
    public long Id { get; set; }
}