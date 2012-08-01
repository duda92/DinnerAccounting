using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.Abstract
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void Add(Order order);
        void EditOrder(Order order);
        void DeleteOrder(Order order);
    }
}
