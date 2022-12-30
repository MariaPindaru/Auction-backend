using AuctionBackend.DomainLayer.DomainModel;
using AuctionBackend.DomainLayer.DomainModel.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests.DomainLayerTests
{
    class ProductTests
    {
        private Product product;
        private ProductValidator productValidator;

        [SetUp]
        public void Setup()
        {
            this.productValidator = new ProductValidator();
            Category category = new Category
            {
                Name = "Category name"
            };
            this.product = new Product
            {
                Name = "Product name",
                Description = "Product description",
                Categories = new HashSet<Category>{ category }
            };
        }

        [Test]
        public void TestValidProduct()
        {
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void TestNullName()
        {
            this.product.Name = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Name);
        }

        [Test]
        public void TestShortName()
        {
            this.product.Name = "N";
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Name);
        }

        [Test]
        public void TestLongName()
        {
            string longString = new string('*', 101);
            this.product.Name = longString;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Name);
        }

        [Test]
        public void TestNullDescription()
        {
            this.product.Description = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Description);
        }

        [Test]
        public void TestShortDescription()
        {
            this.product.Description = "N";
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Description);
        }

        [Test]
        public void TestLongDescription()
        {
            string longString = new string('*', 501);
            this.product.Description = longString;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Description);
        }

        [Test]
        public void TestNullCategories()
        {
            this.product.Categories = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Categories);
        }

        [Test]
        public void TestEmptyCategories()
        {
            this.product.Categories = new HashSet<Category>();
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Categories);
        }
    }
}
