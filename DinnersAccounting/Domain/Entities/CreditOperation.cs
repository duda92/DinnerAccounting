namespace DA.Dinners.Domain
{
    /// <summary>
    /// Operation with account to pay for an order
    /// </summary>
    public class CreditOperation : AccountOperation
    {
        /// <summary>
        /// Order to pay by the credit operation
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public virtual Order Order { get; set; }
    }
}