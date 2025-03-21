using System.Diagnostics;
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

    // public async Task<IActionResult> Index()
    // {
    //     // await _databaseService.PopulateTicketsAsync();
    //     
    //     
    //     var tickets = await _databaseService.GetAllTicketsAsync();
    //     // var version = _databaseService.GetDatabaseVersion();
    //     // ViewBag.Version = version;
    //     return View(tickets);
    // }

    
    public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
    {
       
        var totalTickets = await _databaseService.GetTicketCountAsync();

       
        var tickets = await _databaseService.GetTicketsAsync(page, pageSize);

        
        var totalPages = (int)Math.Ceiling(totalTickets / (double)pageSize);
        
        var viewModel = new TicketPaginationViewModel
        {
            Tickets = tickets,
            CurrentPage = page,
            TotalPages = totalPages,
            PageSize = pageSize,
            TotalTickets = totalTickets
        };

        return View(viewModel);
    }

    public IActionResult ChangeItemsPerPage(int pageSize)
    {
        return RedirectToAction("Index", new { page = 1, pageSize = pageSize });
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}