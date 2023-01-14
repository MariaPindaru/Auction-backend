// <copyright file="ProductValidator.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel.Validators
{
    using FluentValidation;

    /// <summary>
    /// ProductValidator.
    /// </summary>
    public class ProductValidator : AbstractValidator<Product>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductValidator"/> class.
        /// </summary>
        public ProductValidator()
        {
            this.RuleFor(product => product.Name).NotEmpty().WithMessage("Product name cannot be null");
            this.RuleFor(product => product.Name).Length(2, 100).WithMessage("The product name must have between 2 and 100 chars");

            this.RuleFor(product => product.Description).NotEmpty().WithMessage("Product description cannot be null");
            this.RuleFor(product => product.Description).Length(3, 500).WithMessage("The product description must have between 3 and 500 chars");

            this.RuleFor(product => product.Category).NotEmpty().WithMessage("The product category cannot be null.");
            this.When(product => product.Category != null,
                () => RuleFor(product => product.Category)
                .SetValidator(new CategoryValidator()));

            this.RuleFor(product => product.Offerer)
                .NotEmpty()
                .WithMessage("The offerer cannot be null.");

            this.When(product => product.Offerer != null,
                () => RuleFor(product => product.Offerer).SetValidator(new UserValidator()));

            this.When(product => product.Offerer != null,
                () => RuleFor(product => product.Offerer)
                .Must(offerer => offerer.Role.HasFlag(Role.Offerer))
                .WithMessage("The offerer must have the role of offerer."));
        }
    }
}
