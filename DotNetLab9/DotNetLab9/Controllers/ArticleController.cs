using System.Globalization;
using System.Linq.Expressions;
using DotNetLab9.Data;
using DotNetLab9.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetLab9.Controllers
{
    public class ArticleController : Controller
    {
        private IArticleContext ArticleContext;

        public ArticleController(IArticleContext articleContext)
        {
            ArticleContext = articleContext;
        }
        // GET: ArticleController
        public ActionResult Index()
        {
            return View(ArticleContext.GetArticles());
        }

        // GET: ArticleController/Details/5
        public ActionResult Details(int id)
        {
            return View(ArticleContext.GetArticle(id));
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            try
            {
                if (ModelState.IsValid)
                    ArticleContext.AddArticle(article);
                else
                {
                    if (!ModelState.IsValid)
                    {
                        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                        {
                            // Logowanie lub wyświetlanie błędów
                            Console.WriteLine(error.ErrorMessage);
                        }
                        return View(article);
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            
            catch
            {
                return View();
            }
            
        }

        // GET: ArticleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: ArticleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(ArticleContext.GetArticle(id));
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Article article)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    article.Id = id; // added
                    ArticleContext.UpdateArticle(article); //added
                }
                else
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        // Logowanie lub wyświetlanie błędów
                        Console.WriteLine(error.ErrorMessage);
                    }
                    return View(article);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(ArticleContext.GetArticle(id));
        }

        // POST: ArticleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Article article)
        {
            try
            {
                ArticleContext.RemoveArticle(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
