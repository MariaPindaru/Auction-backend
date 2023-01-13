// <copyright file="AuctionService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.Config;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using FluentValidation.Results;

    /// <summary>
    /// AuctionService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.Auction, AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IAuctionRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IAuctionService" />
    public class AuctionService : BaseService<Auction, IAuctionRepository>, IAuctionService
    {
        private IConfiguration appConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionService"/> class.
        /// </summary>
        public AuctionService()
        : base(Injector.Get<IAuctionRepository>(), new AuctionValidator())
        {
            this.appConfiguration = Injector.Get<IConfiguration>();
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The validation result.
        /// </returns>
        public override ValidationResult Insert(Auction entity)
        {
            IList<ValidationFailure> validationFailures = new List<ValidationFailure>();
            if (entity.Offerer != null)
            {
                int offererId = entity.Offerer.Id;
                var activeAuctionForOfferer = this.GetUserActiveAuctions(offererId).ToList().Count;

                if (activeAuctionForOfferer >= this.appConfiguration.MaxActiveAuctions)
                {
                    validationFailures.Add(new ValidationFailure("Offerer", "The offerer has reached the maximum limit of active auctions for the moment."));
                }
            }

            if (entity.StartTime < DateTime.Now)
            {
                validationFailures.Add(new ValidationFailure("StartTime", "Start time cannot be in the past."));
            }

            if (validationFailures.Count > 0)
            {
                Logger.Error($"The object is not valid. The following errors occurred: {validationFailures}");
                return new ValidationResult(validationFailures);
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
        public override ValidationResult Update(Auction entity)
        {
            IList<ValidationFailure> validationFailures = new List<ValidationFailure>();

            Auction auction = this.Repository.GetByID(entity.Id);
            if (auction is null)
            {
                validationFailures.Add(new ValidationFailure("Id", "The auction's id is not valid so the object cannot be updated."));
            }
            else
            {
                if (auction.IsFinished)
                {
                    validationFailures.Add(new ValidationFailure("Finished", "The auction with has been finished so it cannot be updated."));
                }

                if (entity.StartTime < auction.StartTime)
                {
                    validationFailures.Add(new ValidationFailure("StartTime", "The auction new start time cannot be before the previous start time."));
                }

                if (auction.EndTime < DateTime.Now && entity != auction)
                {
                    Logger.Info("The end time for the auction has been past so only the 'IsFinished' field will be updated into the database.");
                }

                if (validationFailures.Count > 0)
                {
                    Logger.Error($"The object is not valid. The following errors occurred: {validationFailures}");
                    return new ValidationResult(validationFailures);
                }
            }

            if (validationFailures.Count > 0)
            {
                Logger.Error($"The object is not valid. The following errors occurred: {validationFailures}");
                return new ValidationResult(validationFailures);
            }

            return base.Update(entity);
        }

        /// <summary>
        /// Gets the user auctions.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Collection of auctions which have as offerer the given user.
        /// </returns>
        public IEnumerable<Auction> GetUserActiveAuctions(int userId)
        {
            return this.Repository.Get(
                filter: auction => auction.Offerer.Id == userId &&
                                   auction.StartTime < DateTime.Now &&
                                   auction.EndTime > DateTime.Now &&
                                   !auction.IsFinished,
                includeProperties: "User");
        }
    }
}
