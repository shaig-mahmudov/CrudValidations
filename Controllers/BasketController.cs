using WebAppPractiece.ViewModels.BasketVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppPractiece.Data;
using WebAppPractiece.Models;
using System.Text.Json;
using WebAppPractiece.ViewModels.BasketVMs;

namespace WebAppPractiece.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;

        public BasketController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<BasketVM> basketList;

            if (Request.Cookies["basket"] != null)
            {
                basketList = JsonSerializer.Deserialize<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basketList = new List<BasketVM>();
            }

            List<BasketDetailVM> basketDetails = new();

            if (basketList.Count == 0) return View(basketDetails);

            var basketIds = basketList.Select(b => b.Id).ToList();

            var products = await _context.Products
                .Include(m => m.ProductImages)
                .Include(m => m.Category) 
                .Where(m => basketIds.Contains(m.Id) && !m.IsDeleted)
                .ToListAsync();

            foreach (var item in basketList)
            {

                var product = products.FirstOrDefault(p => p.Id == item.Id);
                if (product != null)
                {
                    basketDetails.Add(new BasketDetailVM
                    {
                        Id = item.Id,
                        Count = item.Count,
                        Image = product.ProductImages.FirstOrDefault(m => m.IsMain)?.Image,
                        Name = product.Name,
                        Category = product.Category.Name,
                        Price = product.Price,
                        TotalPrice = product.Price * item.Count
                    });
                }
            }

            return View(basketDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int? id)
        {
            if (id is null) return BadRequest();

            bool isExist = await _context.Products.AnyAsync(p => p.Id == id && !p.IsDeleted);
            if (!isExist) return NotFound();

            List<BasketVM> basket;

            if (Request.Cookies["basket"] != null)
            {
                basket = JsonSerializer.Deserialize<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            var existProduct = basket.FirstOrDefault(m => m.Id == id);

            if (existProduct == null)
            {
                var productFromDb = await _context.Products.FindAsync(id);

                basket.Add(new BasketVM
                {
                    Id = (int)id,
                    Count = 1,
                    Price = productFromDb.Price
                });
            }
            else
            {
                existProduct.Count++;
            }


            Response.Cookies.Append("basket", JsonSerializer.Serialize(basket), new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(7),
                HttpOnly = true 
            });

            int totalCount = basket.Sum(x => x.Count);
            decimal totalPrice = basket.Sum(x => x.Count * x.Price);

            return Ok(new { count = totalCount, totalPrice = totalPrice, message = "Product Added to Cart!" });
        }

        public IActionResult Delete(int id)
        {
            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> basket = JsonSerializer.Deserialize<List<BasketVM>>(Request.Cookies["basket"]);
                var itemToRemove = basket.FirstOrDefault(x => x.Id == id);

                if (itemToRemove != null)
                {
                    basket.Remove(itemToRemove);
                }

                Response.Cookies.Append("basket", JsonSerializer.Serialize(basket), new CookieOptions
                {
                    MaxAge = TimeSpan.FromDays(7),
                    HttpOnly = true
                });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}