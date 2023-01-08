// <copyright file="UserServiceTests.cs" company="Transilvania University of Brasov">
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
    /// User service tests.
    /// </summary>
    internal class UserServiceTests
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// The user service.
        /// </summary>
        private IUserService userService;

        /// <summary>
        /// The user repository.
        /// </summary>
        private IUserRepository userRepository;

        /// <summary>
        /// The configuration.
        /// </summary>
        private IConfiguration configuration;

        /// <summary>
        /// The mocks.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// The user.
        /// </summary>
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

            this.configuration = this.mocks.StrictMock<IConfiguration>();
            this.kernel.Rebind<IConfiguration>().ToConstant(this.configuration);

            this.userService = this.kernel.Get<IUserService>();

            this.user = new User { Id = 0, };
        }

        /// <summary>
        /// Tests the add valid user.
        /// </summary>
        [Test]
        public void TestAddValidUser()
        {
            this.user.Name = "Username";
            this.user.Role = Role.Bidder;

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = this.userService.Insert(this.user);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the name of the add user null.
        /// </summary>
        [Test]
        public void TestAddUserWithNullName()
        {
            this.user.Name = null;
            this.user.Role = Role.Bidder;

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = this.userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the short name of the add user with.
        /// </summary>
        [Test]
        public void TestAddUserWithShortName()
        {
            this.user.Name = "U";
            this.user.Role = Role.Bidder;

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = this.userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the long name of the add user.
        /// </summary>
        [Test]
        public void TestAddUserWithLongName()
        {
            string longString = new string('*', 51);
            this.user.Name = longString;
            this.user.Role = Role.Bidder;

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = this.userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add user invalid role.
        /// </summary>
        [Test]
        public void TestAddUserWithInvalidRole()
        {
            this.user.Name = "Username";
            this.user.Role = (Role)200;

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Insert(this.user));
            }

            ValidationResult result = this.userService.Insert(this.user);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update valid user.
        /// </summary>
        [Test]
        public void TestUpdateValidUser()
        {
            this.user.Name = "Username";
            this.user.Role = Role.Bidder;

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Update(this.user));
            }

            ValidationResult result = this.userService.Update(this.user);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the name of the update user with null.
        /// </summary>
        [Test]
        public void TestUpdateUserWithNullName()
        {
            this.user.Name = null;
            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Update(this.user));
            }

            ValidationResult result = this.userService.Update(this.user);

            Assert.IsFalse(result.IsValid);
        }


        /// <summary>
        /// Tests the get users.
        /// </summary>
        [Test]
        public void TestGetUsers()
        {
            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Get()).Return(new HashSet<User> { this.user });
            }

            var users = this.userService.GetAll();

            Assert.AreEqual(users.ToList().Count, 1);
            Assert.AreEqual(users.ToList().First(), this.user);
        }

        /// <summary>
        /// Tests the get user by identifier.
        /// </summary>
        [Test]
        public void TestGetUserById()
        {
            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.GetByID(10)).Return(this.user);
            }

            var product = this.userService.GetByID(10);

            Assert.AreEqual(product, this.user);
        }

        /// <summary>
        /// Tests the get user seriosity score.
        /// </summary>
        [Test]
        public void TestGetUserSeriosityScoreDefault()
        {
            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Get())
                                   .IgnoreArguments()
                                   .Return(new HashSet<User> { this.user });

                this.configuration.Expect(config => config.DefaultScore).Return(9);
            }

            var score = this.userService.GetSeriosityScore(this.user.Id);

            Assert.AreEqual(score, 9);
        }

        /// <summary>
        /// Tests the get user seriosity score median.
        /// </summary>
        [Test]
        public void TestGetUserSeriosityScoreMedian()
        {
            this.user.ReceivedUserScores = new HashSet<UserScore>
            {
                new UserScore
                {
                    Score = 8,
                    Date = DateTime.Now,
                    ScoredUser = new User(),
                    ScoringUser = new User(),
                },

                new UserScore
                {
                    Score = 2,
                    Date = DateTime.Now,
                    ScoredUser = new User(),
                    ScoringUser = new User(),
                },

                new UserScore
                {
                    Score = 7,
                    Date = DateTime.Now,
                    ScoredUser = new User(),
                    ScoringUser = new User(),
                },
            };

            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Get())
                                   .IgnoreArguments()
                                   .Return(new HashSet<User> { this.user });
            }

            var score = this.userService.GetSeriosityScore(this.user.Id);

            Assert.AreEqual(score, 7);
        }


        /// <summary>
        /// Tests the get user seriosity score null.
        /// </summary>
        [Test]
        public void TestGetUserSeriosityScoreNull()
        {
            using (this.mocks.Record())
            {
                this.userRepository.Expect(repo => repo.Get())
                                   .IgnoreArguments()
                                   .Return(new HashSet<User>());
            }

            var score = this.userService.GetSeriosityScore(this.user.Id);

            Assert.AreEqual(score, null);
        }
    }
}
