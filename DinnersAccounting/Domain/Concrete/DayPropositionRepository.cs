using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DA.Dinners.Domain.Abstract;
using DA.Dinners.Model;

namespace DA.Dinners.Domain.Concrete
{
    public class DayPropositionRepository : IDayPropositionRepository
    {
        DADinnersDomainContext context = new DADinnersDomainContext();

        public IQueryable<DayProposition> All
        {
            get { return context.DayPropositions; }
        }

        public IQueryable<DayProposition> AllIncluding(params Expression<Func<DayProposition, object>>[] includeProperties)
        {
            IQueryable<DayProposition> query = context.DayPropositions;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public DayProposition Find(int id)
        {
            return context.DayPropositions.Find(id);
        }

        public void InsertOrUpdate(DayProposition dayproposition)
        {
            if (dayproposition.Id == default(int))
            {
                // New entity
                context.DayPropositions.Add(dayproposition);
            }
            else
            {
                // Existing entity
                context.Entry(dayproposition).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var dayproposition = context.DayPropositions.Find(id);
            context.DayPropositions.Remove(dayproposition);
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