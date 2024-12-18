using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly StoreDbContext _context;
        private IWebHostEnvironment _hostingEnvironment;

        public ArticlesController(StoreDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var storeDbContext = _context.Articles.Include(a => a.Category);
            return View(await storeDbContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        //POST: Articles/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,CategoryId,ImagePath")] Article article, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                 if (imageFile != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    article.ImagePath = "/upload/"+uniqueFileName;
                } 
               

                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }


        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,CategoryId,ImagePath")] Article article, IFormFile? imageFile)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Obsługa przesyłania lokalnego pliku
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload");
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine(uploadPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        article.ImagePath = "/upload/" + fileName;
                    }
                    // Obsługa zewnętrznego URL-a
                    else if (!string.IsNullOrEmpty(article.ImagePath) && Uri.IsWellFormedUriString(article.ImagePath, UriKind.Absolute))
                    {
                        try
                        {
                            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload");
                            if (!Directory.Exists(uploadPath))
                            {
                                Directory.CreateDirectory(uploadPath);
                            }

                            using (var httpClient = new HttpClient())
                            {
                                var response = await httpClient.GetAsync(article.ImagePath);
                                if (response.IsSuccessStatusCode)
                                {
                                    var fileExtension = Path.GetExtension(article.ImagePath);
                                    var fileName = Guid.NewGuid().ToString() + fileExtension;
                                    var filePath = Path.Combine(uploadPath, fileName);

                                    await using var fileStream = new FileStream(filePath, FileMode.Create);
                                    await response.Content.CopyToAsync(fileStream);

                                    // Aktualizacja ścieżki na lokalną
                                    article.ImagePath = "/upload/" + fileName;
                                }
                                else
                                {
                                    // Jeśli nie udało się pobrać pliku, zachowaj starą ścieżkę
                                    ModelState.AddModelError("", "Failed to fetch image from the provided URL.");
                                }
                            }
                        }
                        catch
                        {
                            ModelState.AddModelError("", "An error occurred while fetching the external image.");
                        }
                    }

                    // Zaktualizuj artykuł w bazie danych
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }


        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article != null)
            {
                if (!string.IsNullOrEmpty(article.ImagePath) && !article.ImagePath.StartsWith("http"))
                {
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, article.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                }
                
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
