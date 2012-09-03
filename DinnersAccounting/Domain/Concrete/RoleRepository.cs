using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DA.Dinners.Domain.Abstract;

namespace DA.Dinners.Domain.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        DADinnersDomainContext context = new DADinnersDomainContext();

        public IQueryable<Role> All
        {
            get { return context.Roles; }
        }

        public IQueryable<Role> AllIncluding(params Expression<Func<Role, object>>[] includeProperties)
        {
            IQueryable<Role> query = context.Roles;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Role Find(int id)
        {
            return context.Roles.Find(id);
        }

        public void InsertOrUpdate(Role role)
        {
            if (role.Id == default(int))
            {
                // New entity
                context.Roles.Add(role);
            }
            else
            {
                // Existing entity
                context.Entry(role).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var role = context.Roles.Find(id);
            context.Roles.Remove(role);
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