using System;
using System.Linq;
using asp_autonews.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace asp_autonews.Domain.Repositories.Abstract
{
    public interface IInfoFieldRepository
    {
        IQueryable<InfoField> GetInfoFields();
        InfoField GetInfoFieldById(Guid id);
        InfoField GetInfoFieldByKey(string key);
        void SaveInfoField(InfoField entity);
        void DeleteInfoField(Guid id);
    }
}