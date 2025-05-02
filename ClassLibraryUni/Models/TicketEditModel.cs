namespace ClassLibraryUni.Models;


public class TicketEditModel
{
    public int Id { get; set; }
    public string Performer { get; set; }
    public string Event { get; set; }
    public DateTime EventDate { get; set; }
    public string Location { get; set; }
    public decimal Price { get; set; }


    public int? CategoryId { get; set; }
    public List<string> Categories { get; set; }
}
