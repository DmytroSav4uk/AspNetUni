using Microsoft.AspNetCore.Mvc;
using ClassLibraryUni.Models;
using ClassLibraryUni.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebApiUni.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public CrudController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // -------------------- Tickets --------------------

        [HttpGet("tickets")]
        public async Task<ActionResult<IEnumerable<TicketModel>>> GetAllTickets()
        {
            var tickets = await _databaseService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        [HttpPost("tickets")]
        public async Task<IActionResult> AddTicket([FromBody] TicketModel ticket)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _databaseService.AddTicketAsync(ticket);
            return Ok(ticket);
        }

        [HttpPut("tickets/{id}")]
        public async Task<IActionResult> EditTicket(int id, [FromBody] TicketModel ticket)
        {
            if (id != ticket.Id)
                return BadRequest("ID mismatch");

            await _databaseService.UpdateTicketAsync(ticket);
            return NoContent();
        }

        [HttpDelete("tickets/{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            await _databaseService.DeleteTicketAsync(id);
            return NoContent();
        }

        // -------------------- Categories --------------------

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetAllCategories()
        {
            var categories = await _databaseService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryModel category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _databaseService.AddCategoryAsync(category);
            return Ok(category);
        }

        [HttpPut("categories/{id}")]
        public async Task<IActionResult> EditCategory(int id, [FromBody] CategoryModel category)
        {
            if (id != category.Id)
                return BadRequest("ID mismatch");

            await _databaseService.UpdateCategoryAsync(category);
            return NoContent();
        }

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _databaseService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
