// <copyright file="Product.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace Auction.DomainLayer.DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>Class used to define a poduct.</summary>
    public class Product
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "Product name cannot be null", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The product name must have between 2 and 100 chars")]
        public string Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [Required(ErrorMessage = "The description must be present")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The category name must be between 3 and 300 chars")]
        public string Description { get; set; }

        /// <summary>Gets or sets the categories.</summary>
        /// <value>The categories.</value>
        public HashSet<Category> Categories { get; set; }
    }
}
