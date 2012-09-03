using System;
using System.Linq;
using System.Linq.Expressions;

namespace DA.Dinners.Domain.Abstract
{
    public interface ICreditOperationRepository : IDisposable
    {
        IQueryable<CreditOperation> All { get; }

        IQueryable<CreditOperation> AllIncluding(params Expression<Func<CreditOperation, object>>[] includeProperties);

        CreditOperation Find(int id);

        void InsertOrUpdate(CreditOperation creditoperation);

        void Delete(int id);

        void Save();
    }
}