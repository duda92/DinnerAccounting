using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DA.Dinners.Domain.Abstract;

namespace DA.Dinners.Domain.Concrete
{
    public class DebitOperationRepository : IDebitOperationRepository
    {
        DADinnersDomainContext context = new DADinnersDomainContext();

        public IQueryable<DebitOperation> All
        {
            get { return context.DebitOperations; }
        }

        public IQueryable<DebitOperation> AllIncluding(params Expression<Func<DebitOperation, object>>[] includeProperties)
        {
            IQueryable<DebitOperation> query = context.DebitOperations;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public DebitOperation Find(int id)
        {
            return context.DebitOperations.Find(id);
        }

        public void InsertOrUpdate(DebitOperation debitoperation)
        {
            if (debitoperation.Id == default(int))
            {
                // New entity
                context.DebitOperations.Add(debitoperation);
            }
            else
            {
                // Existing entity
                context.Entry(debitoperation).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var debitoperation = context.DebitOperations.Find(id);
            context.DebitOperations.Remove(debitoperation);
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