// <copyright file="BidRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Impl
{
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// BidRepository.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Impl.BaseRepository&lt;AuctionBackend.DomainLayer.DomainModel.Bid&gt;" />
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IBidRepository" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal class BidRepository : BaseRepository<Bid>, IBidRepository
    {
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Insert(Bid entity)
        {
            using (var ctx = new AppDbContext())
            {
                User bidder = ctx.Set<User>().Find(entity.Bidder.Id);
                if (bidder != null)
                {
                    entity.Bidder = bidder;
                }

                Auction auction = ctx.Set<Auction>().Find(entity.Auction.Id);
                if (auction != null)
                {
                    entity.Auction = auction;
                }

                ctx.Set<Bid>().Add(entity);

                ctx.SaveChanges();
            }
        }
    }
}
