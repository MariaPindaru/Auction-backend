// <copyright file="AuctionEndToEndTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.EndToEnd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.Config;
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
    public class AuctionEndToEndTests
    {
        private IKernel kernel;

        private IAuctionService auctionService;

        private IAuctionRepository auctionRepository;

        private IConfiguration configuration;

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

            this.configuration = this.mocks.StrictMock<IConfiguration>();
            this.kernel.Rebind<IConfiguration>().ToConstant(this.configuration);

            this.auctionService = this.kernel.Get<IAuctionService>();

            this.auction = new Auction
            {
                Currency = Currency.Dolar,
                StartPrice = 10.3m,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(5),
                Offerer = new User
                {
                    Id = 10,
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
        }

        /// <summary>
        /// Tests the add auction maximum limit achived.
        /// </summary>
        [Test]
        public void TestAddAuctionMaxLimitAchived()
        {
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);

                ICollection<Auction> activeAuctions = Enumerable.Range(0, 2)
                    .Select(i => new Auction())
                    .ToList();

                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(activeAuctions);

                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
        }
    }
}
