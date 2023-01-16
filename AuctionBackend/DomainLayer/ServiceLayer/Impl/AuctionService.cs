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
    using AuctionBackend.DomainLayer.ServiceLayer.Utils;
    using AuctionBackend.Startup;
    using FluentValidation.Results;

    /// <summary>
    /// Class that implements the functionalities for IAuctionService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.Auction, AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IAuctionRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IAuctionService" />
    internal class AuctionService : BaseService<Auction, IAuctionRepository>, IAuctionService
    {
        /// <summary>
        /// The application configuration.
        /// </summary>
        private IConfiguration appConfiguration;

        /// <summary>
        /// The user suspension service.
        /// </summary>
        private IUserSuspensionService userSuspensionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionService"/> class.
        /// </summary>
        public AuctionService()
        : base(Injector.Get<IAuctionRepository>(), new AuctionValidator())
        {
            this.userSuspensionService = Injector.Get<IUserSuspensionService>();
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
            if (entity.Product != null && entity.Product.Offerer != null)
            {
                int offererId = entity.Product.Offerer.Id;
                var activeAuctionForOfferer = this.GetUserActiveAuctions(offererId).ToList();

                if (this.userSuspensionService.UserIsSuspended(entity.Product.Offerer))
                {
                    validationFailures.Add(new ValidationFailure("Offerer", "The offerer is suspended so the auction cannot be added."));
                }
                else if (activeAuctionForOfferer.Count >= this.appConfiguration.MaxActiveAuctions)
                {
                    validationFailures.Add(new ValidationFailure("Offerer", "The offerer has reached the maximum limit of active auctions for the moment."));
                }
                else if (this.ProductHasDuplicateDescription(entity.Product, activeAuctionForOfferer))
                {
                    validationFailures.Add(new ValidationFailure("Product", "The product has a very similar description with another one used by the same user. The description must be changed in order to be added in an auction."));
                }

                if (validationFailures.Count > 0)
                {
                    Logger.Error($"The object is not valid. The following errors occurred: {validationFailures}");
                    return new ValidationResult(validationFailures);
                }
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
                    validationFailures.Add(new ValidationFailure(nameof(Auction.IsFinished), "The auction with has been finished so it cannot be updated."));
                }
                else if (auction.BidHistory.Count > 0 && auction.BidHistory.ElementAt(0).Currency != entity.Currency)
                {
                    validationFailures.Add(new ValidationFailure(nameof(Auction.Currency), "The currency of an auction cannot be change because it already has bids."));
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
                filter: auction => auction.Product.Offerer.Id == userId &&
                                   auction.StartTime < DateTime.Now &&
                                   auction.EndTime > DateTime.Now &&
                                   !auction.IsFinished,
                includeProperties: "Product, Product.Offerer");
        }

        /// <summary>
        /// Products the has duplicate description.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="auctions">The auctions.</param>
        /// <returns> True if there are any similar product descriptions, false otherwise. </returns>
        public bool ProductHasDuplicateDescription(Product entity, IEnumerable<Auction> auctions)
        {
            if (entity.Offerer != null && entity.Description != null)
            {
                var newProductDescription = this.GetDescriptionWithoutUnusefulData(entity.Description);

                foreach (var auction in auctions)
                {
                    var oldProductDescription = this.GetDescriptionWithoutUnusefulData(auction.Product.Description);

                    var distance = LevenshteinDistance.Calculate(oldProductDescription, newProductDescription);
                    if (distance < oldProductDescription.Length / 3)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the description without useless data.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns> The description containing only lowercase letters.</returns>
        private string GetDescriptionWithoutUnusefulData(string description)
        {
            char[] onlyLetters = description.Where(c => char.IsLetter(c)).ToArray();
            description = new string(onlyLetters);
            description = description.ToLower();
            return description;
        }
    }
}
