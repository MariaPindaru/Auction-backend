// <copyright file="IConfiguration.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.Config
{
    /// <summary>
    /// IConfiguration.
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
    }
}
