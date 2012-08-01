using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.Abstract
{
    public interface IPersonRepository
    {
        IQueryable<Person> Persons { get; }
        void Add(Person person);
        void EditOrder(Person person);
        void DeleteOrder(Person person);
    }
}
