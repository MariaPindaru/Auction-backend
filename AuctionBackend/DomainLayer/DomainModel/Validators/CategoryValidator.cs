// <copyright file="CategoryValidator.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel.Validators
{
    using FluentValidation;

    /// <summary>
    /// CategoryValidator.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;AuctionBackend.DomainLayer.DomainModel.Category&gt;" />
    public class CategoryValidator : AbstractValidator<Category>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryValidator"/> class.
        /// </summary>
        public CategoryValidator()
        {
            this.RuleFor(category => category.Name).NotEmpty().WithMessage("Catgeory name cannot be null");
            this.RuleFor(category => category.Name)
                .Length(2, 30)
                .WithMessage("The category name must have between 2 and 30 chars");

            //this.RuleForEach(category => category.Parents).SetValidator(new CategoryValidator());
            //this.RuleForEach(category => category.Children).SetValidator(new CategoryValidator());
        }
    }
}
