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
    public class IndexModel : PageModel
    {
        private readonly Lab10RazorPages.Data.StoreDbContext _context;

        public IndexModel(Lab10RazorPages.Data.StoreDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Article = await _context.Articles
                .Include(a => a.Category).ToListAsync();
        }
    }
}
