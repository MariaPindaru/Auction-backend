// <copyright file="IAuctionRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Interfaces
{
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// Interface used for defining the functionalities of a repository for the entity Auction.
    /// </summary>
    /// <seealso cref="IRepository&lt;AuctionBackend.DomainLayer.DomainModel.Auction&gt;" />
    public interface IAuctionRepository : IRepository<Auction>
    {
    }
}
