using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DA.Dinners.Domain.Concrete;

namespace DA.Dinners.Domain
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the domain name of user.
        /// </summary>
        /// <value>
        /// The domain name of user.
        /// </value>
        public string DomainName { get; set; }

        /// <summary>
        /// Gets or sets the roles of person
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public virtual List<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the person account.
        /// </summary>
        /// <value>
        /// The person account.
        /// </value>

        ///// <summary>
        ///// Gets or sets the orders of person
        ///// </summary>
        ///// <value>
        ///// The orders.
        ///// </value>
        public virtual List<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        /// <value>
        /// The balance.
        /// </value>
        public decimal Balance { get; set; }

        public virtual List<DA.Dinners.Domain.AccountOperation> Operations { get; set; }

        public Person()
        {
            Roles = new List<Role>();
            Orders = new List<Order>();
            Operations = new List<AccountOperation>();
        }

        public void CalculateBalance()
        {
            Balance = 0;
            foreach (var operation in Operations)
            {
                if (operation is CreditOperation)
                    Balance -= operation.Amount;
                else
                    Balance += operation.Amount;
            }
        }

        public string GetName()
        {
            return DomainService.Instance.GetFullName(DomainName);
        }
    }
}