using System;
using System.Linq;
using System.Linq.Expressions;
using DA.Dinners.Model;

namespace DA.Dinners.Domain.Abstract
{
    public interface IContinuousPropositionRepository : IDisposable
    {
        IQueryable<ContinuousProposition> All { get; }

        IQueryable<ContinuousProposition> AllIncluding(params Expression<Func<ContinuousProposition, object>>[] includeProperties);

        ContinuousProposition Find(int id);

        void InsertOrUpdate(ContinuousProposition continuousproposition);

        void Delete(int id);

        void Save();
    }
}