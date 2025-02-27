﻿// <copyright file="BidValidator.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel.Validators
{
    using System.Linq;
    using FluentValidation;

    /// <summary>
    /// Validator for entity of type Bid.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;AuctionBackend.DomainLayer.DomainModel.Bid&gt;" />
    public class BidValidator : AbstractValidator<Bid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BidValidator"/> class.
        /// </summary>
        public BidValidator()
        {
            this.RuleFor(bid => bid.Auction)
                .NotEmpty()
                .WithMessage("The auction cannot be null.");

            this.When(
                bid => bid.Auction != null,
                () => this.RuleFor(bid => bid.Auction)
                .SetValidator(new AuctionValidator()));

            this.RuleFor(bid => bid.Bidder)
                .NotEmpty()
                .WithMessage("The bidder cannot be null.");

            this.When(
                bid => bid.Bidder != null,
                () => this.RuleFor(bid => bid.Bidder)
                .SetValidator(new UserValidator()));

            this.When(
                bid => bid.Bidder != null,
                () => this.RuleFor(bid => bid.Bidder)
                .Must(bidder => bidder.Role.HasFlag(Role.Bidder))
                .WithMessage("The bidder must have the bidder role."));

            this.RuleFor(bid => bid.Currency)
                .IsInEnum()
                .WithMessage("The currency must be within the defined enum.");

            this.When(
                bid => bid.Auction != null,
                () => this.RuleFor(bid => new { BidCurrency = bid.Currency, AuctionCurrency = bid.Auction.Currency })
                        .Must(x => x.BidCurrency == x.AuctionCurrency)
                        .WithMessage("The currency must be the same as it is defined in auction."));

            this.When(
                bid => bid.Auction != null && bid.Auction.BidHistory.Count > 0,
                () => this.RuleFor(bid =>
                        new { bidPrice = bid.Price, lastAuctionBidPrice = bid.Auction.BidHistory.Last().Price })
                        .Must(prices => prices.bidPrice < prices.lastAuctionBidPrice * 3)
                        .WithMessage("The bid price cannot be mode than 300% higher than the previous one."));

            this.When(
                bid => bid.Auction != null && bid.Auction.BidHistory.Count > 0,
                () => this.RuleFor(bid =>
                        new { bidPrice = bid.Price, lastAuctionBidPrice = bid.Auction.BidHistory.Last().Price })
                        .Must(prices => prices.bidPrice > prices.lastAuctionBidPrice)
                        .WithMessage("The bid price must be higher than the previous one."));

            this.When(
                bid => bid.Auction != null && bid.Auction.BidHistory.Count == 0,
                () => this.RuleFor(bid =>
                    new { bidPrice = bid.Price, lastAuctionBidPrice = bid.Auction.StartPrice })
                    .Must(prices => prices.bidPrice < prices.lastAuctionBidPrice * 3)
                    .WithMessage("The bid price cannot be mode than 300% higher than the previous one."));

            this.When(
                bid => bid.Auction != null && bid.Auction.BidHistory.Count == 0,
                () => this.RuleFor(bid =>
                        new { bidPrice = bid.Price, lastAuctionBidPrice = bid.Auction.StartPrice })
                        .Must(prices => prices.bidPrice > prices.lastAuctionBidPrice)
                        .WithMessage("The bid price must be higher than the previous one."));
        }
    }
}
