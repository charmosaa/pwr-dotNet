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
    public class DetailsModel : PageModel
    {
        private readonly Lab10RazorPages.Data.StoreDbContext _context;

        public DetailsModel(Lab10RazorPages.Data.StoreDbContext context)
        {
            _context = context;
        }

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
    }
}
