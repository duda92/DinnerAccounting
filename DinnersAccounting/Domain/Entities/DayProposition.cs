using System;

namespace DA.Dinners.Model
{
    /// <summary>
    /// Class for proposition for one day
    /// </summary>
    public class DayProposition : Proposition
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }
    }
}