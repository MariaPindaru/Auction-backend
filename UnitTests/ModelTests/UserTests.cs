using AuctionBackend.DomainLayer.DomainModel;
using AuctionBackend.DomainLayer.DomainModel.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace UnitTests.ModelTests
{
    class UserTests
    {
        private User user;
        private UserValidator userValidator;

        [SetUp]
        public void Setup()
        {
            this.userValidator = new UserValidator();
            this.user = new User
            {
                Name = "Username",
                Role = Role.Bidder,
                Score = 30.43f
            };
        }

        [Test]
        public void TestValidUser()
        {
            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void TestNullName()
        {
            this.user.Name = null;
            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Name);
        }

        [Test]
        public void TestShortName()
        {
            this.user.Name = "N";
            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Name);
        }

        [Test]
        public void TestLongName()
        {
            string longString = new string('*', 51);
            this.user.Name = longString;

            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Name);
        }


        [Test]
        public void TestInvalidRole()
        {
            this.user.Role = (Role)2;

            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Role);
        }

        [Test]
        public void TestNegativeScore()
        {
            this.user.Score = -2.2f;

            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Score);
        }

        [Test]
        public void TestOutOfRangeScore()
        {
            this.user.Score = 200.2f;

            TestValidationResult<User> result = this.userValidator.TestValidate(this.user);
            result.ShouldHaveValidationErrorFor(user => user.Score);
        }
    }
}
