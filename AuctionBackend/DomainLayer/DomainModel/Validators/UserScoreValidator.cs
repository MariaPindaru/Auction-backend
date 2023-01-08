// <copyright file="UserScoreValidator.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel.Validators
{
    using FluentValidation;

    /// <summary>
    /// UserScoreValidator.
    /// </summary>
    /// <seealso cref="AbstractValidator&lt;AuctionBackend.DomainLayer.DomainModel.Auction&gt;" />
    public class UserScoreValidator : AbstractValidator<UserScore>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserScoreValidator"/> class.
        /// </summary>
        public UserScoreValidator()
        {
            this.ClassLevelCascadeMode = CascadeMode.Stop;

            this.RuleFor(userScore => userScore.ScoringUser)
                .NotEmpty()
                .WithMessage("The scoring user cannot be null.");

            this.RuleFor(userScore => userScore.ScoringUser)
                .SetValidator(new UserValidator());

            this.RuleFor(userScore => userScore.ScoredUser)
                .NotEmpty()
                .WithMessage("The scored user cannot be null.");

            this.RuleFor(userScore => userScore.ScoredUser)
                .SetValidator(new UserValidator());

            this.RuleFor(userScore => userScore.Score).InclusiveBetween(1, 10).WithMessage("The score must be in range 1 to 10.");
        }
    }
}
