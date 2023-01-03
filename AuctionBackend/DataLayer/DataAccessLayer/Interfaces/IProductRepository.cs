// <copyright file="IProductRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Interfaces
{
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// IProductRepository.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IRepository&lt;AuctionBackend.DomainLayer.DomainModel.Product&gt;" />
    public interface IProductRepository : IRepository<Product>
    {
    }
}
