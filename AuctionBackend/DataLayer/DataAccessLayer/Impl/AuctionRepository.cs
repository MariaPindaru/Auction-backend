// <copyright file="AuctionRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DAL
{
    using AuctionBackend.DataLayer.DAL.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// AuctionRepository.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DAL.BaseRepository&lt;AuctionBackend.DomainLayer.DomainModel.Auction&gt;" />
    /// <seealso cref="AuctionBackend.DataLayer.DAL.Interfaces.IAuctionRepository" />
    public class AuctionRepository : BaseRepository<Auction>, IAuctionRepository
    {
    }
}
