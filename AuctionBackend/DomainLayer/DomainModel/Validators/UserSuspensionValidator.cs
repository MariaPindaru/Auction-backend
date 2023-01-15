// <copyright file="UserSuspensionValidator.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel.Validators
{
    using FluentValidation;

    /// <summary>
    /// UserSuspensionValidator.
    /// </summary>
    public class UserSuspensionValidator : AbstractValidator<UserSuspension>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSuspensionValidator"/> class.
        /// </summary>
        public UserSuspensionValidator()
        {
            this.RuleFor(us => us.User).NotNull().WithMessage("The user cannot be null");
            this.When(
                us => us.User != null,
                () => this.RuleFor(us => us.User)
                .SetValidator(new UserValidator()));

            this.RuleFor(us => us.StartDate).NotEmpty().WithMessage("The start date must be a valid date.");

            this.RuleFor(us => us.EndDate).NotEmpty().WithMessage("The end date must be a valid date.");
            this.When(
                 us => us.EndDate != default && us.StartDate != default,
                 () => this.RuleFor(us => us.EndDate)
                 .GreaterThan(us => us.StartDate));

            this.RuleSet("Add", () => this.When(
                                us => us.StartDate != default,
                                () => this.RuleFor(us => us.StartDate)
                                .GreaterThan(System.DateTime.Now)
                                .WithMessage("The start time cannot be in the past")));
        }
    }
}
