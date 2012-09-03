using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DA.Dinners.Domain.Abstract;
using DA.Dinners.Model;

namespace DA.Dinners.Domain.Concrete
{
    public class PropositionRepository : IPropositionRepository
    {
        DADinnersDomainContext context = new DADinnersDomainContext();

        public IQueryable<Proposition> All
        {
            get { return context.Propositions; }
        }

        public IQueryable<Proposition> AllIncluding(params Expression<Func<Proposition, object>>[] includeProperties)
        {
            IQueryable<Proposition> query = context.Propositions;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Proposition Find(int id)
        {
            return context.Propositions.Find(id);
        }

        public void InsertOrUpdate(Proposition proposition)
        {
            if (proposition.Id == default(int))
            {
                // New entity
                context.Propositions.Add(proposition);
            }
            else
            {
                // Existing entity
                context.Entry(proposition).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var proposition = context.Propositions.Find(id);
            context.Propositions.Remove(proposition);
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