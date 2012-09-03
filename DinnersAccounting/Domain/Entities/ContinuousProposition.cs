using System;
using System.Collections.Generic;

namespace DA.Dinners.Model
{
    /// <summary>
    /// Class for continuous proposition with start date and end date
    /// </summary>
    public class ContinuousProposition : Proposition
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the day propositions.
        /// </summary>
        /// <value>
        /// The day propositions.
        /// </value>
        public List<DayProposition> DayPropositions { get; set; }

        public ContinuousProposition()
        {
            DayPropositions = new List<DayProposition>();
        }

        public void Init()
        {
            int days = EndDate.Subtract(StartDate).Days;
            for (int i = 0; i < days; i++)
                DayPropositions.Add(new DayProposition() { Date = StartDate.AddDays(i) });
        }
    }
}