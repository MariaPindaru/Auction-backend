using NUnit.Framework;

namespace UnitTests.ModelTests
{
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using FluentValidation.TestHelper;

    class CategoryTests
    {
        private Category category;
        private CategoryValidator categoryValidator;

        [SetUp]
        public void Setup()
        {
            this.categoryValidator = new CategoryValidator();

            this.category = new Category
            {
                Name = "Electronice",
            };
        }

        [Test]
        public void TestValidCategory()
        {
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void TestNullName()
        {
            this.category.Name = null;
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldHaveValidationErrorFor(category => category.Name);
        }

        [Test]
        public void TestShortName()
        {
            this.category.Name = "e";
            TestValidationResult<Category> result = this.categoryValidator.TestValidate(this.category);
            result.ShouldHaveValidationErrorFor(category => category.Name);
        }

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
