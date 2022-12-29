// <copyright file="Category.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace Auction.DomainLayer.DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>Class used to define a category.</summary>
    public class Category
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "Catgeory name cannot be null", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "The category name must have between 2 and 30 chars")]
        public string Name { get; set; }

        /// <summary>Gets or sets the parents.</summary>
        /// <value>The parents.</value>
        public HashSet<Category> Parents { get; set; }

        /// <summary>Gets or sets the products.</summary>
        /// <value>The products.</value>
        public HashSet<Product> Products { get; set; }
    }
}
