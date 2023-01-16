// <copyright file="IAuctionService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Interfaces
{
    using System.Collections.Generic;
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// Interface used for defining the functionalities for a service for the entity Auction.
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

        /// <summary>
        /// Products the has duplicate description.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="activeAuctions">The active auctions.</param>
        /// <returns>true if there is another very similar description of a product used by the same user, false otherwise.</returns>
        bool ProductHasDuplicateDescription(Product entity, IEnumerable<Auction> activeAuctions);
    }
}
