// <copyright file="Product.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>Class used to define a poduct.</summary>
    public class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            this.Categories = new HashSet<Category>();
        }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }

        /// <summary>Gets or sets the categories.</summary>
        /// <value>The categories.</value>
        [Required]
        public virtual ICollection<Category> Categories { get; set; }
    }
}
