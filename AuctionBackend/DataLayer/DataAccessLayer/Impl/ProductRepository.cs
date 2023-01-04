// <copyright file="ProductRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Impl
{
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// ProductRepository.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Impl.BaseRepository&lt;AuctionBackend.DomainLayer.DomainModel.Product&gt;" />
    /// <seealso cref="AuctionBackend.DataLayer.DAL.Interfaces.IProductRepository" />
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Insert(Product entity)
        {
            using (var ctx = new AppDbContext())
            {
                entity.Category = ctx.Set<Category>().Find(entity.Category.Id);

                ctx.Set<Product>().Add(entity);

                ctx.SaveChanges();
            }
        }
    }
}
