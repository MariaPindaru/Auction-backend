﻿using AuctionBackend.DomainLayer.BL;
using AuctionBackend.DomainLayer.BL.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Rhino.Mocks;
using AuctionBackend.DomainLayer.DomainModel;
using FluentValidation.Results;
using AuctionBackend.DataLayer.DAL.Interfaces;
using AuctionBackend.Startup;
using AuctionBackend.DomainLayer.BL;
using AuctionBackend.DomainLayer.DomainModel.Validators;

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
    }
}
