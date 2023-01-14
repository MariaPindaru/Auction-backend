﻿// <copyright file="ProductService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using System.Collections.Generic;
    using System.Linq;
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
            if (this.DescriptionIsDuplicate(entity))
            {
                IList<ValidationFailure> validationFailures = new List<ValidationFailure>();
                validationFailures.Add(new ValidationFailure("Description", "The product's description is too similar with another product description used for an auction by the same user."));
                Logger.Error($"The object is not valid. The following errors occurred: {validationFailures}");
                return new ValidationResult(validationFailures);
            }

            return base.Insert(entity);
        }

        /// <summary>
        /// Descriptions the is duplicate.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns> True if the product owner has another product with a very similar description, false otherwise.</returns>
        private bool DescriptionIsDuplicate(Product entity)
        {
            if (entity.Offerer != null && entity.Description != null)
            {
                var offerer = entity.Offerer;
                var productsWithTheSameOfferer = this.Repository.Get(
                    filter: product => product.Offerer.Id == offerer.Id,
                    includeProperties: "Auction, User");

                foreach (var previousProduct in productsWithTheSameOfferer)
                {
                    var distance = LevenshteinDistance.Calculate(previousProduct.Description, entity.Description);
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
