// <copyright file="PriceHistory.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace Auction.DomainLayer.DomainModel
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>Class used to define an auction's price history.</summary>
    public class PriceHistory
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>The history</summary>
        private readonly HashSet<double> history;

        /// <summary>Initializes a new instance of the <see cref="PriceHistory" /> class.</summary>
        /// <param name="initialPrice">The initial price.</param>
        public PriceHistory(double initialPrice)
        {
            this.history = new HashSet<double>();
            this.history.Add(initialPrice);
        }

        /// <summary>Adds to history.</summary>
        /// <param name="price">The price.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public bool AddToHistory(double price)
        {
            if (price > 3 * this.history.Last())
            {
                return false;
            }

            this.history.Add(price);
            return true;
        }
    }
}
