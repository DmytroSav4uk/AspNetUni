using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetUni.Models;

public class TicketModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Performer is required.")]
    public string Performer { get; set; }
    
    
    [Required(ErrorMessage = "Event is required.")]
    public string Event { get; set; }
   
    [Required(ErrorMessage = "Event Date is required.")]
    [DataType(DataType.Date)]
    public DateTime EventDate { get; set; }
    
    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, 10000.0, ErrorMessage = "Price must be between 0.01 and 10000.")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Location is required.")]
    public string Location { get; set; }
    
    
    [Required(ErrorMessage = "Category is required.")]
    [Column("CategoryId")]  
    public int? CategoryId { get; set; }
    
    public CategoryModel? Category { get; set; }
}