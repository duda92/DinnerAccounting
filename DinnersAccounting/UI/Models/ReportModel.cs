using System;

namespace UI.Models
{
    public class ReportModel
    {
        public string UserName { get; set; }

        public DateTime DateOfDinner { get; set; }
    
        public string OrderDetails { get; set; }

        public decimal Price { get; set; }

        public decimal UsersAmountOfMoney { get; set; }

    }
}