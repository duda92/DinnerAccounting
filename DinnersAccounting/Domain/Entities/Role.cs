using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DA.Dinners.Domain
{
    /// <summary>
    /// The role of user in application
    /// </summary>
    public class Role
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets list of the persons in role.
        /// </summary>
        /// <value>
        /// The persons.
        /// </value>
        public virtual List<Person> People { get; set; }

        public Role()
        {
            People = new List<Person>();
        }
    }
}