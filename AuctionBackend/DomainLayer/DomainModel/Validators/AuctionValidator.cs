// <copyright file="AuctionValidator.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel.Validators
{
    using System;
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
            this.RuleFor(auction => auction.Product)
                .NotEmpty()
                .WithMessage("The product cannot be null.");

            this.When(auction => auction.Product != null, () => this.RuleFor(auction => auction.Product)
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

            this.When(auction => auction.EndTime != default, () => this.RuleFor(auction => auction.EndTime)
                .GreaterThan(auction => auction.StartTime)
                .WithMessage("End time cannot be lower than start time."));

            this.RuleSet("Add", () =>
            {
                this.RuleFor(auction => auction.StartTime).GreaterThan(DateTime.Now).WithMessage("Start time cannot be in the past.");
                this.RuleFor(auction => auction.IsFinished).Equal(false).WithMessage("A new added auction cannot be already finished.");
            });

            this.RuleSet("Update", () =>
            {
                this.RuleFor(auction => auction.IsFinished).Equal(false).WithMessage("A new added auction cannot be already finished.");

                this.When(auction => auction.StartTime != default, () => this.RuleFor(auction => auction.IsFinished)
                    .Equal(false)
                    .When(auction => auction.StartTime > System.DateTime.Now)
                    .WithMessage("Auction can't be finished if it hadn't started yet."));

                this.When(auction => auction.EndTime != default, () => this.RuleFor(auction => auction.IsFinished)
                    .Equal(true)
                    .When(auction => auction.EndTime < System.DateTime.Now)
                    .WithMessage("Auction must be finished if it is past its end time."));
            });
        }
    }
}
