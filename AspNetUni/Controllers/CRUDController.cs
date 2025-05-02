using AspNetUni.Models;
using AspNetUni.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetUni.Controllers
{
    public class CrudController : Controller
    {
        private readonly IDatabaseService _databaseService;

        public CrudController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IActionResult> Crud(string view = "tickets")
        {
            var model = new CrudViewModel { SelectedView = view };

            if (view == "categories")
            {
                model.Categories = await _databaseService.GetAllCategoriesAsync();
            }
            else
            {
                model.Tickets = await _databaseService.GetAllTicketsAsync();
                model.Categories = await _databaseService.GetAllCategoriesAsync();
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddTicket(TicketModel ticket)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "incorrect data.";
                return RedirectToAction("Crud", new { view = "tickets" });
            }

            await _databaseService.AddTicketAsync(ticket);
            return RedirectToAction("Crud", new { view = "tickets" });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            await _databaseService.DeleteTicketAsync(id);
            return RedirectToAction("Crud", new { view = "tickets" });
        }
        
        
        [HttpPost]
        public async Task<IActionResult> EditTicket(TicketModel ticket)
        {
            await _databaseService.UpdateTicketAsync(ticket);
            return RedirectToAction("Crud", new { view = "tickets" });
        }

        
        
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryModel category)
        {
            await _databaseService.AddCategoryAsync(category);
            return RedirectToAction("Crud", new { view = "categories" });
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryModel category)
        {
            await _databaseService.UpdateCategoryAsync(category);
            return RedirectToAction("Crud", new { view = "categories" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _databaseService.DeleteCategoryAsync(id);
            return RedirectToAction("Crud", new { view = "categories" });
        }
        
    }
}