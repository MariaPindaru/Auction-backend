// <copyright file="AuctionServiceTests.cs" company="Transilvania University of Brasov">
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
    internal class AuctionServiceTests
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// The auction service.
        /// </summary>
        private IAuctionService auctionService;

        /// <summary>
        /// The auction repository.
        /// </summary>
        private IAuctionRepository auctionRepository;

        /// <summary>
        /// The mocks.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// The auction.
        /// </summary>
        private Auction auction;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Injector.Inject();
            this.kernel = Injector.Kernel;

            this.mocks = new MockRepository();
            this.auctionRepository = this.mocks.StrictMock<IAuctionRepository>();

            this.kernel.Rebind<IAuctionRepository>().ToConstant(this.auctionRepository);
            this.auctionService = this.kernel.Get<IAuctionService>();

            this.auction = new Auction
            {
                Id = 10,
                IsFinished = false,
                Currency = Currency.Dolar,
                StartPrice = 10.3m,
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(5),
                Offerer = new User
                {
                    Name = "user",
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
        }

        /// <summary>
        /// Tests the add valid auction.
        /// </summary>
        [Test]
        public void TestAddValidAuction()
        {
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the add auction with invalid currency.
        /// </summary>
        [Test]
        public void TestAddAuctionWithInvalidCurrency()
        {
            this.auction.Currency = (Currency)200;
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add auction with negative price.
        /// </summary>
        [Test]
        public void TestAddAuctionWithNegativePrice()
        {
            this.auction.StartPrice = -20.8m;
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add auction with default start time.
        /// </summary>
        [Test]
        public void TestAddAuctionWithDefaultStartTime()
        {
            this.auction.StartTime = default;
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add auction with default end time.
        /// </summary>
        [Test]
        public void TestAddAuctionWithDefaultEndTime()
        {
            this.auction.EndTime = default;
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add auction with end time before start time.
        /// </summary>
        [Test]
        public void TestAddAuctionWithEndTimeBeforeStartTime()
        {
            this.auction.EndTime = DateTime.Now;
            this.auction.StartTime = DateTime.Now.AddDays(8);
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add auction with start time in past.
        /// </summary>
        [Test]
        public void TestAddAuctionWithStartTimeInPast()
        {
            this.auction.StartTime = DateTime.Now.AddDays(-7);
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add auction with is finished true before start.
        /// </summary>
        [Test]
        public void TestAddAuctionWithIsFinishedTrueBeforeStart()
        {
            this.auction.StartTime = DateTime.Now.AddDays(2);
            this.auction.EndTime = DateTime.Now.AddDays(20);
            this.auction.IsFinished = true;

            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add auction with null offerer.
        /// </summary>
        [Test]
        public void TestAddAuctionWithNullOfferer()
        {
            this.auction.Offerer = null;
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add auction with invalid offerer.
        /// </summary>
        [Test]
        public void TestAddAuctionWithInvalidOfferer()
        {
            this.auction.Offerer.Name = null;
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add auction with null product.
        /// </summary>
        [Test]
        public void TestAddAuctionWithNullProduct()
        {
            this.auction.Product = null;
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add auction with invalid product.
        /// </summary>
        [Test]
        public void TestAddAuctionWithInvalidProduct()
        {
            this.auction.Product.Name = null;
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update auction with is finished false after end date.
        /// </summary>
        [Test]
        public void TestUpdateAuctionWithIsFinishedFalseAfterEndDate()
        {
            this.auction.StartTime = DateTime.Now.AddDays(-2);
            this.auction.EndTime = DateTime.Now.AddDays(-1);
            this.auction.IsFinished = false;
            this.auction.Id = 10;

            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(10)).Return(this.auction);
                this.auctionRepository.Expect(repo => repo.Update(this.auction));
            }

            ValidationResult result = this.auctionService.Update(this.auction);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update auction with invalid product.
        /// </summary>
        [Test]
        public void TestUpdateAuctionWithInvalidProduct()
        {
            this.auction.Product = null;
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(10)).Return(this.auction);
                this.auctionRepository.Expect(repo => repo.Update(this.auction));
            }

            ValidationResult result = this.auctionService.Update(this.auction);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update nonexisting auction.
        /// </summary>
        [Test]
        public void TestUpdateNonexistingAuction()
        {
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(10)).Return(null);
                this.auctionRepository.Expect(repo => repo.Update(this.auction));
            }

            ValidationResult result = this.auctionService.Update(this.auction);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update valid auction.
        /// </summary>
        [Test]
        public void TestUpdateValidAuction()
        {
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(10)).Return(this.auction);
                this.auctionRepository.Expect(repo => repo.Update(this.auction));
            }

            ValidationResult result = this.auctionService.Update(this.auction);
            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the delete auction.
        /// </summary>
        [Test]
        public void TestDeleteAuction()
        {
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Delete(this.auction));
            }

            this.auctionService.Delete(this.auction);
        }

        /// <summary>
        /// Tests the get auction by identifier.
        /// </summary>
        [Test]
        public void TestGetAuctionById()
        {
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(1)).Return(this.auction);
            }

            var result = this.auctionService.GetByID(1);
            Assert.AreEqual(result, this.auction);
        }

        /// <summary>
        /// Tests the get auctions.
        /// </summary>
        [Test]
        public void TestGetAuctions()
        {
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Get()).Return(new HashSet<Auction> { this.auction });
            }

            var result = this.auctionService.GetAll();
            Assert.AreEqual(result.ToList().Count, 1);
            Assert.AreEqual(result.ToList().First(), this.auction);
        }
    }
}
