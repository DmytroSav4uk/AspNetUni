using AspNetUni.Models;
using AspNetUni.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace AspNetUni.Services
{
    public interface IDatabaseService
    {
        string GetDatabaseVersion();
        Task PopulateTicketsAsync();
        Task<List<TicketModel>> GetAllTicketsAsync();
        
        
        Task<int> GetTicketCountAsync();  
        Task<List<TicketModel>> GetTicketsAsync(int page, int pageSize); 
    }

    public class DatabaseService : IDatabaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DatabaseService> _logger;

        public DatabaseService(ApplicationDbContext context, ILogger<DatabaseService> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation("DatabaseService initialized.");
        }

        public string GetDatabaseVersion()
        {
            try
            {
                _logger.LogInformation("Attempting to connect to the database...");
                
                var version = _context.Database.GetDbConnection().ServerVersion;
                _logger.LogInformation($"MySQL Version: {version}");
                return $"MySQL Version: {version}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Database connection failed: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }

        public async Task PopulateTicketsAsync()
        {
            try
            {
                _logger.LogInformation("Populating Tickets data...");

             
                var tickets = new List<TicketModel>
                {
                    new TicketModel { Performer = "Ariana Grande", Event = "Concert in NYC", EventDate = DateTime.Now.AddMonths(1), Price = 99.99M, Location = "Madison Square Garden" },
                    new TicketModel { Performer = "Ed Sheeran", Event = "Summer Tour", EventDate = DateTime.Now.AddMonths(2), Price = 75.50M, Location = "Central Park" },
                    new TicketModel { Performer = "BTS", Event = "World Tour", EventDate = DateTime.Now.AddMonths(3), Price = 120.00M, Location = "MetLife Stadium" },
                    new TicketModel { Performer = "The Weeknd", Event = "After Hours Tour", EventDate = DateTime.Now.AddMonths(4), Price = 85.00M, Location = "Barclays Center" },
                };

                _context.Tickets.AddRange(tickets);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Tickets data populated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error populating tickets: {ex.Message}");
            }
        }
        public async Task<List<TicketModel>> GetAllTicketsAsync()
        {
            try
            {
                return await _context.Tickets.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving tickets: {ex.Message}");
                return new List<TicketModel>(); 
            }
        }
        
        
        
        public async Task<int> GetTicketCountAsync()
        {
            return await _context.Tickets.CountAsync();
        }

        public async Task<List<TicketModel>> GetTicketsAsync(int page, int pageSize)
        {
            return await _context.Tickets
                .Skip((page - 1) * pageSize)  
                .Take(pageSize) 
                .ToListAsync();  
        }

        
        
        
        
        
    }
}
