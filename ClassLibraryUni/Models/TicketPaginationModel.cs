namespace ClassLibraryUni.Models;

public class TicketPaginationViewModel
{
    public List<TicketModel> Tickets { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalTickets { get; set; }
    
    public string? SelectedCategory { get; set; }

}
