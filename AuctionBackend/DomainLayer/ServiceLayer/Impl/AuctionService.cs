﻿// <copyright file="AuctionService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using System;
    using System.Collections.Generic;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using FluentValidation.Results;
    using log4net;

    /// <summary>
    /// AuctionService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.Auction, AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IAuctionRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IAuctionService" />
    public class AuctionService : BaseService<Auction, IAuctionRepository>, IAuctionService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionService"/> class.
        /// </summary>
        public AuctionService()
        : base(Injector.Get<IAuctionRepository>(), new AuctionValidator())
        {
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The validation result.
        /// </returns>
        public new ValidationResult Insert(Auction entity)
        {
            if (entity.StartTime < DateTime.Now)
            {
                IEnumerable<ValidationFailure> failures = new HashSet<ValidationFailure>
                {
                    new ValidationFailure("StartTime", "Start time cannot be in the past."),
                };
                return new ValidationResult(failures);
            }

            return base.Insert(entity);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The validation result.
        /// </returns>
        public new ValidationResult Update(Auction entity)
        {
            Auction auction = this.Repository.GetByID(entity.Id);
            if (auction is null)
            {
                IEnumerable<ValidationFailure> failures = new HashSet<ValidationFailure>
                {
                    new ValidationFailure("Id", "The auction's id is not valid so the object cannot be updated."),
                };
                return new ValidationResult(failures);
            }

            return base.Update(entity);
        }
    }
}
