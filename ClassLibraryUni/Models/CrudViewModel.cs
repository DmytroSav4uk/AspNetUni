namespace ClassLibraryUni.Models
{
    public class CrudViewModel
    {
        public List<TicketModel>? Tickets { get; set; }
        public List<CategoryModel>? Categories { get; set; }
        public string SelectedView { get; set; } = "tickets";
    }
}