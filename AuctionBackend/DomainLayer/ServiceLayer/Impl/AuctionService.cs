// <copyright file="AuctionService.cs" company="Transilvania University of Brasov">
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
    using log4net.Repository.Hierarchy;

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
                var errorString = "Start time cannot be in the past.";
                Logger.Error(errorString);

                IEnumerable<ValidationFailure> failures = new HashSet<ValidationFailure>
                {
                    new ValidationFailure("StartTime", errorString),
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
                var errorString = "The auction's id is not valid so the object cannot be updated.";
                Logger.Error(errorString);

                IEnumerable<ValidationFailure> failures = new HashSet<ValidationFailure>
                {
                    new ValidationFailure("Id", errorString),
                };
                return new ValidationResult(failures);
            }

            if (auction.IsFinished)
            {
                var errorString = "The auction has been finished so it cannot be updated.";
                Logger.Error(errorString);

                IEnumerable<ValidationFailure> failures = new HashSet<ValidationFailure>
                {
                    new ValidationFailure("Finished", errorString),
                };
                return new ValidationResult(failures);
            }

            if (auction.EndTime < DateTime.Now && !auction.IsFinished && entity != auction)
            {
                Logger.Info("The end time for the auction has been past so only the 'IsFinished' field will be updated into the database.");
            }

            return base.Update(entity);
        }
    }
}
