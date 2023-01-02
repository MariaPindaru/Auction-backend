using AuctionBackend.DomainLayer.DomainModel;
using AuctionBackend.DomainLayer.DomainModel.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ModelTests
{
    class BidTests
    {
        private Bid bid;
        private User bidder;
        private BidValidator bidValidator;

        [SetUp]
        public void Setup()
        {
            this.bidValidator = new BidValidator();

            this.bidder = new User
            {
                Name = "username",
            };

            this.bid = new Bid
            {
                Bidder = this.bidder,
                Price = 10.3m
            };
        }
    }
}
