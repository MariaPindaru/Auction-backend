using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DomainLayer.DomainModel
{
    class Category
    {
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "Catgeory name cannot be null", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "The category name must have between 2 and 30 chars")]
        public string Name { get; set; }

        public HashSet<Category> Parents { get; set; }

        public HashSet<Product> Products { get; set; }
    }
}
