using System;
using System.ComponentModel.DataAnnotations;

namespace DA.Dinners.Domain
{
    /// <summary>
    /// Describe status of order with corresponding time it was set
    /// </summary>
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public virtual Order Order { get; set; }

        /// <summary>
        /// Gets or sets the status value.
        /// </summary>
        /// <value>
        /// The status value.
        /// </value>
        public int StatusValue { get; set; }

        /// <summary>
        /// Gets or sets the description of order
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Define is this status current of any order
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is current; otherwise, <c>false</c>.
        /// </value>
        public bool isCurrent { get; set; }

        /// <summary>
        /// Gets or sets the date of creating an order
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }
    }
}