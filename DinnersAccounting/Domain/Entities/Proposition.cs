using System.Collections.Generic;

namespace DA.Dinners.Model
{
    /// <summary>
    /// Base class for any proposition
    /// </summary>
    public abstract class Proposition
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the summary of proposition
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the list of proposition items.
        /// </summary>
        /// <value>
        /// The proposition items.
        /// </value>
        public List<Product> Products { get; set; }

        public Proposition()
        {
            Products = new List<Product>();
        }
    }
}