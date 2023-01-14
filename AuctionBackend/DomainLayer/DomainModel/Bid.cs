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
        /// Gets or sets the auction.
        /// </summary>
        /// <value>
        /// The auction.
        /// </value>
        [Required]
        public Auction Auction { get; set; }

        /// <summary>
        /// Gets or sets the bidder.
        /// </summary>
        /// <value>
        /// The bidder.
        /// </value>
        [Required]
        public User Bidder { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [Required]
        [Range(0, 2)]
        public Currency Currency { get; set; }
    }
}
