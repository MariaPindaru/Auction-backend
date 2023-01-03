// <copyright file="BidTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ModelTests
{
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

            var bidder = new User
            {
                Name = "username",
            };

            this.bid = new Bid
            {
                Bidder = bidder,
                Price = 10.3m,
                Currency = Currency.Dolar,
            };
        }

        /// <summary>
        /// Tests the valid bid.
        /// </summary>
        [Test]
        public void TestValidBid()
        {
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the null bidder.
        /// </summary>
        [Test]
        public void TestNullBidder()
        {
            this.bid.Bidder = null;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => bid.Bidder);
        }

        /// <summary>
        /// Tests the invalid bidder.
        /// </summary>
        [Test]
        public void TestInvalidBidder()
        {
            this.bid.Bidder.Name = null;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => bid.Bidder.Name);
        }

        /// <summary>
        /// Tests the negative price.
        /// </summary>
        [Test]
        public void TestNegativePrice()
        {
            this.bid.Price = -10.5m;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => bid.Price);
        }

        /// <summary>
        /// Tests the invalid currency.
        /// </summary>
        [Test]
        public void TestInvalidCurrency()
        {
            this.bid.Currency = (Currency)90;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => bid.Currency);
        }
    }
}
