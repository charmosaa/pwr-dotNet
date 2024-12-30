using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab10RazorPages.Data;
using Lab10RazorPages.Models;

namespace Lab10RazorPages.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly Lab10RazorPages.Data.StoreDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateModel(StoreDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                Article.ImagePath = "/upload/" + uniqueFileName;
            }

            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
