// <copyright file="AuctionTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ModelTests
{
    using System;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    /// <summary>
    /// AuctionTests.
    /// </summary>
    internal class AuctionTests
    {
        /// <summary>
        /// The auction.
        /// </summary>
        private Auction auction;

        /// <summary>
        /// The auction validator.
        /// </summary>
        private AuctionValidator auctionValidator;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.auctionValidator = new AuctionValidator();

            var offrer = new User
            {
                Name = "Offerer",
                Role = Role.Offerer,
            };
            var category = new Category
            {
                Name = "Category name",
            };
            var product = new Product
            {
                Name = "Product name",
                Description = "Product description",
                Category = category,
                Offerer = offrer,
            };

            this.auction = new Auction
            {
                IsFinished = false,
                Product = product,
                Currency = Currency.Euro,
                StartPrice = 10.5m,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(5),
            };
        }

        /// <summary>
        /// Tests the valid auction.
        /// </summary>
        [Test]
        public void TestValidation_ValidAuction_ReturnsNoErrors()
        {
            TestValidationResult<Auction> result = this.auctionValidator.TestValidate(this.auction);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the null product.
        /// </summary>
        [Test]
        public void TestValidation_HasNullProduct_ReturnsErrorForProduct()
        {
            this.auction.Product = null;
            TestValidationResult<Auction> result = this.auctionValidator.TestValidate(this.auction);
            result.ShouldHaveValidationErrorFor(auction => auction.Product);
        }

        /// <summary>
        /// Tests the invalid product.
        /// </summary>
        [Test]
        public void TestValidation_ProductHasNullName_ReturnsErrorForProductName()
        {
            this.auction.Product.Name = null;
            TestValidationResult<Auction> result = this.auctionValidator.TestValidate(this.auction);
            result.ShouldHaveValidationErrorFor(auction => auction.Product.Name);
        }

        /// <summary>
        /// Tests the negative start price.
        /// </summary>
        [Test]
        public void TestValidation_HasNegativeStartPrice_ReturnsErrorForStartPrice()
        {
            this.auction.StartPrice = -1.7m;
            TestValidationResult<Auction> result = this.auctionValidator.TestValidate(this.auction);
            result.ShouldHaveValidationErrorFor(auction => auction.StartPrice);
        }

        /// <summary>
        /// Tests the invalid currency.
        /// </summary>
        [Test]
        public void TestValidation_HasInvalidCurrency_ReturnsErrorForCurrency()
        {
            this.auction.Currency = (Currency)300;
            TestValidationResult<Auction> result = this.auctionValidator.TestValidate(this.auction);
            result.ShouldHaveValidationErrorFor(auction => auction.Currency);
        }

        /// <summary>
        /// Tests the start time is default.
        /// </summary>
        [Test]
        public void TestValidation_StartTimeIsDefault_ReturnsErrorForStartTime()
        {
            this.auction.StartTime = default;
            this.auction.EndTime = DateTime.Now.AddDays(10);
            TestValidationResult<Auction> result = this.auctionValidator.TestValidate(this.auction);
            result.ShouldHaveValidationErrorFor(auction => auction.StartTime);
        }

        /// <summary>
        /// Tests the end time is default.
        /// </summary>
        [Test]
        public void TestValidation_EndTimeIsDefault_ReturnsErrorForEndTime()
        {
            this.auction.StartTime = DateTime.Now.AddDays(10);
            this.auction.EndTime = default;
            TestValidationResult<Auction> result = this.auctionValidator.TestValidate(this.auction);
            result.ShouldHaveValidationErrorFor(auction => auction.EndTime);
        }

        /// <summary>
        /// Tests the start time after end time.
        /// </summary>
        [Test]
        public void TestValidation_StartTimeIsAfterEndTime_ReturnsErrorForEndTime()
        {
            this.auction.StartTime = DateTime.Now.AddDays(10);
            this.auction.EndTime = DateTime.Now.AddDays(2);
            TestValidationResult<Auction> result = this.auctionValidator.TestValidate(this.auction);
            result.ShouldHaveValidationErrorFor(auction => auction.EndTime);
        }

        /// <summary>
        /// Tests the is finished is false after end date.
        /// </summary>
        [Test]
        public void TestValidation_IsFinishedFalseAfterEndTimePassed_ReturnsErrorForIsFinished()
        {
            this.auction.StartTime = DateTime.Now.AddDays(-2);
            this.auction.EndTime = DateTime.Now.AddDays(-1);
            this.auction.IsFinished = false;
            this.auction.Id = 12;
            TestValidationResult<Auction> result = this.auctionValidator
                .TestValidate(this.auction, options => options.IncludeRuleSets("default", "Update"));
            result.ShouldHaveValidationErrorFor(auction => auction.IsFinished);
        }

        /// <summary>
        /// Tests the is finished is true before start date.
        /// </summary>
        [Test]
        public void TestValidation_IsFinishedBeforeStartTimePassed_ReturnsErrorForIsfinished()
        {
            this.auction.StartTime = DateTime.Now.AddDays(2);
            this.auction.EndTime = DateTime.Now.AddDays(10);
            this.auction.IsFinished = true;
            TestValidationResult<Auction> result = this.auctionValidator
                .TestValidate(this.auction, options => options.IncludeRuleSets("default", "Update"));
            result.ShouldHaveValidationErrorFor(auction => auction.IsFinished);
        }
    }
}
