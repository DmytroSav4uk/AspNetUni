using System.Diagnostics;
using AspNetUni.Extensions;
using Microsoft.AspNetCore.Mvc;
using AspNetUni.Models;
using AspNetUni.Services;

namespace AspNetUni.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDatabaseService _databaseService;
    
    public HomeController(ILogger<HomeController> logger, IDatabaseService databaseService)
    {
        _logger = logger;
        _databaseService = databaseService;
    }

    public async Task<IActionResult> Index(string? category, int page = 1, int pageSize = 5)
    {
        var allTickets = await _databaseService.GetAllTicketsAsync();

        if (!string.IsNullOrEmpty(category))
        {
            allTickets = allTickets
                .Where(t => t.Category != null && t.Category.Name == category)
                .ToList();
        }

        var totalTickets = allTickets.Count;

        var paginatedTickets = allTickets
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var totalPages = (int)Math.Ceiling(totalTickets / (double)pageSize);

        var viewModel = new TicketPaginationViewModel
        {
            Tickets = paginatedTickets,
            CurrentPage = page,
            TotalPages = totalPages,
            PageSize = pageSize,
            TotalTickets = totalTickets,
            SelectedCategory = category
        };

        return View(viewModel);
    }
    
    public IActionResult ChangeItemsPerPage(int pageSize, string? category)
    {
        return RedirectToAction("Index", new { page = 1, pageSize = pageSize, category });
    }

    public IActionResult Booked()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<TicketModel>>("Cart") ?? new List<TicketModel>();
        
        return View(cart);
    }


    public IActionResult Crud()
    {
        return View("CRUD");
    }
    
    
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [HttpPost]
    public async Task<IActionResult> AddToCart(int ticketId)
    {
        var tickets = await _databaseService.GetAllTicketsAsync();
        var selectedTicket = tickets.FirstOrDefault(t => t.Id == ticketId);

        if (selectedTicket != null)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<TicketModel>>("Cart") ?? new List<TicketModel>();
            
            var simplifiedTicket = new TicketModel
            {
                Id = selectedTicket.Id,
                Performer = selectedTicket.Performer,
                Event = selectedTicket.Event,
                EventDate = selectedTicket.EventDate,
                Location = selectedTicket.Location,
                Price = selectedTicket.Price,
                CategoryId = selectedTicket.CategoryId
            };

            cart.Add(simplifiedTicket);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }
        return Ok();  
    }
}
