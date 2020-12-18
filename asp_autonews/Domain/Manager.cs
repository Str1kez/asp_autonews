using asp_autonews.Domain.Entities;
using asp_autonews.Domain.Repositories.Abstract;

namespace asp_autonews.Domain
{
    public class Manager
    {
        public IArticleRepository Articles { get; set; }
        public IInfoFieldRepository InfoFields { get; set; }

        public Manager(IArticleRepository articlesRep, IInfoFieldRepository infoFieldsRep)
        {
            Articles = articlesRep;
            InfoFields = infoFieldsRep;
        }
    }
}