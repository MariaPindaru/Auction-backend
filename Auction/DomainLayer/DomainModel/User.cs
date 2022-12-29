// <copyright file="User.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace Auction.DomainLayer.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Enum used to define the user's role.
    ///   <br />
    /// </summary>
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
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "User's name cannot be null", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The user's name must have between 2 and 50 chars")]
        public string Name { get; set; }

        /// <summary>Gets or sets the role.</summary>
        /// <value>The role.</value>
        [Required]
        [Range(0, 1)]
        public Role Role { get; set; }

        /// <summary>Gets the score.</summary>
        /// <value>The score.</value>
        [Required]
        [Range(0.0f, 100.0f)]
        public float Score { get; }

        /// <summary>Gets or sets the started auctions.</summary>
        /// <value>The started auctions.</value>
        public HashSet<Auction> StartedAuctions { get; set; }

        /// <summary>Gets or sets the participant auctions.</summary>
        /// <value>The participant auctions.</value>
        public HashSet<Auction> ParticipantAuctions { get; set; }
    }
}
