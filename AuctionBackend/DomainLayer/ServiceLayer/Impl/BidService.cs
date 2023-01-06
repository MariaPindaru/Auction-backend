// <copyright file="BidService.cs" company="Transilvania University of Brasov">
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
    using System.Collections.Generic;

    /// <summary>
    /// BidService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.Bid, AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IBidRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IBidService" />
    public class BidService : BaseService<Bid, IBidRepository>, IBidService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BidService"/> class.
        /// </summary>
        public BidService()
        : base(Injector.Get<IBidRepository>(), new BidValidator())
        {
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The validation result.
        /// </returns>
        public new ValidationResult Insert(Bid entity)
        {
            Auction auction = entity.Auction;
            if (auction != null && auction.BidHistory != null && auction.BidHistory.Count > 0)
            {
                if (auction.BidHistory[auction.BidHistory.Count - 1].Price == entity.Price)
                {
                    IEnumerable<ValidationFailure> failures = new HashSet<ValidationFailure>
                    {
                        new ValidationFailure("Price", "The bid price must be higher than the previous one."),
                    };
                    return new ValidationResult(failures);
                }

                if (3 * auction.BidHistory[auction.BidHistory.Count - 1].Price < entity.Price)
                {
                    IEnumerable<ValidationFailure> failures = new HashSet<ValidationFailure>
                    {
                        new ValidationFailure("Price", "The bid price cannot be mode than 300% higher than the previous one."),
                    };
                    return new ValidationResult(failures);
                }
            }

            return base.Insert(entity);
        }
    }
}
