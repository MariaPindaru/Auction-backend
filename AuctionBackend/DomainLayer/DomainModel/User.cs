// <copyright file="User.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Enum used to define the user's role.
    ///   <br />
    /// </summary>
    [Flags]
    public enum Role
    {
        /// <summary>The offerer.</summary>
        Offerer,

        /// <summary>The bidder.</summary>
        Bidder,
    }

    /// <summary>Class used to define a user.</summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            this.StartedAuctions = new HashSet<Auction>();
            this.ParticipantAuctions = new HashSet<Auction>();
        }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>Gets or sets the role.</summary>
        /// <value>The role.</value>
        [Required]
        [Range(0, 1)]
        public Role Role { get; set; }

        /// <summary>Gets or sets the score.</summary>
        /// <value>The score.</value>
        [Required]
        [Range(0.0f, 100.0f)]
        public float Score { get; set; }

        /// <summary>Gets or sets the started auctions.</summary>
        /// <value>The started auctions.</value>
        public ICollection<Auction> StartedAuctions { get; set; }

        /// <summary>Gets or sets the participant auctions.</summary>
        /// <value>The participant auctions.</value>
        public ICollection<Auction> ParticipantAuctions { get; set; }
    }
}
