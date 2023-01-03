using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Rhino.Mocks;
using AuctionBackend.DomainLayer.DomainModel;
using FluentValidation.Results;
using AuctionBackend.Startup;
using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;

namespace UnitTests.ServiceTests
{
    class CategoryServiceTests
    {
        private IKernel kernel;
        private ICategoryService categoryService;

        private ICategoryRepository categoryRepository;
        private MockRepository mocks;

        private Category category;

        [SetUp]
        public void Setup()
        {
            Injector.Inject();
            this.kernel = Injector.Kernel;

            this.mocks = new MockRepository();
            this.categoryRepository = mocks.StrictMock<ICategoryRepository>();

            this.kernel.Rebind<ICategoryRepository>().ToConstant(categoryRepository);
            this.categoryService = kernel.Get<ICategoryService>();

            this.category = new Category();
        }

        [Test]
        public void TestAddValidCategory()
        {
            this.category.Name = "Electronics";

            using (this.mocks.Record())
            {
                categoryRepository.Expect(repo => repo.Insert(this.category));
            }

            ValidationResult result = categoryService.Insert(this.category);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void TestAddCategoryWithNullName()
        {
            this.category.Name = null;

            using (mocks.Record())
            {
                categoryRepository.Expect(repo => repo.Insert(this.category));
            }

            ValidationResult result = categoryService.Insert(this.category);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestAddCategoryWithShortName()
        {
            this.category.Name = "E";

            using (mocks.Record())
            {
                categoryRepository.Expect(repo => repo.Insert(this.category));
            }

            ValidationResult result = categoryService.Insert(this.category);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestAddCategoryWithLongName()
        {
            string longString = new string('*', 31);
            this.category.Name = longString;

            using (mocks.Record())
            {
                categoryRepository.Expect(repo => repo.Insert(this.category));
            }

            ValidationResult result = categoryService.Insert(this.category);

            Assert.IsFalse(result.IsValid);
        }


        [Test]
        public void TestUpdateValidCategory()
        {
            this.category.Id = 1;
            this.category.Name = "ValidName";

            using (mocks.Record())
            {
                categoryRepository.Expect(repo => repo.Update(this.category));
            }

            ValidationResult result = categoryService.Update(this.category);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void TestUpdateCategoryWithNullName()
        {
            this.category.Name = null;

            using (mocks.Record())
            {
                categoryRepository.Expect(repo => repo.Update(this.category));
            }

            ValidationResult result = categoryService.Update(this.category);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestUpdateCategoryWithShortName()
        {
            this.category.Name = "E";

            using (mocks.Record())
            {
                categoryRepository.Expect(repo => repo.Update(this.category));
            }

            ValidationResult result = categoryService.Update(this.category);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestUpdateCategoryWithLongName()
        {
            string longString = new string('*', 31);
            this.category.Name = longString;

            using (mocks.Record())
            {
                categoryRepository.Expect(repo => repo.Update(this.category));
            }

            ValidationResult result = categoryService.Update(this.category);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestDeleteValidCategory()
        {
            this.category.Id = 1;
            this.category.Name = "ValidName";

            using (mocks.Record())
            {
                categoryRepository.Expect(repo => repo.Delete(this.category));
            }

            categoryService.Delete(this.category);
        }

        [Test]
        public void TestGetCategories()
        {
            using (this.mocks.Record())
            {
                categoryRepository.Expect(repo => repo.Get()).Return(new HashSet<Category> { this.category });
            }

            var products = categoryService.GetAll();

            Assert.AreEqual(products.ToList().Count, 1);
            Assert.AreEqual(products.ToList().First(), this.category);
        }

        [Test]
        public void TestGetCategoryById()
        {
            using (this.mocks.Record())
            {
                categoryRepository.Expect(repo => repo.GetByID(10)).Return(this.category);
            }

            var category = categoryService.GetByID(10);
            Assert.AreEqual(category, this.category);
        }
    }
}
