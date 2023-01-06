// <copyright file="ProductService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using FluentValidation.Results;

    /// <summary>
    /// ProductService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.Product, AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IProductRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IProductService" />
    public class ProductService : BaseService<Product, IProductRepository>, IProductService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        public ProductService()
        : base(Injector.Get<IProductRepository>(), new ProductValidator())
        {
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The validation result.
        /// </returns>
        public new ValidationResult Insert(Product entity)
        {
            // TODO: validate description to user's..................
            return base.Insert(entity);
        }
    }
}
