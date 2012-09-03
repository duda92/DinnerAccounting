using System;
using System.Linq;
using System.Linq.Expressions;

namespace DA.Dinners.Domain.Abstract
{
    public interface IOrderRepository : IDisposable
    {
        IQueryable<Order> All { get; }

        IQueryable<Order> AllIncluding(params Expression<Func<Order, object>>[] includeProperties);

        Order Find(int id);

        void InsertOrUpdateWithPerson(Order order, string domainName);

        void UpdateAccountOperation(Order order);

        void InsertOrUpdate(Order order);

        void Delete(int id);

        void Save();
    }
}