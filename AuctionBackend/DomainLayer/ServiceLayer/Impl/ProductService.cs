// <copyright file="ProductService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using System.Collections.Generic;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.DomainLayer.ServiceLayer.Utils;
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

        public IEnumerable<Product> GetProductsForOfferer(User offerer)
        {
            return this.Repository.Get(filter: product => product.Offerer.Id == offerer.Id);
        }

        /// <summary>
        /// Descriptions the is duplicate.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns> True if the product owner has another product with a very similar description, false otherwise.</returns>
        public bool ProductHasDuplicateDescription(Product entity)
        {
            if (entity.Offerer != null && entity.Description != null)
            {
                var productsWithTheSameOfferer = this.GetProductsForOfferer(entity.Offerer);
                var newProductDescription = entity.Description;
                foreach (var previousProduct in productsWithTheSameOfferer)
                {
                    var oldProductDescription = previousProduct.Description;

                    var distance = LevenshteinDistance.Calculate(oldProductDescription, newProductDescription);
                    if (distance < previousProduct.Description.Length / 3)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
