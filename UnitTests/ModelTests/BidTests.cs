using AuctionBackend.DomainLayer.DomainModel;
using AuctionBackend.DomainLayer.DomainModel.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace UnitTests.ModelTests
{
    class BidTests
    {
        private Bid bid;
        private BidValidator bidValidator;

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
                Currency = Currency.Dolar
            };
        }

        [Test]
        public void TestValidBid()
        {
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void TestNullBidder()
        {
            this.bid.Bidder = null;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => bid.Bidder);
        }

        [Test]
        public void TestInvalidBidder()
        {
            this.bid.Bidder.Name = null;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => bid.Bidder.Name);
        }

        [Test]
        public void TestNegativePrice()
        {
            this.bid.Price = -10.5m;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => bid.Price);
        }

        [Test]
        public void TestInvalidCurrency()
        {
            this.bid.Currency = (Currency)90;
            TestValidationResult<Bid> result = this.bidValidator.TestValidate(this.bid);
            result.ShouldHaveValidationErrorFor(bid => bid.Currency);
        }
    }
}
