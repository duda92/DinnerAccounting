using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Abstract;

namespace DataModel.Concrete
{
    class FakeProductPropositionRepository : IProductPropositionRepository
    {
        #region IProductPropositionRepository Members

        public IQueryable<Product> ProductPropositions
        {
            get { throw new NotImplementedException(); }
        }

        public void Add(ProductProposition entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(ProductProposition entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ProductProposition entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
