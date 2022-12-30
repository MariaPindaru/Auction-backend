// <copyright file="Category.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>Class used to define a category.</summary>
    public class Category
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        public Category()
        {
            this.Products = new HashSet<Product>();
            this.Parents = new HashSet<Category>();
        }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>Gets or sets the parents.</summary>
        /// <value>The parents.</value>
        public HashSet<Category> Parents { get; set; }

        /// <summary>Gets or sets the products.</summary>
        /// <value>The products.</value>
        public HashSet<Product> Products { get; set; }
    }
}
