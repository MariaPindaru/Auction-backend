// <copyright file="Bid.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class used to define a bid.
    /// </summary>
    public class Bid
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the bidder.
        /// </summary>
        /// <value>
        /// The bidder.
        /// </value>
        public User Bidder { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }
    }
}
