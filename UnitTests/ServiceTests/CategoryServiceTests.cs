// <copyright file="CategoryServiceTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ServiceTests
{
    using System.Collections.Generic;
    using System.Linq;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using FluentValidation.Results;
    using Ninject;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// Tests for Category service.
    /// </summary>
    internal class CategoryServiceTests
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// The category service.
        /// </summary>
        private ICategoryService categoryService;

        /// <summary>
        /// The category repository.
        /// </summary>
        private ICategoryRepository categoryRepository;

        /// <summary>
        /// The mocks.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// The category.
        /// </summary>
        private Category category;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Injector.Inject();
            this.kernel = Injector.Kernel;

            this.mocks = new MockRepository();
            this.categoryRepository = this.mocks.StrictMock<ICategoryRepository>();

            this.kernel.Rebind<ICategoryRepository>().ToConstant(this.categoryRepository);
            this.categoryService = this.kernel.Get<ICategoryService>();

            this.category = new Category();
        }

        /// <summary>
        /// Tests the add valid category.
        /// </summary>
        [Test]
        public void TestAdd_ValidCategoy_ReturnsNoError()
        {
            this.category.Name = "Electronics";

            using (this.mocks.Record())
            {
                this.categoryRepository.Expect(repo => repo.Insert(this.category));
            }

            ValidationResult result = this.categoryService.Insert(this.category);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the name of the add category with null.
        /// </summary>
        [Test]
        public void TestAdd_HasNullName_ReturnsErrorForName()
        {
            this.category.Name = null;

            using (this.mocks.Record())
            {
                this.categoryRepository.Expect(repo => repo.Insert(this.category));
            }

            ValidationResult result = this.categoryService.Insert(this.category);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Category.Name));
        }

        /// <summary>
        /// Tests the short name of the add category with.
        /// </summary>
        [Test]
        public void TestAdd_NameIsTooShort_ReturnsErrorForName()
        {
            this.category.Name = "E";

            using (this.mocks.Record())
            {
                this.categoryRepository.Expect(repo => repo.Insert(this.category));
            }

            ValidationResult result = this.categoryService.Insert(this.category);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Category.Name));
        }

        /// <summary>
        /// Tests the long name of the add category with.
        /// </summary>
        [Test]
        public void TestAdd_NameIsTooLong_ReturnsErrorForName()
        {
            string longString = new string('*', 31);
            this.category.Name = longString;

            using (this.mocks.Record())
            {
                this.categoryRepository.Expect(repo => repo.Insert(this.category));
            }

            ValidationResult result = this.categoryService.Insert(this.category);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Category.Name));
        }

        /// <summary>
        /// Tests the update valid category.
        /// </summary>
        [Test]
        public void TestUpdate_ValidCategory_ReturnsNoError()
        {
            this.category.Id = 1;
            this.category.Name = "ValidName";

            using (this.mocks.Record())
            {
                this.categoryRepository.Expect(repo => repo.Update(this.category));
            }

            ValidationResult result = this.categoryService.Update(this.category);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the name of the update category with null.
        /// </summary>
        [Test]
        public void TestUpdate_HasNullName_ReturnsErrorForName()
        {
            this.category.Name = null;

            using (this.mocks.Record())
            {
                this.categoryRepository.Expect(repo => repo.Update(this.category));
            }

            ValidationResult result = this.categoryService.Update(this.category);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Category.Name));
        }

        /// <summary>
        /// Tests the short name of the update category with.
        /// </summary>
        [Test]
        public void TestUpdate_NameIsTooShort_ReturnsErrorForName()
        {
            this.category.Name = "E";

            using (this.mocks.Record())
            {
                this.categoryRepository.Expect(repo => repo.Update(this.category));
            }

            ValidationResult result = this.categoryService.Update(this.category);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Category.Name));
        }

        /// <summary>
        /// Tests the long name of the update category with.
        /// </summary>
        [Test]
        public void TestUpdate_NameIsTooLong_ReturnsErrorForName()
        {
            string longString = new string('*', 31);
            this.category.Name = longString;

            using (this.mocks.Record())
            {
                this.categoryRepository.Expect(repo => repo.Update(this.category));
            }

            ValidationResult result = this.categoryService.Update(this.category);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Category.Name));
        }

        /// <summary>
        /// Tests the delete valid category.
        /// </summary>
        [Test]
        public void TestDelete()
        {
            this.category.Id = 1;
            this.category.Name = "ValidName";

            using (this.mocks.Record())
            {
                this.categoryRepository.Expect(repo => repo.Delete(this.category));
            }

            this.categoryService.Delete(this.category);
        }

        /// <summary>
        /// Tests the get categories.
        /// </summary>
        [Test]
        public void TestGetAll_ReturnsCurrentCategory()
        {
            using (this.mocks.Record())
            {
                this.categoryRepository.Expect(repo => repo.Get()).Return(new HashSet<Category> { this.category });
            }

            var products = this.categoryService.GetAll();

            Assert.AreEqual(products.ToList().Count, 1);
            Assert.AreEqual(products.ToList().First(), this.category);
        }

        /// <summary>
        /// Tests the get category by identifier.
        /// </summary>
        [Test]
        public void TestGetById_ReturnsCurrentCategory()
        {
            using (this.mocks.Record())
            {
                this.categoryRepository.Expect(repo => repo.GetByID(10)).Return(this.category);
            }

            var category = this.categoryService.GetByID(10);
            Assert.AreEqual(category, this.category);
        }

        /// <summary>
        /// Tests the get by identifier null identifier returns null.
        /// </summary>
        [Test]
        public void TestGetById_NullId_ReturnsNull()
        {
            using (this.mocks.Record())
            {
                this.categoryRepository.Expect(repo => repo.GetByID(null)).Return(null);
            }

            var result = this.categoryService.GetByID(null);

            Assert.AreEqual(result, null);
        }
    }
}
