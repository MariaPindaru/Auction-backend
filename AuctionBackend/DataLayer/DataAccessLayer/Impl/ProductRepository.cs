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
    }
}
