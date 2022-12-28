using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DomainLayer.DomainModel
{
    class Product
    {
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "Catgeory name cannot be null", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "The category name must have between 2 and 30 chars")]
        public string Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [Required(ErrorMessage = "The description must be present")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The category name must be between 3 and 130 chars")]
        public string Description { get; set; }

        public HashSet<Category> Categories { get; set; }
    }
}
