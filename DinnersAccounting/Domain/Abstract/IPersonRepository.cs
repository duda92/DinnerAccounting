using System;
using System.Linq;
using System.Linq.Expressions;

namespace DA.Dinners.Domain.Abstract
{
    public interface IPersonRepository : IDisposable
    {
        IQueryable<Person> All { get; }

        IQueryable<Person> AllIncluding(params Expression<Func<Person, object>>[] includeProperties);

        Person Find(int id);

        void InsertOrUpdate(Person person);

        void Delete(int id);

        void Save();
    }
}