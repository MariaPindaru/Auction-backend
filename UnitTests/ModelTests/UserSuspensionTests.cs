// <copyright file="UserSuspensionTests.cs" company="Transilvania University of Brasov">
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
    /// UserSuspensionTests.
    /// </summary>
    internal class UserSuspensionTests
    {
        /// <summary>
        /// The user suspension.
        /// </summary>
        private UserSuspension userSuspension;

        /// <summary>
        /// The user suspension validator.
        /// </summary>
        private UserSuspensionValidator userSuspensionValidator;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.userSuspensionValidator = new UserSuspensionValidator();

            this.userSuspension = new UserSuspension
            {
                User = new User
                {
                    Name = "blabla",
                    Role = Role.Bidder,
                },
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(7),
            };
        }

        /// <summary>
        /// Tests the validation valid user suspension returns no errors.
        /// </summary>
        [Test]
        public void TestValidation_ValidUserSuspension_ReturnsNoErrors()
        {
            TestValidationResult<UserSuspension> result = this.userSuspensionValidator.TestValidate(this.userSuspension);
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests the validation has null user returns error for user.
        /// </summary>
        [Test]
        public void TestValidation_HasNullUser_ReturnsErrorForUser()
        {
            this.userSuspension.User = null;
            TestValidationResult<UserSuspension> result = this.userSuspensionValidator.TestValidate(this.userSuspension);
            result.ShouldHaveValidationErrorFor(us => us.User);
        }

        /// <summary>
        /// Tests the name of the validation user has null name returns error for user.
        /// </summary>
        [Test]
        public void TestValidation_UserHasNullName_ReturnsErrorForUserName()
        {
            this.userSuspension.User.Name = null;
            TestValidationResult<UserSuspension> result = this.userSuspensionValidator.TestValidate(this.userSuspension);
            result.ShouldHaveValidationErrorFor(us => us.User.Name);
        }

        /// <summary>
        /// Tests the validation has default start date returns error for start date.
        /// </summary>
        [Test]
        public void TestValidation_HasDefaultStartDate_ReturnsErrorForStartDate()
        {
            this.userSuspension.StartDate = default;
            TestValidationResult<UserSuspension> result = this.userSuspensionValidator.TestValidate(this.userSuspension);
            result.ShouldHaveValidationErrorFor(us => us.StartDate);
        }

        /// <summary>
        /// Tests the validation has start date in the past returns error for start date.
        /// </summary>
        [Test]
        public void TestValidation_HasStartDateInThePast_ReturnsErrorForStartDate()
        {
            this.userSuspension.StartDate = DateTime.Now.AddDays(-2);
            TestValidationResult<UserSuspension> result =
                this.userSuspensionValidator.TestValidate(this.userSuspension, options => options.IncludeRuleSets("default", "Add"));
            result.ShouldHaveValidationErrorFor(us => us.StartDate);
        }

        /// <summary>
        /// Tests the validation has default end date returns error for end date.
        /// </summary>
        [Test]
        public void TestValidation_HasDefaultEndDate_ReturnsErrorForEndDate()
        {
            this.userSuspension.EndDate = default;
            TestValidationResult<UserSuspension> result = this.userSuspensionValidator.TestValidate(this.userSuspension);
            result.ShouldHaveValidationErrorFor(us => us.EndDate);
        }

        /// <summary>
        /// Tests the validation has end date before start date returns error for end date.
        /// </summary>
        [Test]
        public void TestValidation_HasEndDateBeforeStartDate_ReturnsErrorForEndDate()
        {
            this.userSuspension.StartDate = DateTime.Now.AddDays(2);
            this.userSuspension.EndDate = DateTime.Now.AddDays(1);
            TestValidationResult<UserSuspension> result = this.userSuspensionValidator.TestValidate(this.userSuspension);
            result.ShouldHaveValidationErrorFor(us => us.EndDate);
        }

        /// <summary>
        /// Tests the validation end date eqauls start date returns error for end date.
        /// </summary>
        [Test]
        public void TestValidation_EndDateEqaulsStartDate_ReturnsErrorForEndDate()
        {
            this.userSuspension.StartDate = DateTime.Now.AddDays(1);
            this.userSuspension.EndDate = DateTime.Now.AddDays(1);
            TestValidationResult<UserSuspension> result = this.userSuspensionValidator.TestValidate(this.userSuspension);
            result.ShouldHaveValidationErrorFor(us => us.EndDate);
        }
    }
}
