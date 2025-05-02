using System.ComponentModel.DataAnnotations;

namespace ClassLibraryUni.Models;

public class CategoryModel
{
    public int Id { get; set; }
    
    
    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
    public string Name { get; set; }
    
    // public List<TicketModel> Tickets { get; set; }
}