// <copyright file="IAuctionService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Interfaces
{
    using AuctionBackend.DomainLayer.DomainModel;
    using FluentValidation.Results;

    /// <summary>
    /// IAuctionService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IService&lt;AuctionBackend.DomainLayer.DomainModel.Auction&gt;" />
    public interface IAuctionService : IService<Auction>
    {
        /// <summary>
        /// Adds the bid.
        /// </summary>
        /// <param name="auction">The auction.</param>
        /// <param name="bid">The bid.</param>
        /// <returns>
        /// The validation result.
        /// </returns>
        bool AddBid(Auction auction, Bid bid);
    }
}
