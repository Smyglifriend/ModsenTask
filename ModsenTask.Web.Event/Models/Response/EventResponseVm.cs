namespace ModsenTask.Web.Event.Models.Response;

public class EventResponseVm
{
    public string Topic { get; set; }

    public string Description { get; set; }

    public string Sponsor { get; set; }

    public string Speaker { get; set; }

    public DateTime EventDate { get; set; }

    public string Location { get; set; }
}