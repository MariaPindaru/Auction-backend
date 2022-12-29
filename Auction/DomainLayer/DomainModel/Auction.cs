// <copyright file="Auction.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace Auction.DomainLayer.DomainModel
{
    using System;

    /// <summary>Class used to define an auction.</summary>
    internal class Auction
    {
        /// <summary>Initializes a new instance of the <see cref="Auction" /> class.</summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="product">The product.</param>
        /// <param name="initialPrice">The initial price.</param>
        public Auction(DateTime startTime, DateTime endTime, Product product, double initialPrice)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.IsFinished = false;

            this.Product = product;
            this.PriceHistory = new PriceHistory(initialPrice);
        }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the offerer.</summary>
        /// <value>The offerer.</value>
        public User Offerer { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        public Product Product { get; set; }

        /// <summary>Gets the start time.</summary>
        /// <value>The start time.</value>
        public DateTime StartTime { get; }

        /// <summary>Gets the end time.</summary>
        /// <value>The end time.</value>
        public DateTime EndTime { get; }

        /// <summary>Gets the price history.</summary>
        /// <value>The price history.</value>
        public PriceHistory PriceHistory { get; }

        /// <summary>Gets or sets a value indicating whether this instance is finished.</summary>
        /// <value>
        ///   <c>true</c> if this instance is finished; otherwise, <c>false</c>.</value>
        public bool IsFinished { get; set; }
    }
}
