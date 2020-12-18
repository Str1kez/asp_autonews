using System;
using System.Linq;
using asp_autonews.Domain.Entities;
using asp_autonews.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace asp_autonews.Domain.Repositories.Framework
{
    public class FInfoFieldRepository : IInfoFieldRepository
    {
        private readonly DbContext _context;

        public FInfoFieldRepository(DbContext context)
        {
            this._context = context;
        }

        public IQueryable<InfoField> GetInfoFields()
        {
            return _context.InfoFields;
        }

        public InfoField GetInfoFieldById(Guid id)
        {
            return _context.InfoFields.FirstOrDefault(x => x.Id == id);
        }

        public InfoField GetInfoFieldByKey(string key)
        {
            return _context.InfoFields.FirstOrDefault(x => x.Key == key);
        }

        public void SaveInfoField(InfoField entity)
        {
            if (entity.Id == default)
                _context.Entry(entity).State = EntityState.Added;
            else
                _context.Entry(entity).State = EntityState.Modified;
            
            _context.SaveChanges();
        }

        public void DeleteInfoField(Guid id)
        {
            _context.InfoFields.Remove(new InfoField() {Id = id});
            _context.SaveChanges();
        }
    }
}