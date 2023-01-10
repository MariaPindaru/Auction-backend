// <copyright file="UserScoreServiceTests.cs" company="Transilvania University of Brasov">
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
    /// UserScoreServiceTests.
    /// </summary>
    internal class UserScoreServiceTests
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// The user score service.
        /// </summary>
        private IUserScoreService userScoreService;

        /// <summary>
        /// The user score repository.
        /// </summary>
        private IUserScoreRepository userScoreRepository;

        /// <summary>
        /// The configuration.
        /// </summary>
        private IConfiguration configuration;

        /// <summary>
        /// The mocks.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// The user score.
        /// </summary>
        private UserScore userScore;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Injector.Inject();

            this.mocks = new MockRepository();
            this.kernel = Injector.Kernel;

            this.userScoreRepository = this.mocks.StrictMock<IUserScoreRepository>();
            this.kernel.Rebind<IUserScoreRepository>().ToConstant(this.userScoreRepository);

            this.configuration = this.mocks.StrictMock<IConfiguration>();
            this.kernel.Rebind<IConfiguration>().ToConstant(this.configuration);

            this.userScoreService = this.kernel.Get<IUserScoreService>();

            this.userScore = new UserScore
            {
                Id = 10,
                Score = 7,
                ScoringUser = new User
                {
                    Id = 1,
                    Name = "Scoring user",
                    Role = Role.Bidder,
                },
                ScoredUser = new User
                {
                    Id = 3,
                    Name = "Scored user",
                    Role = Role.Bidder,
                },
            };
        }

        /// <summary>
        /// Tests the add valid user score.
        /// </summary>
        [Test]
        public void TestAddValidUserScore()
        {
            using (this.mocks.Record())
            {
                this.userScoreRepository.Expect(repo => repo.Insert(this.userScore));
            }

            ValidationResult result = this.userScoreService.Insert(this.userScore);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the add user score with null scoring user.
        /// </summary>
        [Test]
        public void TestAddUserScoreWithNullScoringUser()
        {
            this.userScore.ScoringUser = null;
            using (this.mocks.Record())
            {
                this.userScoreRepository.Expect(repo => repo.Insert(this.userScore));
            }

            ValidationResult result = this.userScoreService.Insert(this.userScore);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add user score with invalid scoring user.
        /// </summary>
        [Test]
        public void TestAddUserScoreWithInvalidScoringUser()
        {
            var maxRoleValue = Enum.GetValues(typeof(Role)).Cast<int>().Max() + 1;
            this.userScore.ScoringUser.Role = (Role)maxRoleValue;
            using (this.mocks.Record())
            {
                this.userScoreRepository.Expect(repo => repo.Insert(this.userScore));
            }

            ValidationResult result = this.userScoreService.Insert(this.userScore);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add user score with null scored user.
        /// </summary>
        [Test]
        public void TestAddUserScoreWithNullScoredUser()
        {
            this.userScore.ScoredUser = null;
            using (this.mocks.Record())
            {
                this.userScoreRepository.Expect(repo => repo.Insert(this.userScore));
            }

            ValidationResult result = this.userScoreService.Insert(this.userScore);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add user score with invalid scored user.
        /// </summary>
        [Test]
        public void TestAddUserScoreWithInvalidScoredUser()
        {
            var maxRoleValue = Enum.GetValues(typeof(Role)).Cast<int>().Max() + 1;
            this.userScore.ScoredUser.Role = (Role)maxRoleValue;
            using (this.mocks.Record())
            {
                this.userScoreRepository.Expect(repo => repo.Insert(this.userScore));
            }

            ValidationResult result = this.userScoreService.Insert(this.userScore);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add user score with negative score.
        /// </summary>
        [Test]
        public void TestAddUserScoreWithNegativeScore()
        {
            this.userScore.Score = -2;
            using (this.mocks.Record())
            {
                this.userScoreRepository.Expect(repo => repo.Insert(this.userScore));
            }

            ValidationResult result = this.userScoreService.Insert(this.userScore);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add user score with score too high.
        /// </summary>
        [Test]
        public void TestAddUserScoreWithScoreTooHigh()
        {
            this.userScore.Score = 232;
            using (this.mocks.Record())
            {
                this.userScoreRepository.Expect(repo => repo.Insert(this.userScore));
            }

            ValidationResult result = this.userScoreService.Insert(this.userScore);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update valid user score.
        /// </summary>
        [Test]
        public void TestUpdateValidUserScore()
        {
            using (this.mocks.Record())
            {
                this.userScoreRepository.Expect(repo => repo.Update(this.userScore));
            }

            ValidationResult result = this.userScoreService.Update(this.userScore);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the update user with negative score.
        /// </summary>
        [Test]
        public void TestUpdateUserWithNegativeScore()
        {
            this.userScore.Score = -9;
            using (this.mocks.Record())
            {
                this.userScoreRepository.Expect(repo => repo.Update(this.userScore));
            }

            ValidationResult result = this.userScoreService.Update(this.userScore);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add user score with score zero.
        /// </summary>
        [Test]
        public void TestAddUserScoreWithScoreZero()
        {
            this.userScore.Score = 232;
            using (this.mocks.Record())
            {
                this.userScoreRepository.Expect(repo => repo.Insert(this.userScore));
            }

            ValidationResult result = this.userScoreService.Insert(this.userScore);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the get user scores.
        /// </summary>
        [Test]
        public void TestGetUserScores()
        {
            using (this.mocks.Record())
            {
                this.userScoreRepository.Expect(repo => repo.Get()).Return(new HashSet<UserScore> { this.userScore });
            }

            var users = this.userScoreService.GetAll();

            Assert.AreEqual(users.ToList().Count, 1);
            Assert.AreEqual(users.ToList().First(), this.userScore);
        }

        /// <summary>
        /// Tests the get user score by identifier.
        /// </summary>
        [Test]
        public void TestGetUserScoreById()
        {
            using (this.mocks.Record())
            {
                this.userScoreRepository.Expect(repo => repo.GetByID(10)).Return(this.userScore);
            }

            var product = this.userScoreService.GetByID(10);

            Assert.AreEqual(product, this.userScore);
        }
    }
}
