// <copyright file="UserTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ModelTests
{
    using System;
    using System.Linq;
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
            };
        }

        /// <summary>
        /// Tests the valid user.
        /// </summary>
        [Test]
        public void TestValidation_ValidUser_ReturnsNoErrors()
        {
            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the null name.
        /// </summary>
        [Test]
        public void TestValidation_HasNullName_ReturnsErrorForName()
        {
            this.user.Name = null;
            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Name);
        }

        /// <summary>
        /// Tests the short name.
        /// </summary>
        [Test]
        public void TestValidation_HasNameTooShort_ReturnsErrorForName()
        {
            this.user.Name = "N";
            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Name);
        }

        /// <summary>
        /// Tests the long name.
        /// </summary>
        [Test]
        public void TestValidation_HasNameTooLong_ReturnsErrorForName()
        {
            string longString = new string('*', 51);
            this.user.Name = longString;

            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Name);
        }

        /// <summary>
        /// Tests the validation has bidder role returns no error.
        /// </summary>
        [Test]
        public void TestValidation_HasBidderRole_ReturnsNoError()
        {
            this.user.Role = Role.Bidder;

            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the validation has offerer role returns no error.
        /// </summary>
        [Test]
        public void TestValidation_HasOffererRole_ReturnsNoError()
        {
            this.user.Role = Role.Bidder;

            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the validation has both roles returns no error.
        /// </summary>
        [Test]
        public void TestValidation_HasBothRoles_ReturnsNoError()
        {
            this.user.Role = Role.Bidder | Role.Offerer;
            Assert.AreEqual(Role.Bidder | Role.Offerer, Role.OffererAndBidder);

            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the invalid role.
        /// </summary>
        [Test]
        public void TestValidation_HasInvalidRole_ReturnsErrorForRole()
        {
            var invalidRoleValue = Enum.GetValues(typeof(Role)).Cast<int>().Max() + 1;
            this.user.Role = (Role)invalidRoleValue;

            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Role);
        }
    }
}
