
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab10RazorPages.Data;
using Lab10RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lab10RazorPages.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly StoreDbContext _context;

        public IndexModel(StoreDbContext context)
        {
            _context = context;
        }

        public IList<Article> Articles { get; set; }
        public IList<Category> Categories { get; set; }

        public async Task OnGetAsync(int? categoryId)
        {
            Categories = await _context.Categories.ToListAsync();

            if (categoryId.HasValue)
            {
                Articles = await _context.Articles
                    .Where(a => a.CategoryId == categoryId)
                    .ToListAsync();

                TempData["Message"] = "You are viewing articles from category: " + Categories.FirstOrDefault(c => c.Id == categoryId)?.Name;
            }
            else
            {
                Articles = await _context.Articles.ToListAsync();
            }
        }

    }
}
