﻿// <copyright file="AuctionValidator.cs" company="Transilvania University of Brasov">
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
            this.RuleFor(auction => auction.Offerer)
                .NotEmpty()
                .WithMessage("The offerer cannot be null.");

            this.When(auction => auction.Offerer != null,
                () => RuleFor(auction => auction.Offerer).SetValidator(new UserValidator()));

            this.When(auction => auction.Offerer != null,
                () => RuleFor(auction => auction.Offerer)
                .Must(offerer => offerer.Role.HasFlag(Role.Offerer))
                .WithMessage("The offerer must have the role of offerer."));

            this.RuleFor(auction => auction.Product)
                .NotEmpty()
                .WithMessage("The product cannot be null.");

            this.When(auction => auction.Offerer != null,
                () => RuleFor(auction => auction.Product)
                .SetValidator(new ProductValidator()));

            this.RuleFor(auction => auction.StartPrice)
                .ExclusiveBetween(0.0m, decimal.MaxValue)
                .WithMessage("The product price must be in range (0, decimal.MaxValue).");

            this.RuleFor(auction => auction.Currency)
                .IsInEnum()
                .WithMessage("The currency must be within the defined enum.");

            this.RuleFor(auction => auction.StartTime)
                .NotEmpty()
                .WithMessage("Start time must be specified.");

            this.RuleFor(auction => auction.EndTime)
                .NotEmpty()
                .WithMessage("End time must be specified.");

            this.RuleFor(auction => auction.EndTime)
                .GreaterThan(auction => auction.StartTime)
                .WithMessage("End time cannot be lower than start time.");

            this.RuleFor(auction => auction.IsFinished)
                .Equal(false)
                .When(auction => auction.StartTime > System.DateTime.Now)
                .WithMessage("Auction can't be finished if it hadn't started yet.");

            this.RuleFor(auction => auction.IsFinished)
                .Equal(true)
                .When(auction => auction.EndTime < System.DateTime.Now)
                .WithMessage("Auction must be finished if it is past its end time.");
        }
    }
}
