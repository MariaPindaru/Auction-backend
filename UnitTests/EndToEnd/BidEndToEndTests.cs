// <copyright file="BidEndToEndTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.EndToEndTest
{
    using System;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using FluentValidation.Results;
    using Ninject;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// AuctionEndToEndTests.
    /// </summary>
    internal class BidEndToEndTests
    {
        private IKernel kernel;

        private IBidService bidService;

        private IBidRepository bidRepository;

        private MockRepository mocks;

        private Auction auction;

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

            this.auction = new Auction
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

            this.bid = new Bid
            {
                Id = 0,
                Bidder = new User
                {
                    Name = "bidder",
                    Role = Role.Bidder,
                },
                Price = 20.3m,
                Currency = Currency.Dolar,
                Auction = this.auction,
            };
        }

        /// <summary>
        /// Tests the add bid with valid price.
        /// </summary>
        [Test]
        public void TestAddBidWithValidPrice()
        {
            this.auction.BidHistory.Add(new Bid
            {
                Id = 0,
                Bidder = new User
                {
                    Name = "bidder",
                    Role = Role.Bidder,
                },
                Price = 15.3m,
                Currency = Currency.Dolar,
                Auction = this.auction,
            });

            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with same price.
        /// </summary>
        [Test]
        public void TestAddBidWithSamePrice()
        {
            this.auction.BidHistory.Add(new Bid
            {
                Id = 0,
                Bidder = new User
                {
                    Name = "bidder",
                    Role = Role.Bidder,
                },
                Price = 20.3m,
                Currency = Currency.Dolar,
                Auction = this.auction,
            });

            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with price too high.
        /// </summary>
        [Test]
        public void TestAddBidWithPriceTooHigh()
        {
            this.auction.BidHistory.Add(new Bid
            {
                Id = 0,
                Bidder = new User
                {
                    Name = "bidder",
                    Role = Role.Bidder,
                },
                Price = 20.3m,
                Currency = Currency.Dolar,
                Auction = this.auction,
            });

            this.bid.Price = 100.7m;

            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
        }
    }
}
