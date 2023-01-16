// <copyright file="BidService.cs" company="Transilvania University of Brasov">
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
    /// Class that implements the functionalities for IBidService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.Bid, AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IBidRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IBidService" />
    internal class BidService : BaseService<Bid, IBidRepository>, IBidService
    {
        /// <summary>
        /// The auction service.
        /// </summary>
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
            Auction auction = entity.Auction;
            if (auction != null && this.auctionService.GetByID(auction.Id) == null)
            {
                IList<ValidationFailure> validationFailures = new List<ValidationFailure>();
                validationFailures.Add(new ValidationFailure("Auction", "The auction doesn't exist, the bid cannot be added."));
                Logger.Error($"The object is not valid. The following errors occurred: {validationFailures}");
                return new ValidationResult(validationFailures);
            }

            return base.Insert(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The validation result.
        /// </returns>
        public override ValidationResult Update(Bid entity)
        {
            IList<ValidationFailure> validationFailures = new List<ValidationFailure>();

            Bid oldBid = this.Repository.GetByID(entity.Id);
            if (oldBid == null)
            {
                validationFailures.Add(new ValidationFailure("Id", "The bid doesn't exist."));
            }
            else
            {
                var auction = oldBid.Auction;
                var oldBidIndex = auction.BidHistory.ToList().IndexOf(oldBid);

                if (oldBidIndex < auction.BidHistory.Count() - 1)
                {
                    validationFailures.Add(new ValidationFailure("Id", "The bid is not the last one for this auction so it cannot be updated."));
                }
            }

            if (validationFailures.Count > 0)
            {
                Logger.Error($"The object is not valid. The following errors occurred: {validationFailures}");
                return new ValidationResult(validationFailures);
            }

            return base.Update(entity);
        }
    }
}
