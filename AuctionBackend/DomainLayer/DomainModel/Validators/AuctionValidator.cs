// <copyright file="AuctionValidator.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel.Validators
{
    using FluentValidation;

    /// <summary>
    /// AuctionValidator.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;AuctionBackend.DomainLayer.DomainModel.Auction&gt;" />
    public class AuctionValidator : AbstractValidator<Auction>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionValidator"/> class.
        /// </summary>
        public AuctionValidator()
        {
            this.RuleFor(auction => auction.Offerer).NotNull().WithMessage("The offerer cannot be null.");
            this.RuleFor(auction => auction.Product).NotNull().WithMessage("The product cannot be null.");
            this.RuleFor(auction => auction.StartPrice).ExclusiveBetween(0.0m, decimal.MaxValue).WithMessage("The product price must be in range (0, decimal.MaxValue).");
            this.RuleFor(auction => auction.Currency).IsInEnum().WithMessage("The currency must be within the defined enum.");
            this.RuleFor(auction => auction.StartTime).NotEmpty().WithMessage("Start time must be specified.");
            this.RuleFor(auction => auction.EndTime).NotEmpty().WithMessage("End time must be specified.");
        }
    }
}
