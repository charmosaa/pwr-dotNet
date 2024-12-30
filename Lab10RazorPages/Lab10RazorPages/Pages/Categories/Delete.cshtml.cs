using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab10RazorPages.Data;
using Lab10RazorPages.Models;

namespace Lab10RazorPages.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly Lab10RazorPages.Data.StoreDbContext _context;

        public DeleteModel(Lab10RazorPages.Data.StoreDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {

                if (category.Name == "Other")
                {
                    // Set the error message in TempData
                    TempData["Error"] = "The 'Other' category cannot be deleted.";
                    return RedirectToPage("./Index");  // Use RedirectToPage for Razor Pages
                }

                var articles = await _context.Articles
                    .Where(a => a.CategoryId == id)
                    .ToListAsync();

                var defaultCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Other");
                if (defaultCategory == null)
                    return BadRequest("Default category 'Other' is missing.");

                // Move articles to the default category
                foreach (var article in articles)
                {
                    article.CategoryId = defaultCategory.Id;
                }

                // Delete the category
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
