// <copyright file="UserServiceTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ServiceTests
{
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using FluentValidation.Results;
    using Ninject;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// User service tests.
    /// </summary>
    internal class UserServiceTests
    {
        private IKernel kernel;
        private IUserService userService;

        private IUserRepository userRepository;
        private MockRepository mocks;

        private User user;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Injector.Inject();
            this.kernel = Injector.Kernel;

            this.mocks = new MockRepository();
            this.userRepository = this.mocks.StrictMock<IUserRepository>();

            this.kernel.Rebind<IUserRepository>().ToConstant(this.userRepository);
            this.userService = this.kernel.Get<IUserService>();

            this.user = new User();
        }

        /// <summary>
        /// Tests the add valid user.
        /// </summary>
        [Test]
        public void TestAddValidUser()
        {
            this.user.Name = "Username";
            this.user.Role = Role.Bidder;
            this.user.Score = 20.9f;

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = this.userService.Insert(this.user);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the name of the addd user null.
        /// </summary>
        [Test]
        public void TestAdddUserNullName()
        {
            this.user.Name = null;
            this.user.Role = Role.Bidder;
            this.user.Score = 20.9f;

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = this.userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the short name of the addd user.
        /// </summary>
        [Test]
        public void TestAdddUserShortName()
        {
            this.user.Name = "U";
            this.user.Role = Role.Bidder;
            this.user.Score = 20.9f;

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = this.userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the long name of the addd user.
        /// </summary>
        [Test]
        public void TestAdddUserLongName()
        {
            string longString = new string('*', 51);
            this.user.Name = longString;
            this.user.Role = Role.Bidder;
            this.user.Score = 20.9f;

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = this.userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the addd user invalid role.
        /// </summary>
        [Test]
        public void TestAdddUserInvalidRole()
        {
            this.user.Name = "Username";
            this.user.Role = (Role)200;
            this.user.Score = 20.9f;

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = this.userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the addd user negative score.
        /// </summary>
        [Test]
        public void TestAdddUserNegativeScore()
        {
            this.user.Name = "Username";
            this.user.Role = Role.Bidder;
            this.user.Score = -20.9f;

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = this.userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the addd user out of range score.
        /// </summary>
        [Test]
        public void TestAdddUserOutOfRangeScore()
        {
            this.user.Name = "Username";
            this.user.Role = Role.Bidder;
            this.user.Score = 220.9f;

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = this.userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }
    }
}
