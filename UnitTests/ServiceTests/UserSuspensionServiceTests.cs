// <copyright file="UserSuspensionServiceTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ServiceTests
{
    using AuctionBackend.DomainLayer.Config;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.RepositoryInterfaces;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using FluentValidation.Results;
    using Ninject;
    using NUnit.Framework;
    using Rhino.Mocks;
    using System.Linq;

    /// <summary>
    /// UserSuspensionServiceTests.
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
    }
}
