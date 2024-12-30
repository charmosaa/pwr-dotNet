using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab10RazorPages.Data;
using Lab10RazorPages.Models;

namespace Lab10RazorPages.Pages.Articles
{
    public class EditModel : PageModel
    {
        private readonly Lab10RazorPages.Data.StoreDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EditModel(StoreDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;
        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article =  await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            Article = article;
           ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Article).State = EntityState.Modified;

            try
            {
                var existingArticle = await _context.Articles.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
                if (existingArticle == null)
                {
                    return NotFound();
                }

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Handle image upload
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    // Delete old image if exists
                    if (!string.IsNullOrEmpty(existingArticle.ImagePath))
                    {
                        string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, existingArticle.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    Article.ImagePath = "/upload/" + uniqueFileName;
                }
                else
                {
                    // Keep the existing image if no new one is uploaded
                    Article.ImagePath = existingArticle.ImagePath;
                }

                // Update the article in the database
                _context.Articles.Update(Article);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during edit: " + ex.Message);
                return RedirectToPage("./Index");
            }


            return RedirectToPage("./Index");
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
