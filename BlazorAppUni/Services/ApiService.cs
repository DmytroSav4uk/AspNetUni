using System.Net.Http.Json;
using ClassLibraryUni.Models;

namespace BlazorAppUni.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
        }

        // ******************************
        public async Task<List<TicketModel>> GetAllTicketsAsync() =>
            await _http.GetFromJsonAsync<List<TicketModel>>("api/crud/tickets");

        public async Task AddTicketAsync(TicketModel ticket) =>
            await _http.PostAsJsonAsync("api/crud/tickets", ticket);

        public async Task UpdateTicketAsync(TicketModel ticket) =>
            await _http.PutAsJsonAsync($"api/crud/tickets/{ticket.Id}", ticket);

        public async Task DeleteTicketAsync(int id) =>
            await _http.DeleteAsync($"api/crud/tickets/{id}");

        // ******************************
        public async Task<List<CategoryModel>> GetAllCategoriesAsync() =>
            await _http.GetFromJsonAsync<List<CategoryModel>>("api/crud/categories");

        public async Task AddCategoryAsync(CategoryModel category) =>
            await _http.PostAsJsonAsync("api/crud/categories", category);

        public async Task UpdateCategoryAsync(CategoryModel category) =>
            await _http.PutAsJsonAsync($"api/crud/categories/{category.Id}", category);

        public async Task DeleteCategoryAsync(int id) =>
            await _http.DeleteAsync($"api/crud/categories/{id}");
    }
}