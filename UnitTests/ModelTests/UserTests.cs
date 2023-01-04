// <copyright file="UserTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ModelTests
{
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    /// <summary>
    /// UserTests.
    /// </summary>
    internal class UserTests
    {
        /// <summary>
        /// The user.
        /// </summary>
        private User user;

        /// <summary>
        /// The user validator.
        /// </summary>
        private UserValidator userValidator;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.userValidator = new UserValidator();
            this.user = new User
            {
                Name = "Username",
                Role = Role.Bidder,
                Score = 30.43f,
            };
        }

        /// <summary>
        /// Tests the valid user.
        /// </summary>
        [Test]
        public void TestValidUser()
        {
            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the null name.
        /// </summary>
        [Test]
        public void TestNullName()
        {
            this.user.Name = null;
            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Name);
        }

        /// <summary>
        /// Tests the short name.
        /// </summary>
        [Test]
        public void TestShortName()
        {
            this.user.Name = "N";
            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Name);
        }

        /// <summary>
        /// Tests the long name.
        /// </summary>
        [Test]
        public void TestLongName()
        {
            string longString = new string('*', 51);
            this.user.Name = longString;

            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Name);
        }

        /// <summary>
        /// Tests the invalid role.
        /// </summary>
        [Test]
        public void TestInvalidRole()
        {
            this.user.Role = (Role)20;

            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Role);
        }

        /// <summary>
        /// Tests the negative score.
        /// </summary>
        [Test]
        public void TestNegativeScore()
        {
            this.user.Score = -2.2f;

            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Score);
        }

        /// <summary>
        /// Tests the out of range score.
        /// </summary>
        [Test]
        public void TestOutOfRangeScore()
        {
            this.user.Score = 200.2f;

            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Score);
        }
    }
}
