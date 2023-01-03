// <copyright file="AuctionServiceTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ServiceTests
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
    /// AuctionServiceTests.
    /// </summary>
    internal class AuctionServiceTests
    {
        private IKernel kernel;
        private IAuctionService auctionService;

        private IAuctionRepository auctionRepository;
        private MockRepository mocks;

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

            this.auction = new Auction();
        }

        /// <summary>
        /// Tests the add valid auction.
        /// </summary>
        [Test]
        public void TestAddValidAuction()
        {
            this.auction.Currency = Currency.Dolar;
            this.auction.StartPrice = 10.3m;
            this.auction.StartTime = new DateTime(2012, 12, 12);
            this.auction.EndTime = new DateTime(2012, 12, 19);
            this.auction.Offerer = new User
            {
                Name = "user",
                Role = Role.Offerer,
            };
            this.auction.Product = new Product
            {
                Name = "product",
                Description = "the product description",
                Category = new Category
                {
                    Name = "category",
                },
            };

            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsTrue(result.IsValid);
        }
    }
}
