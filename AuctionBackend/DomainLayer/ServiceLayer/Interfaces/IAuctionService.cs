// <copyright file="IAuctionService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Interfaces
{
    using System.Collections.Generic;
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// IAuctionService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IService&lt;AuctionBackend.DomainLayer.DomainModel.Auction&gt;" />
    public interface IAuctionService : IService<Auction>
    {
        /// <summary>
        /// Gets the user active auctions.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns> Collection of active auctions which have as offerer the given user. </returns>
        IEnumerable<Auction> GetUserActiveAuctions(int userId);
    }
}
