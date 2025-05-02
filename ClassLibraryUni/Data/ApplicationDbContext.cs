using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClassLibraryUni.Models; 


namespace ClassLibraryUni.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<TicketModel> Tickets { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
    }
}