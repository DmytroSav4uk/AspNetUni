using ClassLibraryUni.Models;
using ClassLibraryUni.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClassLibraryUni.Services
{
    public interface IDatabaseService
    {
        string GetDatabaseVersion();
        Task PopulateTicketsAsync();
        Task<List<TicketModel>> GetAllTicketsAsync();
        Task<List<CategoryModel>> GetAllCategoriesAsync();

        Task<int> GetTicketCountAsync();  
        Task<List<TicketModel>> GetTicketsAsync(int page, int pageSize); 

        Task AddTicketAsync(TicketModel ticket);  
        Task UpdateTicketAsync(TicketModel ticket);  
        Task DeleteTicketAsync(int ticketId);  
        
        
        Task AddCategoryAsync(CategoryModel category);
        Task UpdateCategoryAsync(CategoryModel category);
        Task DeleteCategoryAsync(int id);
        
        
        
        Task<TicketModel> GetTicketByIdAsync(int id);
        Task<List<string>> GetAllCategoryNamesAsync();
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
                return await _context.Tickets
                    .Include(t => t.Category)
                    .ToListAsync();
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
        
        public async Task<List<CategoryModel>> GetAllCategoriesAsync()
        {
            try
            {
                return await _context.Categories
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving categories: {ex.Message}");
                return new List<CategoryModel>();
            }
        }


        
        public async Task<List<string>> GetAllCategoryNamesAsync()
        {
            return await _context.Categories
                .Select(c => c.Name)
                .OrderBy(name => name)
                .ToListAsync();
        }

        
        
        public async Task<List<TicketModel>> GetTicketsAsync(int page, int pageSize)
        {
            return await _context.Tickets
                .Skip((page - 1) * pageSize)  
                .Take(pageSize) 
                .ToListAsync();  
        }
        
        
        public async Task AddTicketAsync(TicketModel ticket)
        {
            try
            {
                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Ticket added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding ticket: {ex.Message}");
            }
        }

        
        
        public async Task UpdateTicketAsync(TicketModel ticket)
        {
            try
            {
                var existingTicket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticket.Id);
                if (existingTicket != null)
                {
                    existingTicket.Performer = ticket.Performer;
                    existingTicket.Event = ticket.Event;
                    existingTicket.EventDate = ticket.EventDate;
                    existingTicket.Location = ticket.Location;
                    existingTicket.Price = ticket.Price;
                    existingTicket.CategoryId = ticket.CategoryId;

                    _context.Tickets.Update(existingTicket);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Ticket updated successfully.");
                }
                else
                {
                    _logger.LogError("Ticket not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating ticket: {ex.Message}");
            }
        }

        
        
        public async Task DeleteTicketAsync(int ticketId)
        {
            try
            {
                var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
                if (ticket != null)
                {
                    _context.Tickets.Remove(ticket);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Ticket deleted successfully.");
                }
                else
                {
                    _logger.LogError("Ticket not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting ticket: {ex.Message}");
            }
        }

        
        public async Task<TicketModel> GetTicketByIdAsync(int id)
        {
            try
            {
                var ticket = await _context.Tickets
                    .Include(t => t.Category)  
                    .FirstOrDefaultAsync(t => t.Id == id);

                return ticket;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving ticket by ID {id}: {ex.Message}");
                return null; 
            }
        }
        
        
        public async Task AddCategoryAsync(CategoryModel category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(CategoryModel category)
        {
            var existing = await _context.Categories.FindAsync(category.Id);
            if (existing != null)
            {
                existing.Name = category.Name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        
        
        
        
        
        
    }
}
