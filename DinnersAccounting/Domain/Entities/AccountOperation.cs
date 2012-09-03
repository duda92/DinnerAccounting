using System;
using System.ComponentModel.DataAnnotations;

namespace DA.Dinners.Domain
{
    /// <summary>
    /// Base for any operation with an account
    /// </summary>
    public abstract class AccountOperation
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the amount of operation
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the description of an operation
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the date of operation
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }
    }
}