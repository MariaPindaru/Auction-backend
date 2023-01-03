using AuctionBackend.DomainLayer.DomainModel;
using AuctionBackend.DomainLayer.DomainModel.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace UnitTests.ModelTests
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
                Category = category 
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
        public void TestNullCategory()
        {
            this.product.Category = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Category);
        }

        [Test]
        public void TestInvalidCategory()
        {
            this.product.Category.Name = null;
            TestValidationResult<Product> result = this.productValidator.TestValidate(this.product);
            result.ShouldHaveValidationErrorFor(product => product.Category.Name);
        }
    }
}
