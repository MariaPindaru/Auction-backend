// <copyright file="AuctionRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Impl
{
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Impl.BaseRepository&lt;AuctionBackend.DomainLayer.DomainModel.Auction&gt;" />
    /// <seealso cref="AuctionBackend.DataLayer.DAL.Interfaces.IAuctionRepository" />
    public class AuctionRepository : BaseRepository<Auction>, IAuctionRepository
    {
    }
}
