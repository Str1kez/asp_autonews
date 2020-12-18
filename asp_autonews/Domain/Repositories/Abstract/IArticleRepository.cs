using System.Linq;
using System;

namespace asp_autonews.Domain.Repositories.Abstract
{
    public interface IArticleRepository
    {
        IQueryable<Entities.Article> GetArticles();
        Entities.Article GetArticleById(Guid id);
        void SaveArticle(Entities.Article entity);
        void DeleteArticle(Guid id);
    }
}