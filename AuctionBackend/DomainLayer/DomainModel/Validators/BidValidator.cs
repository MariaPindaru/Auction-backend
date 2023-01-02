﻿// <copyright file="BidValidator.cs" company="Transilvania University of Brasov">
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
            this.RuleFor(bid => bid.Bidder).NotNull().WithMessage("The bidder cannot be null."); ;
            this.RuleFor(bid => bid.Price).InclusiveBetween(0.1m, decimal.MaxValue).WithMessage("The price must be in range 0 and decimal max value."); ;
        }
    }
}