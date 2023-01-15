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
        public void TestAdd_HasNegativePrice_ReturnsErrorForPrice()
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
        public void TestAdd_HasPriceZero_ReturnsErrorForPrice()
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
        public void TestAdd_HasPriceTooHigh_LastPriceIsStartPrice_ReturnsErrorForPrice()
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
        public void TestAdd_HasPriceTooHigh_LastPriceIsAnotherBid_ReturnsErrorForPrice()
        {
            this.bid.Auction.StartPrice = 10.7m;
            this.bid.Auction.BidHistory.Add(new Bid { Price = 20.6m });
            this.bid.Price = 70.8m;
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add bid with previous price.
        /// </summary>
        [Test]
        public void TestAdd_PriceIsEqualWithTheLastOne_LastPriceIsStartPrice_ReturnsErrorForPrice()
        {
            this.bid.Auction.StartPrice = 10.7m;
            this.bid.Price = 10.7m;
            using (this.mocks.Record())
            {
                this.auctionService.Expect(service => service.GetByID(0)).Return(this.bid.Auction);
                this.bidRepository.Expect(repo => repo.Insert(this.bid));
            }

            ValidationResult result = this.bidService.Insert(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add price is equal with the last one last price is a bid returns error for price.
        /// </summary>
        [Test]
        public void TestAdd_PriceIsEqualWithTheLastOne_LastPriceIsABid_ReturnsErrorForPrice()
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
        public void TestAdd_HasValidPrice_ReturnsNoError()
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
        public void TestAdd_HasDifferentCurrencyThanAuction_ReturnsErrorForCurrency()
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
        public void TestAdd_HasInvlidCurrency_ReturnsErrorForCurrancy()
        {
            var invalidCurrencyValue = Enum.GetValues(typeof(Currency)).Cast<int>().Max() + 1;
            this.bid.Currency = (Currency)invalidCurrencyValue;
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
        public void TestAdd_HasNullAuction_ReturnsErrorForAuction()
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
        public void TestAdd_AuctionDoesNotExist_ReturnsErrorForAuction()
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
        public void TestUpdate_ValidBid_ReturnNoError()
        {
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.GetByID(this.bid.Id)).Return(this.bid);
                this.bidRepository.Expect(repo => repo.Update(this.bid));
            }

            ValidationResult result = this.bidService.Update(this.bid);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the update bid with null bidder.
        /// </summary>
        [Test]
        public void TestUpdate_HasNullOfferer_ReturnsErrorForOfferer()
        {
            this.bid.Bidder = null;
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.GetByID(this.bid.Id)).Return(this.bid);
                this.bidRepository.Expect(repo => repo.Update(this.bid));
            }

            ValidationResult result = this.bidService.Update(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update bid with invalid bidder.
        /// </summary>
        [Test]
        public void TestUpdate_OffererHasNullName_ReturnsErrorForOffererName()
        {
            this.bid.Bidder.Name = null;
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.GetByID(this.bid.Id)).Return(this.bid);
                this.bidRepository.Expect(repo => repo.Update(this.bid));
            }

            ValidationResult result = this.bidService.Update(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update change currency returns error for currency.
        /// </summary>
        [Test]
        public void TestUpdate_ChangeCurrency_ReturnsErrorForCurrency()
        {
            this.bid.Currency = Currency.Euro;
            this.bid.Auction.Currency = Currency.Dolar;
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.GetByID(this.bid.Id)).Return(this.bid);
                this.bidRepository.Expect(repo => repo.Update(this.bid));
            }

            ValidationResult result = this.bidService.Update(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update auction does not exist returns error for auction.
        /// </summary>
        [Test]
        public void TestUpdate_NotTheLastBid_ReturnsErrorForId()
        {
            this.bid.Auction.BidHistory = new HashSet<Bid> 
            {
                this.bid,
                new Bid(),
            };
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.GetByID(this.bid.Id)).Return(this.bid);
                this.bidRepository.Expect(repo => repo.Update(this.bid));
            }

            ValidationResult result = this.bidService.Update(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update bid does not exist returns error for identifier.
        /// </summary>
        [Test]
        public void TestUpdate_BidDoesNotExist_ReturnsErrorForId()
        {
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.GetByID(this.bid.Id)).Return(null);
                this.bidRepository.Expect(repo => repo.Update(this.bid));
            }

            ValidationResult result = this.bidService.Update(this.bid);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the get bids.
        /// </summary>
        [Test]
        public void TestGet_ReturnsCurrentBid()
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
        public void TestGetById_ReturnsCurrentBid()
        {
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.GetByID(10)).Return(this.bid);
            }

            var product = this.bidService.GetByID(10);

            Assert.AreEqual(product, this.bid);
        }

        /// <summary>
        /// Tests the get by identifier null identifier returns null.
        /// </summary>
        [Test]
        public void TestGetById_NullId_ReturnsNull()
        {
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.GetByID(null)).Return(null);
            }

            var result = this.bidService.GetByID(null);

            Assert.AreEqual(result, null);
        }

        /// <summary>
        /// Tests the delete valid bid.
        /// </summary>
        [Test]
        public void TestDelete_ValidBid()
        {
            using (this.mocks.Record())
            {
                this.bidRepository.Expect(repo => repo.Delete(this.bid));
            }

            this.bidService.Delete(this.bid);
        }
    }
}
