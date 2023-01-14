// <copyright file="ProductTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ModelTests
{
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using System;
    using System.Linq;

    /// <summary>
    /// ProductTests.
    /// </summary>
    internal class ProductTests
    {
        /// <summary>
        /// The product.
        /// </summary>
        private Product product;

        /// <summary>
        /// The product validator.
        /// </summary>
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
            User offerer = new User
            {
                Role = Role.Offerer,
                Name = "Offerer",
            };
            this.product = new Product
            {
                Name = "Product name",
                Description = "Product description",
                Category = category,
                Offerer = offerer,
            };
        }

        /// <summary>
        /// Tests the valid product.
        /// </summary>
        [Test]
        public void TestValidation_ValidProduct_ReturnsNoErrors()
        {
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the null name.
        /// </summary>
        [Test]
        public void TestValidation_HasNullName_ReturnsErrorForName()
        {
            this.product.Name = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Name);
        }

        /// <summary>
        /// Tests the short name.
        /// </summary>
        [Test]
        public void TestValidation_HasNameTooShort_ReturnsErrorForName()
        {
            this.product.Name = "N";
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Name);
        }

        /// <summary>
        /// Tests the long name.
        /// </summary>
        [Test]
        public void TestValidation_HasNameTooLong_ReturnsErrorForName()
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
        public void TestValidation_HasNullDescription_ReturnsErrorForDescription()
        {
            this.product.Description = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Description);
        }

        /// <summary>
        /// Tests the short description.
        /// </summary>
        [Test]
        public void TestValidation_HasDescriptionTooShort_ReturnsErrorForDescription()
        {
            this.product.Description = "N";
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Description);
        }

        /// <summary>
        /// Tests the long description.
        /// </summary>
        [Test]
        public void TestValidation_HasDescriptionTooLong_ReturnsErrorForDescription()
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
        public void TestValidation_HasNullCategory_ReturnsErrorForCategory()
        {
            this.product.Category = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Category);
        }

        /// <summary>
        /// Tests the invalid category.
        /// </summary>
        [Test]
        public void TestValidation_CategoryHasNullName_ReturnsErrorForCategoryName()
        {
            this.product.Category.Name = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Category.Name);
        }

        /// <summary>
        /// Tests the null offerer.
        /// </summary>
        [Test]
        public void TestValidation_HasNullOfferer_ReturnsErrorForOfferer()
        {
            this.product.Offerer = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Offerer);
        }

        /// <summary>
        /// Tests the invalid offerer.
        /// </summary>
        [Test]
        public void TestValidation_OffererHasNullName_ReturnsErrorForOffererName()
        {
            this.product.Offerer.Name = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Offerer.Name);
        }

        /// <summary>
        /// Tests the invalid offerer role.
        /// </summary>
        [Test]
        public void TestValidation_OffererHasWrongRole_ReturnsErrorForOfferer()
        {
            this.product.Offerer.Role = Role.Bidder;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Offerer);
        }

        /// <summary>
        /// Tests the offerer with both roles.
        /// </summary>
        [Test]
        public void TestValidation_OffererHasBothRoles_ReturnsNoErrors()
        {
            this.product.Offerer.Role = Role.Bidder | Role.Offerer;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the validation offerer has invalid role returns error for offerer role.
        /// </summary>
        [Test]
        public void TestValidation_OffererHasInvalidRole_ReturnsErrorForOffererRole()
        {
            var invalidRoleValue = Enum.GetValues(typeof(Role)).Cast<int>().Max() + 1;
            this.product.Offerer.Role = (Role)invalidRoleValue;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Offerer.Role);
        }

    }
}
