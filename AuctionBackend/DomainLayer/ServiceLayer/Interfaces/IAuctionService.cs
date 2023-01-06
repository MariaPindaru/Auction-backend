// <copyright file="IAuctionService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Interfaces
{
    using AuctionBackend.DomainLayer.DomainModel;
    using System.Collections.Generic;

    /// <summary>
    /// IAuctionService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IService&lt;AuctionBackend.DomainLayer.DomainModel.Auction&gt;" />
    public interface IAuctionService : IService<Auction>
    {
        /// <summary>
        /// Gets the auctions with their bid history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Collection of aucctions including their bidding history.
        /// </returns>
        /*Auction GetAuctionWithTheirBidHistory(int id);*/
    }
}
