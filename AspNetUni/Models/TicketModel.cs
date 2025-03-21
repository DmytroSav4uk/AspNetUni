namespace AspNetUni.Models;

public class TicketModel
{
    public int Id { get; set; }
    public string Performer { get; set; }
    public string Event { get; set; }
    public DateTime EventDate { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; }
}