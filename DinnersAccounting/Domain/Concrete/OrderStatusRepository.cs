using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DA.Dinners.Domain.Concrete
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        DADinnersDomainContext context = new DADinnersDomainContext();

        public IQueryable<OrderStatus> All
        {
            get { return context.OrderStatus; }
        }

        public IQueryable<OrderStatus> AllIncluding(params Expression<Func<OrderStatus, object>>[] includeProperties)
        {
            IQueryable<OrderStatus> query = context.OrderStatus;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderStatus Find(int id)
        {
            return context.OrderStatus.Find(id);
        }

        public void InsertOrUpdate(OrderStatus orderstatus)
        {
            if (orderstatus.Id == default(int))
            {
                // New entity
                context.OrderStatus.Add(orderstatus);
            }
            else
            {
                // Existing entity
                context.Entry(orderstatus).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var orderstatus = context.OrderStatus.Find(id);
            context.OrderStatus.Remove(orderstatus);
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

    public interface IOrderStatusRepository : IDisposable
    {
        IQueryable<OrderStatus> All { get; }

        IQueryable<OrderStatus> AllIncluding(params Expression<Func<OrderStatus, object>>[] includeProperties);

        OrderStatus Find(int id);

        void InsertOrUpdate(OrderStatus orderstatus);

        void Delete(int id);

        void Save();
    }
}