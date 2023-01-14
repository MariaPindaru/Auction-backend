// <copyright file="BidServiceTests.cs" company="Transilvania University of Brasov">
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
        /// The auction service.
        /// </summary>
        private IAuctionService auctionService;

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

            this.auctionService = this.mocks.StrictMock<IAuctionService>();
            this.kernel.Rebind<IAuctionService>().ToConstant(this.auctionService);

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
        public void TestAdd_ValidBid_ReturnsNoError()
        {
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with null bidder.
        /// </summary>
        [Test]
        public void TestAdd_HasNullBidder_ReturnsErrorForBidder()
        {
            this.bid.Bidder = null;
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Bid.Bidder));
        }

        /// <summary>
        /// Tests the name of the add bid with invalid bidder null.
        /// </summary>
        [Test]
        public void TestAdd_BidderHasNullName_ReturnsErrorForBidderName()
        {
            this.bid.Bidder.Name = null;
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, "Bidder.Name");
        }

        /// <summary>
        /// Tests the add bid with invalid bidder role.
        /// </summary>
        [Test]
        public void TestAdd_BidderHasInvalidRole_ReturnsErrorForBidderRole()
        {
            var invalidRoleValue = Enum.GetValues(typeof(Role)).Cast<int>().Max() + 1;
            this.bid.Bidder.Role = (Role)invalidRoleValue;
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 2);
            Assert.AreEqual(result.Errors.ElementAt(0).PropertyName, "Bidder.Role");
            Assert.AreEqual(result.Errors.ElementAt(1).PropertyName, "Bidder");
        }

        /// <summary>
        /// Tests the add bid with wrong bidder role.
        /// </summary>
        [Test]
        public void TestAdd_BidderHasOffererRole_ReturnsErrorForBidderRole()
        {
            this.bid.Bidder.Role = Role.Offerer;
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Bid.Bidder));
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
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with price zero.
        /// </summary>
        [Test]
        public void TestAddBidWithPriceZero()
        {
            this.bid.Price = 0;
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
        }

        /// <summary>
        /// Tests the add bid with price too high default price.
        /// </summary>
        [Test]
        public void TestAddBidWithPriceTooHighDefaultPrice()
        {
            this.bid.Auction.StartPrice = 10.7m;
            this.bid.Price = 300.8m;
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
        }

        /// <summary>
        /// Tests the add bid with price too high existing bid.
        /// </summary>
        [Test]
        public void TestAddBidWithPriceTooHighExistingBid()
        {
            this.bid.Auction.StartPrice = 10.7m;
            this.bid.Auction.BidHistory.Add(new Bid { Price = 20.6m});
            this.bid.Price = 40.8m;
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with previous price.
        /// </summary>
        [Test]
        public void TestAddBidWithPreviousPrice()
        {
            this.bid.Auction.StartPrice = 10.7m;
            this.bid.Auction.BidHistory.Add(new Bid { Price = 20.6m });
            this.bid.Price = 20.6m;
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with valid price.
        /// </summary>
        [Test]
        public void TestAddBidWithValidPrice()
        {
            this.bid.Auction.StartPrice = 10.8m;
            this.bid.Price = 20.8m;
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsTrue(result.IsValid);
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
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with invalid currency.
        /// </summary>
        [Test]
        public void TestAddBidWithInvalidCurrency()
        {
            var maxRoleValue = Enum.GetValues(typeof(Currency)).Cast<int>().Max() + 1;
            this.bid.Currency = (Currency)maxRoleValue;
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with null auction.
        /// </summary>
        [Test]
        public void TestAddBidWithNullAuction()
        {
            this.bid.Auction = null;
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with nonexisting auction.
        /// </summary>
        [Test]
        public void TestAddBidWithNonexistingAuction()
        {
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(null);
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
