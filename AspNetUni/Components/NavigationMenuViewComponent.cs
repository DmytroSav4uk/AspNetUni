// Components/NavigationMenuViewComponent.cs

using Microsoft.AspNetCore.Mvc;
using AspNetUni.Services;

public class NavigationMenuViewComponent : ViewComponent
{
    private readonly IDatabaseService _databaseService;

    public NavigationMenuViewComponent(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categoryNames = await _databaseService.GetAllCategoryNamesAsync();
        return View(categoryNames); // передаєш в Default.cshtml
    }
}