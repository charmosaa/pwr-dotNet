using DotNetLab9.Models.ViewModels;

namespace DotNetLab9.Data
{
    public interface IArticleContext
    {
        List<Article> GetArticles();
        Article? GetArticle(int id);
        void AddArticle(Article person);
        void RemoveArticle(int id);
        void UpdateArticle(Article person);
    }
}
