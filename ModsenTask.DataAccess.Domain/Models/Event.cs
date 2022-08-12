using ModsenTask.DataAccess.Domain.Abstraction.Interfaces;

namespace ModsenTask.DataAccess.Domain.Models;

public class Event : IEntity
{
    public long Id { get; set; }

    public string Topic { get; set; }

    public string Description { get; set; }

    public string Sponsor { get; set; }

    public string Speaker { get; set; }

    public DateTime EventDate { get; set; }

    public string Location { get; set; }
    
}