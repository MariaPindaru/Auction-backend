// <copyright file="AuctionRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Impl
{
    using System.Diagnostics.CodeAnalysis;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// AuctionRepository.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Impl.BaseRepository&lt;AuctionBackend.DomainLayer.DomainModel.Auction&gt;" />
    /// <seealso cref="AuctionBackend.DataLayer.DAL.Interfaces.IAuctionRepository" />
    [ExcludeFromCodeCoverage]
    internal class AuctionRepository : BaseRepository<Auction>, IAuctionRepository
    {
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Insert(Auction entity)
        {
            using (var ctx = new AppDbContext())
            {
                Product product = ctx.Set<Product>().Find(entity.Product.Id);
                Category category = ctx.Set<Category>().Find(entity.Product.Category.Id);
                User user = ctx.Set<User>().Find(entity.Product.Offerer.Id);


                if (product != null)
                {
                    entity.Product = product;
                }

                if (category != null)
                {
                    entity.Product.Category = category;
                }

                if (user != null)
                {
                    entity.Product.Offerer = user;
                }

                ctx.Set<Auction>().Add(entity);

                ctx.SaveChanges();
            }
        }
    }
}
