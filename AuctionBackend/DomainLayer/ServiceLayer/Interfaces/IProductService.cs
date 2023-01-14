// <copyright file="IProductService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Interfaces
{
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// IProductService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IService&lt;AuctionBackend.DomainLayer.DomainModel.Product&gt;" />
    public interface IProductService : IService<Product>
    {
        bool ProductHasDuplicateDescription(Product entity);
    }
}
