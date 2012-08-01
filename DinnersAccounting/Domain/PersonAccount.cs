using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel
{
    public class PersonAccount
    {
        public Person Person;
        public List<DataModel.AccountOperation> Operations;
        public decimal Balance;
    }
}
