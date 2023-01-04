// <copyright file="UserValidator.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel.Validators
{
    using FluentValidation;

    /// <summary>
    /// UserValidator.
    /// </summary>
    public class UserValidator : AbstractValidator<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidator"/> class.
        /// </summary>
        public UserValidator()
        {
            this.ClassLevelCascadeMode = CascadeMode.Stop;

            this.RuleFor(user => user.Name).NotEmpty().WithMessage("User name cannot be null");
            this.RuleFor(user => user.Name).Length(2, 50).WithMessage("The user's name must have between 2 and 50 chars");

            this.RuleFor(user => user.Role).IsInEnum().WithMessage("The role must be within the Role enum.");

            this.RuleFor(user => user.Score).InclusiveBetween(0.0f, 100.0f).WithMessage("The score must be in range 0 to 100.");
        }
    }
}
