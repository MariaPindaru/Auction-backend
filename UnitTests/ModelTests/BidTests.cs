// <copyright file="BidTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ModelTests
{
    using System;
    using System.Linq;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    /// <summary>
    /// BidTests.
    /// </summary>
    internal class BidTests
    {
        /// <summary>
        /// The bid.
        /// </summary>
        private Bid bid;

        /// <summary>
        /// The bid validator.
        /// </summary>
        private BidValidator bidValidator;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.bidValidator = new BidValidator();

            var auction = new Auction
            {
                Currency = Currency.Dolar,
                StartPrice = 10.3m,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(5),
                Product = new Product
                {
                    Name = "product",
                    Description = "the product description",
                    Category = new Category
                    {
                        Name = "category",
                    },
                    Offerer = new User
                    {
                        Name = "offerer",
                        Role = Role.Offerer,
                    },
                },
            };

            var bidder = new User
            {
                Name = "username",
                Role = Role.Bidder,
            };

            this.bid = new Bid
            {
                Bidder = bidder,
                Price = 10.3m,
                Currency = Currency.Dolar,
                Auction = auction,
            };
        }

        /// <summary>
        /// Tests the valid bid.
        /// </summary>
        [Test]
        public void TestValidation_ValidBid_ReturnsNoErrors()
        {
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the null bidder.
        /// </summary>
        [Test]
        public void TestValidation_HasNullBidder_ReturnsErrorForBidder()
        {
            this.bid.Bidder = null;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => bid.Bidder);
        }

        /// <summary>
        /// Tests the invalid bidder.
        /// </summary>
        [Test]
        public void TestValidation_BidderHasNullName_ReturnsErrorForBidderName()
        {
            this.bid.Bidder.Name = null;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => bid.Bidder.Name);
        }

        /// <summary>
        /// Tests the invalid role bidder.
        /// </summary>
        [Test]
        public void TestValidation_HasOffererRole_ReturnsErrorForBidder()
        {
            this.bid.Bidder.Role = Role.Offerer;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => bid.Bidder);
        }

        /// <summary>
        /// Tests the bidder with both roles.
        /// </summary>
        [Test]
        public void TestValidation_HasBothRoles_ReturnsNoError()
        {
            this.bid.Bidder.Role = Role.Offerer | Role.Bidder;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the negative price.
        /// </summary>
        [Test]
        public void TestValidation_HasNegativePrice_ReturnsErrorForPrice()
        {
            this.bid.Price = -10.5m;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => bid.Price);
        }

        /// <summary>
        /// Tests the invalid currency.
        /// </summary>
        [Test]
        public void TestValidation_HasInvalidCurrency_ReturnsErrorForCurrency()
        {
            var invalidRoleValue = Enum.GetValues(typeof(Role)).Cast<int>().Max() + 1;
            this.bid.Currency = (Currency)invalidRoleValue;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => bid.Currency);
        }

        /// <summary>
        /// Tests the different currency than auction.
        /// </summary>
        [Test]
        public void TestValidation_HasDifferentCurrencyThanAuction_ReturnsErrorForCurrency()
        {
            this.bid.Currency = Currency.Euro;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => new { BidCurrency = bid.Currency, AuctionCurrency = bid.Auction.Currency });
        }
    }
}
