// <copyright file="UserSuspensionServiceTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AuctionBackend.DomainLayer.Config;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.RepositoryInterfaces;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using FluentValidation.Results;
    using Ninject;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// Tests for UserSuspensionService.
    /// </summary>
    internal class UserSuspensionServiceTests
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// The user service.
        /// </summary>
        private IUserSuspensionService userSuspensionService;

        /// <summary>
        /// The user repository.
        /// </summary>
        private IUserSuspensionRepository userSuspensionRepository;

        /// <summary>
        /// The configuration.
        /// </summary>
        private IConfiguration configuration;

        /// <summary>
        /// The mocks.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// The user suspension.
        /// </summary>
        private UserSuspension userSuspension;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Injector.Inject();
            this.kernel = Injector.Kernel;

            this.mocks = new MockRepository();
            this.userSuspensionRepository = this.mocks.StrictMock<IUserSuspensionRepository>();
            this.kernel.Rebind<IUserSuspensionRepository>().ToConstant(this.userSuspensionRepository);

            this.configuration = this.mocks.StrictMock<IConfiguration>();
            this.kernel.Rebind<IConfiguration>().ToConstant(this.configuration);

            this.userSuspensionService = this.kernel.Get<IUserSuspensionService>();

            this.userSuspension = new UserSuspension
            {
                Id = 10,
                User = new User
                {
                    Name = "name",
                    Role = Role.Offerer,
                },
                StartDate = System.DateTime.Now.AddDays(2),
                EndDate = System.DateTime.Now.AddDays(4),
            };
        }

        /// <summary>
        /// Tests the add valid user suspension returns no error.
        /// </summary>
        [Test]
        public void TestAdd_ValidUserSuspension_ReturnsNoError()
        {
            using (this.mocks.Record())
            {
                this.userSuspensionRepository.Expect(repo => repo.Insert(this.userSuspension));
            }

            ValidationResult result = this.userSuspensionService.Insert(this.userSuspension);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the add has null user returns error for user.
        /// </summary>
        [Test]
        public void TestAdd_HasNullUser_ReturnsErrorForUser()
        {
            this.userSuspension.User = null;
            using (this.mocks.Record())
            {
                this.userSuspensionRepository.Expect(repo => repo.Insert(this.userSuspension));
            }

            ValidationResult result = this.userSuspensionService.Insert(this.userSuspension);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(UserSuspension.User));
        }

        /// <summary>
        /// Tests the name of the add user has null name returns error for user.
        /// </summary>
        [Test]
        public void TestAdd_UserHasNullName_ReturnsErrorForUserName()
        {
            this.userSuspension.User.Name = null;
            using (this.mocks.Record())
            {
                this.userSuspensionRepository.Expect(repo => repo.Insert(this.userSuspension));
            }

            ValidationResult result = this.userSuspensionService.Insert(this.userSuspension);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, "User.Name");
        }

        /// <summary>
        /// Tests the add has default start date returns error for start time.
        /// </summary>
        [Test]
        public void TestAdd_HasDefaultStartDate_ReturnsErrorForStartTime()
        {
            this.userSuspension.StartDate = default;
            using (this.mocks.Record())
            {
                this.userSuspensionRepository.Expect(repo => repo.Insert(this.userSuspension));
            }

            ValidationResult result = this.userSuspensionService.Insert(this.userSuspension);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(UserSuspension.StartDate));
        }

        /// <summary>
        /// Tests the add start date is in past returns error for start time.
        /// </summary>
        [Test]
        public void TestAdd_StartDateIsInPast_ReturnsErrorForStartTime()
        {
            this.userSuspension.StartDate = System.DateTime.Now.AddDays(-1);
            using (this.mocks.Record())
            {
                this.userSuspensionRepository.Expect(repo => repo.Insert(this.userSuspension));
            }

            ValidationResult result = this.userSuspensionService.Insert(this.userSuspension);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(UserSuspension.StartDate));
        }

        /// <summary>
        /// Tests the add has default start date returns error for end time.
        /// </summary>
        [Test]
        public void TestAdd_HasDefaultEndDate_ReturnsErrorForEndTime()
        {
            this.userSuspension.EndDate = default;
            using (this.mocks.Record())
            {
                this.userSuspensionRepository.Expect(repo => repo.Insert(this.userSuspension));
            }

            ValidationResult result = this.userSuspensionService.Insert(this.userSuspension);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(UserSuspension.EndDate));
        }

        /// <summary>
        /// Tests the add end time is before start time returns error for end time.
        /// </summary>
        [Test]
        public void TestAdd_EndDateIsBeforeStartDate_ReturnsErrorForEndTime()
        {
            this.userSuspension.EndDate = System.DateTime.Now.AddDays(2);
            this.userSuspension.StartDate = System.DateTime.Now.AddDays(22);
            using (this.mocks.Record())
            {
                this.userSuspensionRepository.Expect(repo => repo.Insert(this.userSuspension));
            }

            ValidationResult result = this.userSuspensionService.Insert(this.userSuspension);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(UserSuspension.EndDate));
        }

        /// <summary>
        /// Tests the add suspension for user user is valid returns no error.
        /// </summary>
        [Test]
        public void TestAddSuspensionForUser_UserIsValid_ReturnsNoError()
        {
            var suspension = new UserSuspension
            {
                User = this.userSuspension.User,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
            };
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.SuspensionDays).Return(1);
                this.userSuspensionRepository.Expect(repo => repo.Insert(suspension)).IgnoreArguments();
            }

            ValidationResult result = this.userSuspensionService.AddSuspensionForUser(this.userSuspension.User);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the add suspension for user user is null returns error for user.
        /// </summary>
        [Test]
        public void TestAddSuspensionForUser_UserIsNull_ReturnsErrorForUser()
        {
            var suspension = new UserSuspension
            {
                User = null,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
            };
            using (this.mocks.Record())
            {
                this.configuration.Expect(config => config.SuspensionDays).Return(1);
                this.userSuspensionRepository.Expect(repo => repo.Insert(suspension)).IgnoreArguments();
            }

            ValidationResult result = this.userSuspensionService.AddSuspensionForUser(null);

            Assert.False(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(UserSuspension.User));
        }

        /// <summary>
        /// Tests the is user suspended user is null returns error for user.
        /// </summary>
        [Test]
        public void TestIsUserSuspended_UserIsNull_ReturnsFalse()
        {
            bool result = this.userSuspensionService.UserIsSuspended(null);
            Assert.False(result);
        }

        /// <summary>
        /// Tests the is user suspended user has no suspension returns false.
        /// </summary>
        [Test]
        public void TestIsUserSuspended_UserHasNoSuspension_ReturnsFalse()
        {
            using (this.mocks.Record())
            {
                this.userSuspensionRepository.Expect(repo => repo.Get()).IgnoreArguments()
                    .Return(new HashSet<UserSuspension>());
            }

            bool result = this.userSuspensionService.UserIsSuspended(this.userSuspension.User);

            Assert.False(result);
        }

        /// <summary>
        /// Tests the is user suspended user suspension returns true.
        /// </summary>
        [Test]
        public void TestIsUserSuspended_UserSuspension_ReturnsTrue()
        {
            using (this.mocks.Record())
            {
                this.userSuspensionRepository.Expect(repo => repo.Get()).IgnoreArguments()
                    .Return(new HashSet<UserSuspension> { this.userSuspension });
            }

            bool result = this.userSuspensionService.UserIsSuspended(this.userSuspension.User);

            Assert.True(result);
        }
    }
}
