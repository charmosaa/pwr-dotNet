﻿using Microsoft.AspNetCore.Mvc;
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

    public IActionResult AddToCart(int articleId)
    {
        string cookieName = $"article{articleId}";
        int quantity = 1;

        if (Request.Cookies.ContainsKey(cookieName))
        {
            if (int.TryParse(Request.Cookies[cookieName], out int currentQuantity))
            {
                quantity = currentQuantity + 1;
            }
        }

        Response.Cookies.Append(cookieName, quantity.ToString(), new CookieOptions
        {
            Expires = DateTime.Now.AddDays(7)
        });

        return RedirectToAction("Index");
    }

    public IActionResult Cart()
    {
        var cartItems = new List<(WebApplication1.Models.Article Article, int Quantity)>();

        foreach (var cookie in Request.Cookies)
        {
            if (cookie.Key.StartsWith("article"))
            {
                if (int.TryParse(cookie.Key.Substring("article".Length), out int articleId) &&
                    int.TryParse(cookie.Value, out int quantity))
                {
                    var article = _context.Articles.Include(a => a.Category).FirstOrDefault(a => a.Id == articleId);
                    if (article != null)
                    {
                        cartItems.Add((article, quantity));
                    }
                }
            }
        }

        return View(cartItems);
    }

    public IActionResult IncreaseQuantity(int articleId)
    {
        string cookieName = $"article{articleId}";
        if (Request.Cookies.ContainsKey(cookieName))
        {
            if (int.TryParse(Request.Cookies[cookieName], out int currentQuantity))
            {
                Response.Cookies.Append(cookieName, (currentQuantity + 1).ToString(), new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                });
            }
        }
        return RedirectToAction("Cart");
    }

    public IActionResult DecreaseQuantity(int articleId)
    {
        string cookieName = $"article{articleId}";
        if (Request.Cookies.ContainsKey(cookieName))
        {
            if (int.TryParse(Request.Cookies[cookieName], out int currentQuantity) && currentQuantity > 1)
            {
                Response.Cookies.Append(cookieName, (currentQuantity - 1).ToString(), new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                });
            }
            else
            {
                Response.Cookies.Delete(cookieName);
            }
        }
        return RedirectToAction("Cart");
    }

    public IActionResult RemoveFromCart(int articleId)
    {
        string cookieName = $"article{articleId}";
        Response.Cookies.Delete(cookieName);
        return RedirectToAction("Cart");
    }


}
