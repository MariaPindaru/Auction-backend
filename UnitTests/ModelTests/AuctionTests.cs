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
        private Auction auction;
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
                Score = 30.43f,
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
            };

            this.auction = new Auction
            {
                Offerer = offrer,
                Product = product,
                Currency = Currency.Euro,
                StartPrice = 10.5m,
                StartTime = new DateTime(2022, 12, 10),
                EndTime = new DateTime(2022, 12, 13),
            };
        }

        /// <summary>
        /// Tests the valid auction.
        /// </summary>
        [Test]
        public void TestValidAuction()
        {
            TestValidationResult<Auction> result = this.auctionValidator.TestValidate(this.auction);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the null offerer.
        /// </summary>
        [Test]
        public void TestNullOfferer()
        {
            this.auction.Offerer = null;
            TestValidationResult<Auction> result = this.auctionValidator.TestValidate(this.auction);
            result.ShouldHaveValidationErrorFor(auction => auction.Offerer);
        }

        /// <summary>
        /// Tests the null product.
        /// </summary>
        [Test]
        public void TestNullProduct()
        {
            this.auction.Product = null;
            TestValidationResult<Auction> result = this.auctionValidator.TestValidate(this.auction);
            result.ShouldHaveValidationErrorFor(auction => auction.Product);
        }

        /// <summary>
        /// Tests the negative start price.
        /// </summary>
        [Test]
        public void TestNegativeStartPrice()
        {
            this.auction.StartPrice = -1.7m;
            TestValidationResult<Auction> result = this.auctionValidator.TestValidate(this.auction);
            result.ShouldHaveValidationErrorFor(auction => auction.StartPrice);
        }

        /// <summary>
        /// Tests the invalid currency.
        /// </summary>
        [Test]
        public void TestInvalidCurrency()
        {
            this.auction.Currency = (Currency)300;
            TestValidationResult<Auction> result = this.auctionValidator.TestValidate(this.auction);
            result.ShouldHaveValidationErrorFor(auction => auction.Currency);
        }

        /// <summary>
        /// Tests the add valid bid.
        /// </summary>
        [Test]
        public void TestAddValidBid()
        {
            var bidder = new User
            {
                Name = "Bidder",
                Role = Role.Bidder,
                Score = 30.43f,
            };
            this.auction.AddToBidHistory(ref bidder, price: 11.5m);
            Assert.AreEqual(this.auction.GetLastPrice(), 11.5m);
        }

        /// <summary>
        /// Tests the adition of a bid with a price too high.
        /// </summary>
        [Test]
        public void TestAddTooHighBid()
        {
            var bidder = new User
            {
                Name = "Bidder",
                Role = Role.Bidder,
                Score = 30.43f,
            };

            var ex = Assert.Throws<Exception>(() => this.auction.AddToBidHistory(ref bidder, price: 10000.5m));
            Assert.That(ex.Message, Is.EqualTo("The price can't be 300% bigger than last price."));

            Assert.AreEqual(this.auction.GetLastPrice(), 10.5m);
        }

        /// <summary>
        /// Tests the add lower bid.
        /// </summary>
        [Test]
        public void TestAddLowerBid()
        {
            var bidder = new User
            {
                Name = "Bidder",
                Role = Role.Bidder,
                Score = 30.43f,
            };

            var ex = Assert.Throws<Exception>(() => this.auction.AddToBidHistory(ref bidder, price: 9.5m));
            Assert.That(ex.Message, Is.EqualTo("The price can't be lower than last price."));

            Assert.AreEqual(this.auction.GetLastPrice(), 10.5m);
        }

        /// <summary>
        /// Tests the add bid with offerer role.
        /// </summary>
        [Test]
        public void TestAddBidWithOffererRole()
        {
            var bidder = new User
            {
                Name = "Bidder",
                Role = Role.Offerer,
                Score = 30.43f,
            };

            var ex = Assert.Throws<Exception>(() => this.auction.AddToBidHistory(ref bidder, price: 12.5m));
            Assert.That(ex.Message, Is.EqualTo("The bidder must have the role Bidder."));

            Assert.AreEqual(this.auction.GetLastPrice(), 10.5m);
        }
    }
}
