using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Abstract;

namespace DataModel.Concrete
{
    public class FakeOrderRepository : IOrderRepository
    {
        #region IOrderRepository Members

        public IQueryable<Product> Orders
        {
            get { throw new NotImplementedException(); }
        }

        public void Add(Order order)
        {
            throw new NotImplementedException();
        }

        public void EditOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
