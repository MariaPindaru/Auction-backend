﻿// <copyright file="BidService.cs" company="Transilvania University of Brasov">
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
    using AuctionBackend.Startup;
    using FluentValidation.Results;

    /// <summary>
    /// BidService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.Bid, AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IBidRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IBidService" />
    public class BidService : BaseService<Bid, IBidRepository>, IBidService
    {
        private IAuctionService auctionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BidService"/> class.
        /// </summary>
        public BidService()
        : base(Injector.Get<IBidRepository>(), new BidValidator())
        {
            this.auctionService = Injector.Get<IAuctionService>();
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The validation result.
        /// </returns>
        public override ValidationResult Insert(Bid entity)
        {
            IList<ValidationFailure> validationFailures = new List<ValidationFailure>();

            Auction auction = entity.Auction;
            if (auction != null)
            {
                if (this.auctionService.GetByID(auction.Id) == null)
                {
                    validationFailures.Add(new ValidationFailure("Auction", "The auction doesn't exist, the bid cannot be added."));
                }
                else if (auction.BidHistory != null)
                {
                    var lastPrice = auction.BidHistory.Count == 0 ? auction.StartPrice : auction.BidHistory[auction.BidHistory.Count - 1].Price;

                    if (lastPrice == entity.Price)
                    {
                        validationFailures.Add(new ValidationFailure("Price", "The bid price must be higher than the previous one."));
                    }
                    else if (3 * lastPrice < entity.Price)
                    {
                        validationFailures.Add(new ValidationFailure("Price", "The bid price cannot be mode than 300% higher than the previous one."));
                    }
                }
            }

            if (validationFailures.Count > 0)
            {
                Logger.Error($"The object is not valid. The following errors occurred: {validationFailures}");
                return new ValidationResult(validationFailures);
            }

            return base.Insert(entity);
        }
    }
}
