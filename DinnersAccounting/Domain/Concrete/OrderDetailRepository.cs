using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DA.Dinners.Domain.Abstract;

namespace DA.Dinners.Domain.Concrete
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        DADinnersDomainContext context = new DADinnersDomainContext();

        public IQueryable<OrderDetail> All
        {
            get { return context.OrderDetails; }
        }

        public IQueryable<OrderDetail> AllIncluding(params Expression<Func<OrderDetail, object>>[] includeProperties)
        {
            IQueryable<OrderDetail> query = context.OrderDetails;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderDetail Find(int id)
        {
            return context.OrderDetails.Find(id);
        }

        public void InsertOrUpdate(OrderDetail orderdetail)
        {
            if (orderdetail.Id == default(int))
            {
                // New entity
                context.OrderDetails.Add(orderdetail);
            }
            else
            {
                // Existing entity
                context.Entry(orderdetail).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var orderdetail = context.OrderDetails.Find(id);
            context.OrderDetails.Remove(orderdetail);
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