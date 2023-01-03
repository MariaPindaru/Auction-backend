// <copyright file="ProductTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ModelTests
{
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    /// <summary>
    /// ProductTests.
    /// </summary>
    internal class ProductTests
    {
        private Product product;
        private ProductValidator productValidator;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.productValidator = new ProductValidator();
            Category category = new Category
            {
                Name = "Category name",
            };
            this.product = new Product
            {
                Name = "Product name",
                Description = "Product description",
                Category = category,
            };
        }

        /// <summary>
        /// Tests the valid product.
        /// </summary>
        [Test]
        public void TestValidProduct()
        {
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the null name.
        /// </summary>
        [Test]
        public void TestNullName()
        {
            this.product.Name = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Name);
        }

        /// <summary>
        /// Tests the short name.
        /// </summary>
        [Test]
        public void TestShortName()
        {
            this.product.Name = "N";
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Name);
        }

        /// <summary>
        /// Tests the long name.
        /// </summary>
        [Test]
        public void TestLongName()
        {
            string longString = new string('*', 101);
            this.product.Name = longString;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Name);
        }

        /// <summary>
        /// Tests the null description.
        /// </summary>
        [Test]
        public void TestNullDescription()
        {
            this.product.Description = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Description);
        }

        /// <summary>
        /// Tests the short description.
        /// </summary>
        [Test]
        public void TestShortDescription()
        {
            this.product.Description = "N";
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Description);
        }

        /// <summary>
        /// Tests the long description.
        /// </summary>
        [Test]
        public void TestLongDescription()
        {
            string longString = new string('*', 501);
            this.product.Description = longString;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Description);
        }

        /// <summary>
        /// Tests the null category.
        /// </summary>
        [Test]
        public void TestNullCategory()
        {
            this.product.Category = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Category);
        }

        /// <summary>
        /// Tests the invalid category.
        /// </summary>
        [Test]
        public void TestInvalidCategory()
        {
            this.product.Category.Name = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Category.Name);
        }
    }
}
