// <copyright file="IConfiguration.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.Config
{
    /// <summary>
    /// Defines the interface for the application dynamic configuration.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the maximum active auctions.
        /// </summary>
        /// <value>
        /// The maximum active auctions.
        /// </value>
        int MaxActiveAuctions { get; }

        /// <summary>
        /// Gets the default score.
        /// </summary>
        /// <value>
        /// The default score.
        /// </value>
        int DefaultScore { get; }

        /// <summary>
        /// Gets the minimum score.
        /// </summary>
        /// <value>
        /// The minimum score.
        /// </value>
        int MinimumScore { get; }

        /// <summary>
        /// Gets the suspension days.
        /// </summary>
        /// <value>
        /// The suspension days.
        /// </value>
        int SuspensionDays { get; }
    }
}
