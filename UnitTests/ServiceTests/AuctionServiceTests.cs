﻿// <copyright file="AuctionServiceTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ServiceTests
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
    /// Tests for AuctionService.
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
        /// The user suspension service.
        /// </summary>
        private IUserSuspensionService userSuspensionService;

        /// <summary>
        /// The auction repository.
        /// </summary>
        private IAuctionRepository auctionRepository;

        /// <summary>
        /// The configuration.
        /// </summary>
        private IConfiguration configuration;

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

            this.userSuspensionService = this.mocks.StrictMock<IUserSuspensionService>();
            this.kernel.Rebind<IUserSuspensionService>().ToConstant(this.userSuspensionService);

            this.configuration = this.mocks.StrictMock<IConfiguration>();
            this.kernel.Rebind<IConfiguration>().ToConstant(this.configuration);

            this.auctionService = this.kernel.Get<IAuctionService>();

            this.auction = new Auction
            {
                Id = 10,
                IsFinished = false,
                Currency = Currency.Dolar,
                StartPrice = 10.3m,
                StartTime = DateTime.Now.AddDays(1),
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
                        Name = "user",
                        Role = Role.Offerer,
                    },
                },
            };
        }

        /// <summary>
        /// Tests the add valid auction.
        /// </summary>
        [Test]
        public void TestAdd_ValidAuction_ReturnsNoErrors()
        {
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Auction>());
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                    .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the add auction with invalid currency.
        /// </summary>
        [Test]
        public void TestAdd_HasInvalidCurrency_ReturnsErrorForCurrency()
        {
            var invalidCurrencyValue = Enum.GetValues(typeof(Currency)).Cast<int>().Max() + 1;
            this.auction.Currency = (Currency)invalidCurrencyValue;
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Auction>());
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.Currency));
        }

        /// <summary>
        /// Tests the add auction with negative price.
        /// </summary>
        [Test]
        public void TestAdd_HasNegativeStartPrice_ReturnsErrorForStartPrice()
        {
            this.auction.StartPrice = -20.8m;
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Auction>());
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.StartPrice));
        }

        /// <summary>
        /// Tests the add has zero start price returns error for start price.
        /// </summary>
        [Test]
        public void TestAdd_HasZeroStartPrice_ReturnsErrorForStartPrice()
        {
            this.auction.StartPrice = 0.0m;
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Auction>());
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.StartPrice));
        }

        /// <summary>
        /// Tests the add auction with default start time.
        /// </summary>
        [Test]
        public void TestAdd_HasDefaultStartTime_ReturnsErrorForStartTime()
        {
            this.auction.StartTime = default;
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Auction>());
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 2);
            Assert.AreEqual(result.Errors.ElementAt(0).PropertyName, nameof(Auction.StartTime));
            Assert.AreEqual(result.Errors.ElementAt(1).PropertyName, nameof(Auction.StartTime));
        }

        /// <summary>
        /// Tests the add auction with default end time.
        /// </summary>
        [Test]
        public void TestAdd_HasDefaultEndTime_ReturnsErrorForEndTime()
        {
            this.auction.EndTime = default;
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Auction>());
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.EndTime));
        }

        /// <summary>
        /// Tests the add auction with end time before start time.
        /// </summary>
        [Test]
        public void TestAdd_EndTimeIsBeforeStartTime_ReturnsErrorForEndTime()
        {
            this.auction.EndTime = DateTime.Now.AddDays(1);
            this.auction.StartTime = DateTime.Now.AddDays(8);
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Auction>());
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.EndTime));
        }

        /// <summary>
        /// Tests the add auction with start time in past.
        /// </summary>
        [Test]
        public void TestAdd_StartTimeIsInPast_ReturnsErrorForStartTime()
        {
            this.auction.StartTime = DateTime.Now.AddDays(-7);
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Auction>());
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.StartTime));
        }

        /// <summary>
        /// Tests the add start time is in past and end time is before start time returns error for start time.
        /// </summary>
        [Test]
        public void TestAdd_StartTimeIsInPast_And_EndTimeIsBeforeStartTime_ReturnsErrorForStartTime()
        {
            this.auction.StartTime = DateTime.Now.AddDays(-7);
            this.auction.EndTime = DateTime.Now.AddDays(-10);
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Auction>());
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 2);
            Assert.AreEqual(result.Errors.ElementAt(0).PropertyName, nameof(Auction.EndTime)); // End time is before start time
            Assert.AreEqual(result.Errors.ElementAt(1).PropertyName, nameof(Auction.StartTime));  // Start time in the past
        }

        /// <summary>
        /// Tests the add auction with is finished true before start.
        /// </summary>
        [Test]
        public void TestAdd_IsFinishedIsTrueBeforeStartTime_ReturnsErrorForIsFinished()
        {
            this.auction.StartTime = DateTime.Now.AddDays(2);
            this.auction.EndTime = DateTime.Now.AddDays(20);
            this.auction.IsFinished = true;

            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Auction>());
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.IsFinished));
        }

        /// <summary>
        /// Tests the add auction with null product.
        /// </summary>
        [Test]
        public void TestAdd_HasNullProduct_ReturnsErrorForProduct()
        {
            this.auction.Product = null;
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Auction>());
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.Product));
        }

        /// <summary>
        /// Tests the add auction with invalid product.
        /// </summary>
        [Test]
        public void TestAdd_ProductHasNullName_ReturnsErrorForProductName()
        {
            this.auction.Product.Name = null;
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Auction>());
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, "Product.Name");
        }

        /// <summary>
        /// Tests the add user is suspended returns error for user.
        /// </summary>
        [Test]
        public void TestAdd_UserIsSuspended_ReturnsErrorForUser()
        {
            this.auction.Product.Name = null;
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Auction>());
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(true);
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, "Offerer");
        }

        /// <summary>
        /// Tests the update valid auction.
        /// </summary>
        [Test]
        public void TestUpdate_ValidAuction_ReturnsNoErrors()
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
        /// Tests the update auction with invalid product.
        /// </summary>
        [Test]
        public void TestUpdate_HasNullProduct_ReturnsErrorForProduct()
        {
            this.auction.Product = null;
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(10)).Return(this.auction);
                this.auctionRepository.Expect(repo => repo.Update(this.auction));
            }

            ValidationResult result = this.auctionService.Update(this.auction);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.Product));
        }

        /// <summary>
        /// Tests the update non existing auction.
        /// </summary>
        [Test]
        public void TestUpdate_AuctionDoesNotExist_ReturnsErrorforId()
        {
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(10)).Return(null);
                this.auctionRepository.Expect(repo => repo.Update(this.auction));
            }

            ValidationResult result = this.auctionService.Update(this.auction);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.Id));
        }

        /// <summary>
        /// Tests the update auction after is finished.
        /// </summary>
        [Test]
        public void TestUpdate_AuctionIsFinished_ReturnsErrorForIsFinished()
        {
            this.auction.StartTime = DateTime.Now.AddDays(-10);
            this.auction.EndTime = DateTime.Now.AddDays(-6);
            this.auction.IsFinished = true;

            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(10)).Return(this.auction);
                this.auctionRepository.Expect(repo => repo.Update(this.auction));
            }

            ValidationResult result = this.auctionService.Update(this.auction);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.IsFinished));
        }

        /// <summary>
        /// Tests the update is finished true before end time returns error for is finished.
        /// </summary>
        [Test]
        public void TestUpdate_IsFinishedTrueBeforeEndTime_ReturnsErrorForIsFinished()
        {
            this.auction.StartTime = DateTime.Now.AddDays(-10);
            this.auction.EndTime = DateTime.Now.AddDays(6);
            this.auction.IsFinished = true;

            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(10)).Return(this.auction);
                this.auctionRepository.Expect(repo => repo.Update(this.auction));
            }

            ValidationResult result = this.auctionService.Update(this.auction);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.IsFinished));
        }

        /// <summary>
        /// Tests the update not finished after end time returns no error.
        /// </summary>
        [Test]
        public void TestUpdate_NotFinishedAfterEndTime_ReturnsErrorForIsFinished()
        {
            this.auction.StartTime = DateTime.Now.AddDays(-10);
            this.auction.EndTime = DateTime.Now.AddDays(-3);
            this.auction.IsFinished = false;

            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(10)).Return(this.auction);
                this.auctionRepository.Expect(repo => repo.Update(this.auction));
            }

            ValidationResult result = this.auctionService.Update(this.auction);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update when auction has bids but changes currency returns error for currency.
        /// </summary>
        [Test]
        public void TestUpdate_HasBidsButChangesCurrency_ReturnsErrorForCurrency()
        {
            this.auction.Currency = Currency.Euro;
            this.auction.BidHistory.Add(new Bid { Currency = Currency.Dolar });
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(10)).Return(this.auction);
                this.auctionRepository.Expect(repo => repo.Update(this.auction));
            }

            ValidationResult result = this.auctionService.Update(this.auction);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the delete auction.
        /// </summary>
        [Test]
        public void TestDelete_ReturnsNoError()
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
        public void TestGetById_AuctionExists_ReturnsAuction()
        {
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(1)).Return(this.auction);
            }

            var result = this.auctionService.GetByID(1);
            Assert.AreEqual(result, this.auction);
        }

        /// <summary>
        /// Tests the get by identifier auction does not exists returns no error.
        /// </summary>
        [Test]
        public void TestGetById_AuctionDoesNotExists_ReturnsNull()
        {
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(1)).Return(null);
            }

            var result = this.auctionService.GetByID(1);
            Assert.AreEqual(result, null);
        }

        /// <summary>
        /// Tests the get auctions.
        /// </summary>
        [Test]
        public void TestGetAll_ReturnsAuctions()
        {
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.Get()).Return(new HashSet<Auction> { this.auction });
            }

            var result = this.auctionService.GetAll();
            Assert.AreEqual(result.ToList().Count, 1);
            Assert.AreEqual(result.ToList().First(), this.auction);
        }

        /// <summary>
        /// Tests the get by identifier null identifier returns null.
        /// </summary>
        [Test]
        public void TestGetById_NullId_ReturnsNull()
        {
            using (this.mocks.Record())
            {
                this.auctionRepository.Expect(repo => repo.GetByID(null)).Return(null);
            }

            var score = this.auctionService.GetByID(null);

            Assert.AreEqual(score, null);
        }

        /// <summary>
        /// Tests the add user active auctions limit reached return error for offerer.
        /// </summary>
        [Test]
        public void TestAdd_UserActiveAuctionsLimitReached_ReturnErrorForOfferer()
        {
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);

                ICollection<Auction> activeAuctions = Enumerable.Range(0, 2)
                    .Select(i => new Auction { Product = new Product { Description = "blabla" } })
                    .ToList();

                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(activeAuctions);
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);

                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.Product.Offerer));
        }

        /// <summary>
        /// Tests the product has duplicate description description is identical with another one same user returns error for description.
        /// </summary>
        [Test]
        public void TestProductHasDuplicateDescription_DescriptionIsIdenticalWithAnotherOne_SameUser_ReturnsTrue()
        {
            var auctions = new HashSet<Auction>
            {
                this.auction,
            };
            bool result = this.auctionService.ProductHasDuplicateDescription(this.auction.Product, auctions);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the product has duplicate description description is similar with another one same user returns false.
        /// </summary>
        [Test]
        public void TestProductHasDuplicateDescription_DescriptionIsSimilarWithAnotherOne_SameUser_ReturnsFalse()
        {
            this.auction.Product.Description = "hihi";
            var copyAuction = new Auction
            {
                Id = 10,
                IsFinished = false,
                Currency = Currency.Dolar,
                StartPrice = 10.3m,
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(5),
                Product = new Product
                {
                    Name = "product",
                    Description = "haha",
                    Category = new Category
                    {
                        Name = "category",
                    },
                    Offerer = new User
                    {
                        Name = "user",
                        Role = Role.Offerer,
                    },
                },
            };
            copyAuction.Product.Description = "haha";
            var auctions = new HashSet<Auction>
            {
                copyAuction,
            };
            bool result = this.auctionService.ProductHasDuplicateDescription(this.auction.Product, auctions);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the product has duplicate description description has extra characters returns false.
        /// </summary>
        [Test]
        public void TestProductHasDuplicateDescription_DescriptionHasExtraCharacters_ReturnsFalse()
        {
            var copyAuction = new Auction
            {
                Id = 10,
                IsFinished = false,
                Currency = Currency.Dolar,
                StartPrice = 10.3m,
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(5),
                Product = new Product
                {
                    Name = "product",
                    Description = "haha",
                    Category = new Category
                    {
                        Name = "category",
                    },
                    Offerer = new User
                    {
                        Name = "user",
                        Role = Role.Offerer,
                    },
                },
            };
            this.auction.Product.Description = "????????hihi!!!";
            var auctions = new HashSet<Auction>
            {
                copyAuction,
            };
            bool result = this.auctionService.ProductHasDuplicateDescription(this.auction.Product, auctions);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the product has duplicate description offerer is null returns false.
        /// </summary>
        [Test]
        public void TestProductHasDuplicateDescription_OffererIsNull_ReturnsFalse()
        {
            this.auction.Product.Offerer = null;
            var auctions = new HashSet<Auction>
            {
                this.auction,
            };

            bool result = this.auctionService.ProductHasDuplicateDescription(this.auction.Product, auctions);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the product has duplicate description description is null returns false.
        /// </summary>
        [Test]
        public void TestProductHasDuplicateDescription_DescriptionIsNull_ReturnsFalse()
        {
            this.auction.Product.Description = null;
            var auctions = new HashSet<Auction>
            {
                this.auction,
            };

            bool result = this.auctionService.ProductHasDuplicateDescription(this.auction.Product, auctions);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the add product has duplicate description returns error for product.
        /// </summary>
        [Test]
        public void TestAdd_ProductHasDuplicateDescription_ReturnsErrorForProduct()
        {
            var auctions = new HashSet<Auction>
            {
                new Auction { Product = new Product { Description = this.auction.Product.Description } },
            };

            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(auctions);
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.Product));
        }

        /// <summary>
        /// Tests the add product has very similar description returns error for product.
        /// </summary>
        [Test]
        public void TestAdd_ProductHasVerySimilarDescription_ReturnsErrorForProduct()
        {
            this.auction.Product.Description = "Product description.";
            var auctions = new HashSet<Auction>
            {
                new Auction { Product = new Product { Description = "Product descript." } },
            };

            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(auctions);
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.Product));
        }

        /// <summary>
        /// Tests the add product has very similar description returns error for product.
        /// </summary>
        [Test]
        public void TestAdd_ProductHasDuplicateDescription_Extracharacters_ReturnsErrorForProduct()
        {
            this.auction.Product.Description = ":) ???????? Product description. ?? :(";
            var auctions = new HashSet<Auction>
            {
                new Auction { Product = new Product { Description = "Product description." } },
            };

            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(auctions);
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                              .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Auction.Product));
        }

        /// <summary>
        /// Tests the add product has somewhat similar description extra characters returns error for product.
        /// </summary>
        [Test]
        public void TestAdd_ProductHasSomewhatSimilarDescription_ExtraCharacters_ReturnsErrorForProduct()
        {
            this.auction.Product.Description = "Product something.";
            var auctions = new HashSet<Auction>
            {
                new Auction { Product = new Product { Description = "Product description." } },
            };

            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.MaxActiveAuctions).Return(2);
                this.auctionRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(auctions);
                this.userSuspensionService.Expect(service => service.UserIsSuspended(this.auction.Product.Offerer))
                .Return(false);
                this.auctionRepository.Expect(repo => repo.Insert(this.auction));
            }

            ValidationResult result = this.auctionService.Insert(this.auction);

            Assert.IsTrue(result.IsValid);
        }
    }
}
