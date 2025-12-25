using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAppPractiece.Services;
using WebAppPractiece.ViewModels;
using WebAppPractiece.ViewModels.BasketVMs;

namespace WebAppPractiece.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly LayoutService _layoutService;
        public HeaderViewComponent(LayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basketProduct = HttpContext.Request.Cookies["basket"];
            int count = 0;
            decimal price = 0;

            if (basketProduct != null)
            {
                List<BasketVM> basket = JsonSerializer.Deserialize<List<BasketVM>>(basketProduct);
                count = basket.Sum(b => b.Count);
                price = basket.Sum(b => b.Count * b.Price);
            }

            Dictionary<string, string> settings = await _layoutService.GetAllSettings();

            HeaderVM headerVM = new()
            {
                Settings = settings,
                BasketCount = count,
                BasketPrice = price
            };

            return await Task.FromResult(View(headerVM));
        }
    }
}
