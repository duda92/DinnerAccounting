using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel
{
    public class ProductProposition
    {
        public List<Product> Products;
        public DateTime StartDate;
        public DateTime EndDate;
        public ProductProposition PrevProposition;
        public ProductProposition NextProposition;
    }
}
