﻿// <copyright file="Auction.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Currency enum.
    /// </summary>
    public enum Currency
    {
        /// <summary>
        /// The euro.
        /// </summary>
        Euro,

        /// <summary>
        /// The dolar.
        /// </summary>
        Dolar,

        /// <summary>
        /// The ron.
        /// </summary>
        Ron,
    }

    /// <summary> Class used to define an entity Auction.</summary>
    public class Auction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Auction" /> class.
        /// </summary>
        public Auction()
        {
            this.IsFinished = false;
            this.BidHistory = new List<Bid>();
        }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        [Required]
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [Required]
        [Range(0, 2)]
        public Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the start price.
        /// </summary>
        /// <value>
        /// The start price.
        /// </value>
        [Required]
        [Range(0.0, double.PositiveInfinity)]
        public decimal StartPrice { get; set; }

        /// <summary>Gets or sets the start time.</summary>
        /// <value>The start time.</value>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>Gets or sets the end time.</summary>
        /// <value>The end time.</value>
        [Required]
        public DateTime EndTime { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is finished.</summary>
        /// <value>
        /// true if this instance is finished; otherwise, false.</value>
        public bool IsFinished { get; set; }

        /// <summary>
        /// Gets or sets the bid history.
        /// </summary>
        /// <value>
        /// The bid history.
        /// </value>
        public virtual ICollection<Bid> BidHistory { get; set; }
    }
}
