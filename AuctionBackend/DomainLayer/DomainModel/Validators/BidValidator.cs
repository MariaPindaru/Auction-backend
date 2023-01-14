// <copyright file="BidValidator.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel.Validators
{
    using FluentValidation;

    /// <summary>
    /// BidValidator.
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

            this.When(bid => bid.Auction != null,
                () => RuleFor(bid => bid.Auction)
                .SetValidator(new AuctionValidator()));

            this.RuleFor(bid => bid.Bidder)
                .NotEmpty()
                .WithMessage("The bidder cannot be null.");

            this.When(bid => bid.Bidder != null,
                () => RuleFor(bid => bid.Bidder)
                .SetValidator(new UserValidator()));

            this.When(bid => bid.Bidder != null,
                () => RuleFor(bid => bid.Bidder)
                .Must(bidder => bidder.Role.HasFlag(Role.Bidder))
                .WithMessage("The bidder must have the bidder role."));

            this.RuleFor(bid => bid.Currency)
                .IsInEnum()
                .WithMessage("The currency must be within the defined enum.");

            this.RuleFor(bid => bid.Price)
                .InclusiveBetween(0.1m, decimal.MaxValue)
                .WithMessage("The price must be in range 0 and decimal max value.");

            this.When(bid => bid.Auction != null,
                () => RuleFor(bid => new { BidCurrency = bid.Currency, AuctionCurrency = bid.Auction.Currency })
                        .Must(x => x.BidCurrency == x.AuctionCurrency)
                        .WithMessage("The currency must be the same as it is defined in auction."));
        }
    }
}
