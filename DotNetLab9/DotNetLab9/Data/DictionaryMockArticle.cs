using DotNetLab9.Models.ViewModels;
using System.Xml.Linq;

namespace DotNetLab9.Data
{
    public class DictionaryMockArticle : IArticleContext
    {
        Dictionary<int,Article> articles = new Dictionary<int, Article>()
        {
            {1, new Article(1,"apple", 1.5m, DateTime.Now,FoodType.Fruit) },
            {2, new Article(2,"salmon", 2.79m, DateTime.Now,FoodType.Fish) }
        };
        public void AddArticle(Article article)
        {
            int nextNumber = 1;
            if(articles.Any()) 
                nextNumber = articles.Max(a => a.Key) + 1;
            article.Id = nextNumber;
            articles[nextNumber] = article;
        }

        public Article? GetArticle(int id)
        {
            return articles[id];
        }

        public ICollection<Article> GetArticles()
        {
            return articles.Values;
        }

        public void RemoveArticle(int id)
        {
            articles.Remove(id);
        }

        public void UpdateArticle(Article article)
        {
            if (articles.ContainsKey(article.Id))
            {
                articles[article.Id] = article;
            }
        }
    }
}
