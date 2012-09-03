using DA.Dinners.Model;

namespace DA.Dinners.Domain
{
    /// <summary>
    /// Describe single item of an order : product itme and quantity
    /// </summary>
    public class OrderDetail
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the quantity of product in order
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the product instance
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }
    }
}