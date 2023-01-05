﻿// <copyright file="BidServiceTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using FluentValidation.Results;
    using Ninject;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// AuctionServiceTests.
    /// </summary>
    internal class BidServiceTests
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// The bid service.
        /// </summary>
        private IBidService bidService;

        /// <summary>
        /// The bid repository.
        /// </summary>
        private IBidRepository bidRepository;

        /// <summary>
        /// The mocks.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// The bid.
        /// </summary>
        private Bid bid;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Injector.Inject();
            this.kernel = Injector.Kernel;

            this.mocks = new MockRepository();
            this.bidRepository = this.mocks.StrictMock<IBidRepository>();

            this.kernel.Rebind<IBidRepository>().ToConstant(this.bidRepository);
            this.bidService = this.kernel.Get<IBidService>();

            var auction = new Auction
            {
                Currency = Currency.Dolar,
                StartPrice = 10.3m,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(5),
                Offerer = new User
                {
                    Name = "offerer",
                    Role = Role.Offerer,
                },
                Product = new Product
                {
                    Name = "product",
                    Description = "the product description",
                    Category = new Category
                    {
                        Name = "category",
                    },
                },
            };

            var bidder = new User
            {
                Name = "bidder",
                Role = Role.Bidder,
            };

            this.bid = new Bid
            {
                Id = 0,
                Bidder = bidder,
                Price = 20.3m,
                Currency = Currency.Dolar,
                Auction = auction,
            };
        }

        /// <summary>
        /// Tests the add valid bid.
        /// </summary>
        [Test]
        public void TestAddValidBid()
        {
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with null bidder.
        /// </summary>
        [Test]
        public void TestAddBidWithNullBidder()
        {
            this.bid.Bidder = null;
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with invalid bidder.
        /// </summary>
        [Test]
        public void TestAddBidWithInvalidBidder()
        {
            this.bid.Bidder.Name = null;
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with negative price.
        /// </summary>
        [Test]
        public void TestAddBidWithNegativePrice()
        {
            this.bid.Price = -19.3m;
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with different currency than auction.
        /// </summary>
        [Test]
        public void TestAddBidWithDifferentCurrencyThanAuction()
        {
            this.bid.Currency = Currency.Euro;
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update valid bid.
        /// </summary>
        [Test]
        public void TestUpdateValidBid()
        {
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Update(this.bid));
            }

            ValidationResult result = this.bidService.Update(this.bid);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the update bid with null bidder.
        /// </summary>
        [Test]
        public void TestUpdateBidWithNullBidder()
        {
            this.bid.Bidder = null;
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Update(this.bid));
            }

            ValidationResult result = this.bidService.Update(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update bid with invalid bidder.
        /// </summary>
        [Test]
        public void TestUpdateBidWithInvalidBidder()
        {
            this.bid.Bidder.Name = null;
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Update(this.bid));
            }

            ValidationResult result = this.bidService.Update(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the get bids.
        /// </summary>
        [Test]
        public void TestGetBids()
        {
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Get()).Return(new HashSet<Bid> { this.bid });
            }

            var bids = this.bidService.GetAll();

            Assert.AreEqual(bids.ToList().Count, 1);
            Assert.AreEqual(bids.ToList().First(), this.bid);
        }

        /// <summary>
        /// Tests the get bid by identifier.
        /// </summary>
        [Test]
        public void TestGetBidById()
        {
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.GetByID(10)).Return(this.bid);
            }

            var product = this.bidService.GetByID(10);

            Assert.AreEqual(product, this.bid);
        }
    }
}