using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DA.Dinners.Domain.Abstract;

namespace DA.Dinners.Domain.Concrete
{
    public class PersonRepository : IPersonRepository
    {
        DADinnersDomainContext context = new DADinnersDomainContext();

        public IQueryable<Person> All
        {
            get { return context.People; }
        }

        public IQueryable<Person> AllIncluding(params Expression<Func<Person, object>>[] includeProperties)
        {
            IQueryable<Person> query = context.People;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Person Find(int id)
        {
            return context.People.Find(id);
        }

        public void InsertOrUpdate(Person person)
        {
            if (person.Id == default(int))
            {
                // New entity
                context.People.Add(person);
            }
            else
            {
                // Existing entity
                context.Entry(person).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var person = context.People.Find(id);
            context.People.Remove(person);
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