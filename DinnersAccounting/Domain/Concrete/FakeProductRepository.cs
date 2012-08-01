using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Abstract;

namespace DataModel.Concrete
{
    public class FakeProductRepository : IProductRepository
    {

        #region IProductRepository Members

        public IQueryable<Product> Products
        {
            get { throw new NotImplementedException(); }
        }

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public void EditOrder(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(Product product)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
