using Microsoft.EntityFrameworkCore;
using AspNetUni.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace AspNetUni.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TicketModel> Tickets { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        
        
    }
}