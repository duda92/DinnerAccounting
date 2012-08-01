using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void Add(Product product);
        void EditOrder(Product product);
        void DeleteOrder(Product product);
    }
}
