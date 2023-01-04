// <copyright file="BidService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;

    /// <summary>
    /// BidService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.Bid, AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IBidRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IBidService" />
    public class BidService : BaseService<Bid, IBidRepository>, IBidService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BidService"/> class.
        /// </summary>
        public BidService()
        : base(Injector.Get<IBidRepository>(), new BidValidator())
        {
        }
    }
}
