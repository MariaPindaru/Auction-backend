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

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The validation result.
        /// </returns>
        public override ValidationResult Insert(Product entity)
        {
            if (entity.Auction != null && entity.Auction.Offerer != null && entity.Description != null)
            {
                var offerer = entity.Auction.Offerer;
                var productsWithTheSameOfferer = this.Repository.Get(
                    filter: product => product.Auction.Offerer.Id == offerer.Id,
                    includeProperties: "Auction, User");

                foreach (var previousProduct in productsWithTheSameOfferer)
                {
                    if (LevenshteinDistance.Calculate(previousProduct.Description, entity.Description) > 0)
                    {
                        var errorString = "The product's description is too similar with another product description used for an auction by the same user.";
                        Logger.Error(errorString);

                        IEnumerable<ValidationFailure> failures = new HashSet<ValidationFailure>
                        {
                            new ValidationFailure("Description", errorString),
                        };
                        return new ValidationResult(failures);
                    }
                }
            }

            return base.Insert(entity);
        }
    }
}
