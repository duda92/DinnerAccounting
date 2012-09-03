using System;
using System.Linq;
using System.Linq.Expressions;

namespace DA.Dinners.Domain.Abstract
{
    public interface IRoleRepository : IDisposable
    {
        IQueryable<Role> All { get; }

        IQueryable<Role> AllIncluding(params Expression<Func<Role, object>>[] includeProperties);

        Role Find(int id);

        void InsertOrUpdate(Role role);

        void Delete(int id);

        void Save();
    }
}