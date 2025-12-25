using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppPractiece.Areas.Admin.ViewModels.CategoryVMs;
using WebAppPractiece.Data;
using WebAppPractiece.Models;

namespace WebAppPractiece.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _context.Categories.OrderByDescending(m => m.Id).Where(m => !m.IsDeleted).ToListAsync();

            IEnumerable<GetAllCategoryVM> getAllCategoryVMs = categories.Select(m => new GetAllCategoryVM()
            {
                Name = m.Name
            });

            //foreach (var item in categories)
            //{
            //    GetAllCategoryVM model = new()
            //    {
            //        Name = item.Name
            //    };

            //    getAllCategoryVMs.Append(model);

            //}

            return View(getAllCategoryVMs.ToList());
        }
    }
}
