using System;
using System.Linq;
using asp_autonews.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace asp_autonews.Domain.Repositories.Framework
{
    public class FArticleRepository : IArticleRepository
    {
        private readonly DbContext _context;

        public FArticleRepository(DbContext context)
        {
            this._context = context;
        }
        
        public IQueryable<Entities.Article> GetArticles()
        {
            return _context.Articles;
        }

        public Entities.Article GetArticleById(Guid id)
        {
            return _context.Articles.FirstOrDefault(x => x.Id == id);
        }

        public void SaveArticle(Entities.Article entity)
        {
            // Пометка, что это - новая статья (текст)
            if (entity.Id == default)
                _context.Entry(entity).State = EntityState.Added;
            else
                _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();
        }

        public void DeleteArticle(Guid id)
        {
            _context.Articles.Remove(new Entities.Article() {Id = id});
            _context.SaveChanges();
        }
    }
}