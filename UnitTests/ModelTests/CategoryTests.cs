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
        public void TestValidCategory()
        {
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the name of the null.
        /// </summary>
        [Test]
        public void TestNullName()
        {
            this.category.Name = null;
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldHaveValidationErrorFor(category => category.Name);
        }

        /// <summary>
        /// Tests the short name.
        /// </summary>
        [Test]
        public void TestShortName()
        {
            this.category.Name = "e";
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldHaveValidationErrorFor(category => category.Name);
        }

        /// <summary>
        /// Tests the long name.
        /// </summary>
        [Test]
        public void TestLongName()
        {
            string longString = new string('*', 31);
            this.category.Name = longString;
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldHaveValidationErrorFor(category => category.Name);
        }
    }
}
