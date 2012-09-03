using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DA.Dinners.Domain.Abstract;

namespace DA.Dinners.Domain.Concrete
{
    public class CreditOperationRepository : ICreditOperationRepository
    {
        DADinnersDomainContext context = new DADinnersDomainContext();

        public IQueryable<CreditOperation> All
        {
            get { return context.CreditOperations; }
        }

        public IQueryable<CreditOperation> AllIncluding(params Expression<Func<CreditOperation, object>>[] includeProperties)
        {
            IQueryable<CreditOperation> query = context.CreditOperations;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public CreditOperation Find(int id)
        {
            return context.CreditOperations.Find(id);
        }

        public void InsertOrUpdate(CreditOperation creditoperation)
        {
            if (creditoperation.Id == default(int))
            {
                // New entity
                context.CreditOperations.Add(creditoperation);
            }
            else
            {
                // Existing entity
                context.Entry(creditoperation).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var creditoperation = context.CreditOperations.Find(id);
            context.CreditOperations.Remove(creditoperation);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}