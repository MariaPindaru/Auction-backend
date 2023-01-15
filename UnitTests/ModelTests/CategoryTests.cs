// <copyright file="CategoryTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ModelTests
{
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    /// <summary>
    /// CategoryTests.
    /// </summary>
    internal class CategoryTests
    {
        /// <summary>
        /// The category.
        /// </summary>
        private Category category;

        /// <summary>
        /// The category validator.
        /// </summary>
        private CategoryValidator categoryValidator;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.categoryValidator = new CategoryValidator();

            this.category = new Category
            {
                Name = "Electronice",
            };
        }

        /// <summary>
        /// Tests the valid category.
        /// </summary>
        [Test]
        public void TestValidation_ValidCategory_returnsNoErrors()
        {
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the category one parent.
        /// </summary>
        [Test]
        public void TestValidation_HasNoParents_ReturnsNoErrors()
        {
            this.category.Parents = null;
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Validations the category has one parents no errors.
        /// </summary>
        [Test]
        public void Validation_HasOneParents_ReturnsNoErrors()
        {
            this.category.Parents.Add(new Category { Name = "Parent" });
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the validation when has multiple parents returns no errors.
        /// </summary>
        [Test]
        public void TestValidation_HasMultipleParents_ReturnsNoErrors()
        {
            this.category.Parents.Add(new Category { Name = "Parent" });
            this.category.Parents.Add(new Category { Name = "Parent" });
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the validation has no children no errors.
        /// </summary>
        [Test]
        public void TestValidation_HasNoChildren_ReturnsNoErrors()
        {
            this.category.Children = null;
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Validations the category has one parents no errors.
        /// </summary>
        [Test]
        public void TestValidation_HasOneChilde_ReturnsNoErrors()
        {
            this.category.Children.Add(new Category { Name = "Child" });
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the validation when has multiple parents returns no errors.
        /// </summary>
        [Test]
        public void TestValidation_HasMultipleChildren_ReturnsNoErrors()
        {
            this.category.Children.Add(new Category { Name = "Child" });
            this.category.Children.Add(new Category { Name = "Child" });
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the name of the null.
        /// </summary>
        [Test]
        public void TestValidation_HasNullName_ReturnsErrorForName()
        {
            this.category.Name = null;
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldHaveValidationErrorFor(category => category.Name);
        }

        /// <summary>
        /// Tests the short name.
        /// </summary>
        [Test]
        public void TestValidation_HasNameTooShot_ReturnsErrorForName()
        {
            this.category.Name = "e";
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldHaveValidationErrorFor(category => category.Name);
        }

        /// <summary>
        /// Tests the long name.
        /// </summary>
        [Test]
        public void TestValidation_HasNameTooLong_ReturnsErrorForName()
        {
            string longString = new string('*', 31);
            this.category.Name = longString;
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldHaveValidationErrorFor(category => category.Name);
        }

        /// <summary>
        /// Tests the validation has no products returns no error.
        /// </summary>
        [Test]
        public void TestValidation_HasNoProducts_ReturnsNoError()
        {
            this.category.Products = null;
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the validation has one product returns no error.
        /// </summary>
        [Test]
        public void TestValidation_HasOneProduct_ReturnsNoError()
        {
            this.category.Products.Add(new Product());
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the validation has two products returns no error.
        /// </summary>
        [Test]
        public void TestValidation_HasTwoProducts_ReturnsNoError()
        {
            this.category.Products.Add(new Product());
            this.category.Products.Add(new Product());
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
