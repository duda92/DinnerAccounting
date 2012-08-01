using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.Abstract
{
    interface IProductPropositionRepository
    {
        IQueryable<ProductProposition> ProductPropositions { get; }
        void Add(ProductProposition entity);       
        void Edit(ProductProposition entity);
        void Delete(ProductProposition entity);

    }
}
