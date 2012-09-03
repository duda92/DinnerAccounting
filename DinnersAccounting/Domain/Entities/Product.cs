namespace DA.Dinners.Model
{
    /// <summary>
    /// class of any product
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of proposition item
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the summary of proposition item
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the price of proposition item
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this product is complex.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this product is complex; otherwise, <c>false</c>.
        /// </value>
        public bool isComplex { get; set; }

        public Product Clone()
        {
            return new Product { isComplex = isComplex, Price = Price, Summary = Summary, Title = Title };
        }
    }
}