using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DA.Dinners.Domain
{
    /// <summary>
    /// Describe a regular order
    /// </summary>
    public class Order
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a person who is owner of the order
        /// </summary>
        /// <value>
        /// The person.
        /// </value>
        public virtual Person Person { get; set; }

        /// <summary>
        /// Gets or sets the date of the order
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the order detail info : product items with quantities
        /// </summary>
        /// <value>
        /// The order detail.
        /// </value>
        public virtual List<OrderDetail> OrderDetail { get; set; }

        /// <summary>
        /// Gets or sets the list of statuses mapped with corresponding time
        /// </summary>
        /// <value>
        /// The statuses.
        /// </value>
        public virtual List<OrderStatus> Statuses { get; set; }

        public Order()
        {
            OrderDetail = new List<OrderDetail>();
            Statuses = new List<OrderStatus>();
        }
    }
}