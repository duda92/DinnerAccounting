using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Abstract;

namespace DataModel.Concrete
{
    public class FakePersonRepository : IPersonRepository
    {
        #region IPersonRepository Members

        public IQueryable<Person> Persons
        {
            get { throw new NotImplementedException(); }
        }

        public void Add(Person person)
        {
            throw new NotImplementedException();
        }

        public void EditOrder(Person person)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(Person person)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
