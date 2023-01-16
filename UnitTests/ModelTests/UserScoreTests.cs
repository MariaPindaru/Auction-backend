// <copyright file="UserScoreTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ModelTests
{
    using System;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    /// <summary>
    /// Tests for entity UserScore.
    /// </summary>
    internal class UserScoreTests
    {
        /// <summary>
        /// The user score.
        /// </summary>
        private UserScore userScore;

        /// <summary>
        /// The user score validator.
        /// </summary>
        private UserScoreValidator userScoreValidator;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.userScoreValidator = new UserScoreValidator();

            this.userScore = new UserScore
            {
                Id = 1,
                Score = 9,
                Date = DateTime.Now,
                ScoringUser = new User
                {
                    Id = 1,
                    Name = "Scorrer",
                    Role = Role.Offerer,
                },
                ScoredUser = new User
                {
                    Id = 2,
                    Name = "Scored",
                    Role = Role.Bidder,
                },
            };
        }

        /// <summary>
        /// Tests the add valid user score.
        /// </summary>
        [Test]
        public void TestValidation_ValidUserScore_ReturnsNoError()
        {
            TestValidationResult<UserScore> result = this.userScoreValidator.TestValidate(this.userScore);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the null scoring user.
        /// </summary>
        [Test]
        public void TestValidation_NullScoringUser_ReturnsErrorForScoringUser()
        {
            this.userScore.ScoringUser = null;

            TestValidationResult<UserScore> result = this.userScoreValidator.TestValidate(this.userScore);
            result.ShouldHaveValidationErrorFor(score => score.ScoringUser);
        }

        /// <summary>
        /// Tests the null scored user.
        /// </summary>
        [Test]
        public void TestValidation_NullScoredUser_ReturnsErrorForScoredUser()
        {
            this.userScore.ScoredUser = null;

            TestValidationResult<UserScore> result = this.userScoreValidator.TestValidate(this.userScore);
            result.ShouldHaveValidationErrorFor(score => score.ScoredUser);
        }

        /// <summary>
        /// Tests the invalid scoring user.
        /// </summary>
        [Test]
        public void TestValidation_ScoringUserHasNullName_ReturnsErrorForScoringUserName()
        {
            this.userScore.ScoringUser.Name = null;

            TestValidationResult<UserScore> result = this.userScoreValidator.TestValidate(this.userScore);
            result.ShouldHaveValidationErrorFor(score => score.ScoringUser.Name);
        }

        /// <summary>
        /// Tests the invalid scored user.
        /// </summary>
        [Test]
        public void TestValidation_ScoredUserHasNullName_ReturnsErrorForScoredUserName()
        {
            this.userScore.ScoredUser.Name = null;

            TestValidationResult<UserScore> result = this.userScoreValidator.TestValidate(this.userScore);
            result.ShouldHaveValidationErrorFor(score => score.ScoredUser.Name);
        }

        /// <summary>
        /// Tests the negative score.
        /// </summary>
        [Test]
        public void TestValidation_HasNegativeScore_ReturnsErrorForScore()
        {
            this.userScore.Score = -1;

            TestValidationResult<UserScore> result = this.userScoreValidator.TestValidate(this.userScore);
            result.ShouldHaveValidationErrorFor(score => score.Score);
        }

        /// <summary>
        /// Tests the score too high.
        /// </summary>
        [Test]
        public void TestValidation_HasScoreTooHigh_ReturnsErrorForScore()
        {
            this.userScore.Score = 11;

            TestValidationResult<UserScore> result = this.userScoreValidator.TestValidate(this.userScore);
            result.ShouldHaveValidationErrorFor(score => score.Score);
        }

        /// <summary>
        /// Tests the score zero.
        /// </summary>
        [Test]
        public void TestValidation_HasScoreZero_ReturnsErrorForScore()
        {
            this.userScore.Score = 0;

            TestValidationResult<UserScore> result = this.userScoreValidator.TestValidate(this.userScore);
            result.ShouldHaveValidationErrorFor(score => score.Score);
        }
    }
}
