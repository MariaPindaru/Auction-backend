// <copyright file="Category.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>Class used to define a category.</summary>
    public class Category
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        public Category()
        {
            this.Products = new HashSet<Product>();
            this.Children = new HashSet<Category>();
        }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>Gets or sets the children.</summary>
        /// <value>The parents.</value>
        public virtual ICollection<Category> Children { get; set; }

        /// <summary>Gets or sets the products.</summary>
        /// <value>The products.</value>
        public virtual ICollection<Product> Products { get; set; }
    }
}
