using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DA.Dinners.Domain.Abstract;
using DA.Dinners.Model;

namespace DA.Dinners.Domain.Concrete
{
    public class ContinuousPropositionRepository : IContinuousPropositionRepository
    {
        DADinnersDomainContext context = new DADinnersDomainContext();

        public IQueryable<ContinuousProposition> All
        {
            get { return context.ContinuousPropositions; }
        }

        public IQueryable<ContinuousProposition> AllIncluding(params Expression<Func<ContinuousProposition, object>>[] includeProperties)
        {
            IQueryable<ContinuousProposition> query = context.ContinuousPropositions;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ContinuousProposition Find(int id)
        {
            return context.ContinuousPropositions.Find(id);
        }

        public void InsertOrUpdate(ContinuousProposition continuousproposition)
        {
            if (continuousproposition.Id == default(int))
            {
                // New entity
                context.ContinuousPropositions.Add(continuousproposition);
            }
            else
            {
                // Existing entity
                context.Entry(continuousproposition).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var continuousproposition = context.ContinuousPropositions.Include(prop => prop.Products).Include(prop => prop.DayPropositions.Select(dp => dp.Products)).SingleOrDefault(pp => pp.Id == id);

            continuousproposition.DayPropositions.Clear();
            continuousproposition.Products.Clear();

            context.ContinuousPropositions.Remove(continuousproposition);
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