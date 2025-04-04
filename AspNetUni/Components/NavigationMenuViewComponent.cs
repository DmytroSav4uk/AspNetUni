using AspNetUni.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetUni.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IDatabaseService _databaseService;

        public NavigationMenuViewComponent(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _databaseService.GetAllCategoriesAsync();
            return View(categories);
        }
    }
}