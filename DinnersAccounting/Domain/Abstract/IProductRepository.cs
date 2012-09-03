using System;
using System.Linq;
using System.Linq.Expressions;
using DA.Dinners.Model;

namespace DA.Dinners.Domain.Abstract
{
    public interface IProductRepository : IDisposable
    {
        IQueryable<Product> All { get; }

        IQueryable<Product> AllIncluding(params Expression<Func<Product, object>>[] includeProperties);

        Product Find(int id);

        void InsertOrUpdate(Product product);

        void Delete(int id);

        void Save();
    }
}