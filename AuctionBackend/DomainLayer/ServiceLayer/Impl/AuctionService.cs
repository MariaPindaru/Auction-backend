// <copyright file="AuctionService.cs" company="Transilvania University of Brasov">
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
    using log4net;
    using System.Collections.Generic;

    /// <summary>
    /// AuctionService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.Auction, AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IAuctionRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IAuctionService" />
    public class AuctionService : BaseService<Auction, IAuctionRepository>, IAuctionService
    {
        private static readonly ILog Logger = LogManager.GetLogger("AuctionService");

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionService"/> class.
        /// </summary>
        public AuctionService()
        : base(Injector.Get<IAuctionRepository>(), new AuctionValidator())
        {
        }

        /// <summary>
        /// Adds the bid.
        /// </summary>
        /// <param name="auction">The auction.</param>
        /// <param name="bid">The bid.</param>
        /// <returns>
        /// The validation result.
        /// </returns>
        public bool AddBid(Auction auction, Bid bid)
        {
            var result = this.validator.Validate(auction);
            if (!result.IsValid)
            {
                IList<ValidationFailure> failures = result.Errors;
                Logger.Error($"The auction in which a bid was tried to be added was not valid. The following errors occurred: {failures}");
                return false;
            }

            var bidValidator = new BidValidator();
            result = bidValidator.Validate(bid);
            if (!result.IsValid)
            {
                IList<ValidationFailure> failures = result.Errors;
                Logger.Error($"The bid to be added in auction with the id {auction.Id} was invalid. The following errors occurred: {failures}");
                return false;
            }

            return true;
        }
    }
}
