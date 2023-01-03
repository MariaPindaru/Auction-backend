using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
using AuctionBackend.DomainLayer.DomainModel;
using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
using AuctionBackend.Startup;
using FluentValidation.Results;
using Ninject;
using NUnit.Framework;
using Rhino.Mocks;

namespace UnitTests.ServiceTests
{
    class UserServiceTests
    {
        private IKernel kernel;
        private IUserService userService;

        private IUserRepository userRepository;
        private MockRepository mocks;

        private User user;

        [SetUp]
        public void Setup()
        {
            Injector.Inject();
            this.kernel = Injector.Kernel;

            this.mocks = new MockRepository();
            this.userRepository = mocks.StrictMock<IUserRepository>();

            this.kernel.Rebind<IUserRepository>().ToConstant(userRepository);
            this.userService = kernel.Get<IUserService>();

            this.user = new User();
        }

        [Test]
        public void TestAddValidUser()
        {
            this.user.Name = "Username";
            this.user.Role = Role.Bidder;
            this.user.Score = 20.9f;

            using (this.mocks.Record())
            {
                userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = userService.Insert(this.user);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void TestAdddUserNullName()
        {
            this.user.Name = null;
            this.user.Role = Role.Bidder;
            this.user.Score = 20.9f;

            using (this.mocks.Record())
            {
                userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }
        
        [Test]
        public void TestAdddUserShortName()
        {
            this.user.Name = "U";
            this.user.Role = Role.Bidder;
            this.user.Score = 20.9f;

            using (this.mocks.Record())
            {
                userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestAdddUserLongName()
        {
            string longString = new string('*', 51);
            this.user.Name = longString;
            this.user.Role = Role.Bidder;
            this.user.Score = 20.9f;

            using (this.mocks.Record())
            {
                userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestAdddUserInvalidRole()
        {
            this.user.Name = "Username";
            this.user.Role = (Role)200;
            this.user.Score = 20.9f;

            using (this.mocks.Record())
            {
                userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestAdddUserNegativeScore()
        {
            this.user.Name = "Username";
            this.user.Role = Role.Bidder;
            this.user.Score = -20.9f;

            using (this.mocks.Record())
            {
                userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestAdddUserOutOfRangeScore()
        {
            this.user.Name = "Username";
            this.user.Role = Role.Bidder;
            this.user.Score = 220.9f;

            using (this.mocks.Record())
            {
                userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

    }
}
