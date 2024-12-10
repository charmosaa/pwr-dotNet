using DotNetLab9.Models.ViewModels;

namespace DotNetLab9.Data
{
    public class MockArticleContext : IArticleContext
    {
        List<Article> articles = new List<Article>()
        {
            new Article(1,"banana", 1.5m, DateTime.Now,FoodType.Fruit),
            new Article(2,"cheese", 2.79m, DateTime.Now,FoodType.Dairy)
        };
        public void AddArticle(Article article)
        {
            int nextNumber = articles.Max(a => a.Id) + 1;
            article.Id = nextNumber;
            articles.Add(article);
        }

        public Article? GetArticle(int id)
        {
            return articles.FirstOrDefault(a => a.Id == id);
        }

        public List<Article> GetArticles()
        {
            return articles;
        }

        public void RemoveArticle(int id)
        {
            Article? articleToRemove = articles.FirstOrDefault(a => a.Id == id);
            if(articleToRemove != null) 
                articles.Remove(articleToRemove);
        }

        public void UpdateArticle(Article article)
        {
            Article? articleToRemove = articles.FirstOrDefault(a => a.Id == article.Id);
            articles = articles.Select(a => (a.Id ==  article.Id) ? article : a).ToList();
        }
    }
}
