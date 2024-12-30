using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab10RazorPages.Data;
using Lab10RazorPages.Models;

namespace Lab10RazorPages.Pages.Articles
{
    public class DeleteModel : PageModel
    {
        private readonly Lab10RazorPages.Data.StoreDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DeleteModel(StoreDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.Include(a => a.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            }
            else
            {
                Article = article;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                if (!string.IsNullOrEmpty(article.ImagePath) && !article.ImagePath.StartsWith("http"))
                {
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, article.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                }

                Article = article;
                _context.Articles.Remove(Article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
