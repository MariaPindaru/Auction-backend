﻿// <copyright file="User.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Enum used to define the user's role.
    ///   <br />
    /// </summary>
    [Flags]
    public enum Role
    {
        /// <summary>The offerer.</summary>
        Offerer = 1,

        /// <summary>The bidder.</summary>
        Bidder = 2,

        /// <summary>
        /// Both offerer and bidder.
        /// </summary>
        OffererAndBidder = Offerer | Bidder,
    }

    /// <summary>Class used to define a user.</summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            this.BidHistory = new HashSet<Bid>();
            this.ReceivedUserScores = new HashSet<UserScore>();
            this.GivenUserScores = new HashSet<UserScore>();
            this.Products = new HashSet<Product>();
        }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>Gets or sets the role.</summary>
        /// <value>The role.</value>
        [Required]
        public Role Role { get; set; }

        /// <summary>
        /// Gets or sets the bid history.
        /// </summary>
        /// <value>
        /// The bid history.
        /// </value>
        public virtual ICollection<Bid> BidHistory { get; set; }

        /// <summary>
        /// Gets or sets the received user scores.
        /// </summary>
        /// <value>
        /// The received user scores.
        /// </value>
        public virtual ICollection<UserScore> ReceivedUserScores { get; set; }

        /// <summary>
        /// Gets or sets the given user scores.
        /// </summary>
        /// <value>
        /// The given user scores.
        /// </value>
        public virtual ICollection<UserScore> GivenUserScores { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Product> Products { get; set; }
    }
}
