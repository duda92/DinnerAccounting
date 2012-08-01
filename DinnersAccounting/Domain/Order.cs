using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel
{
    public class Order
    {
        public List<OrderDetail> OrderDetails;
        public Person Person;
        public DateTime Date;
        public OrderStatus CurrentStatus;
        public List<OrderStatus> Statuses;
    }
}
