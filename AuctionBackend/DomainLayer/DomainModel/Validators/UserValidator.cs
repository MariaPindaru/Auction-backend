// <copyright file="UserValidator.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel.Validators
{
    using FluentValidation;

    /// <summary>
    /// Validator for entity of type User.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;AuctionBackend.DomainLayer.DomainModel.User&gt;" />
    public class UserValidator : AbstractValidator<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidator"/> class.
        /// </summary>
        public UserValidator()
        {
            this.RuleFor(user => user.Name).NotEmpty().WithMessage("User name cannot be null");
            this.When(
                user => user.Name != null,
                () => this.RuleFor(user => user.Name)
                     .Length(2, 50)
                     .WithMessage("The user's name must have between 2 and 50 chars"));
            this.RuleFor(user => user.Role).IsInEnum().WithMessage("The role must be within the Role enum.");
        }
    }
}
