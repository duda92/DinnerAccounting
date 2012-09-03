using System;
using System.Linq;
using System.Linq.Expressions;
using DA.Dinners.Model;

namespace DA.Dinners.Domain.Abstract
{
    public interface IPropositionRepository : IDisposable
    {
        IQueryable<Proposition> All { get; }

        IQueryable<Proposition> AllIncluding(params Expression<Func<Proposition, object>>[] includeProperties);

        Proposition Find(int id);

        void InsertOrUpdate(Proposition proposition);

        void Delete(int id);

        void Save();
    }
}