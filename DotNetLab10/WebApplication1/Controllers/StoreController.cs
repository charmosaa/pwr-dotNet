using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

public class StoreController : Controller
{
    private readonly StoreDbContext _context;

    public StoreController(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? categoryId)
    {
        var articles = await _context.Articles.ToListAsync();

        if (categoryId != null)
        {
            articles = articles.Where(a => a.CategoryId == categoryId).ToList();
        }

        ViewBag.Categories = await _context.Categories.ToListAsync();

        return View(articles);
    }
}
