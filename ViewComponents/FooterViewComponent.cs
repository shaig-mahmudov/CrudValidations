using Microsoft.AspNetCore.Mvc;
using WebAppPractiece.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAppPractiece.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly LayoutService _layoutService;
        public FooterViewComponent(LayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, string> settings = await _layoutService.GetAllSettings();

            return await Task.FromResult(View(settings));
        }
    }
}