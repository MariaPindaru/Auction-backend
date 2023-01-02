// <copyright file="Bid.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class used to define a bid.
    /// </summary>
    public class Bid
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets the auction.
        /// </summary>
        /// <value>
        /// The auction.
        /// </value>
        public Auction Auction { get; }

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
    }
}
