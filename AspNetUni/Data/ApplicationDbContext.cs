using Microsoft.EntityFrameworkCore;
using AspNetUni.Models;

namespace AspNetUni.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TicketModel> Tickets { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        
        
    }
}