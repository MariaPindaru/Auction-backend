// <copyright file="ProductServiceTests.cs" company="Transilvania University of Brasov">
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
    /// ProductServiceTests.
    /// </summary>
    internal class ProductServiceTests
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// The product service.
        /// </summary>
        private IProductService productService;

        /// <summary>
        /// The product repository.
        /// </summary>
        private IProductRepository productRepository;

        /// <summary>
        /// The mocks.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// The product.
        /// </summary>
        private Product product;

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
            this.productRepository = this.mocks.StrictMock<IProductRepository>();

            this.kernel.Rebind<IProductRepository>().ToConstant(this.productRepository);
            this.productService = this.kernel.Get<IProductService>();

            this.category = new Category
            {
                Name = "Electronics",
            };
            this.product = new Product
            {
                Id = 0,
                Category = this.category,
                Name = "TV",
                Description = "you can watch the tv :)",
                Offerer = new User
                {
                    Name = "Offerer",
                    Role = Role.Offerer,
                },
            };
        }

        /// <summary>
        /// Tests the add valid product.
        /// </summary>
        [Test]
        public void TestAdd_ValidProduct_ReturnsNoError()
        {
            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the name of the add product null.
        /// </summary>
        [Test]
        public void TestAdd_HasNullName_ReturnsErrorForName()
        {
            this.product.Name = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Name));
        }

        /// <summary>
        /// Tests the short name of the add product.
        /// </summary>
        [Test]
        public void TestAdd_NameIsTooShort_ReturnsErrorForName()
        {
            this.product.Name = "E";

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Name));
        }

        /// <summary>
        /// Tests the long name of the add product.
        /// </summary>
        [Test]
        public void TestAdd_NameIsTooLong_ReturnsErrorForName()
        {
            string longString = new string('*', 101);
            this.product.Name = longString;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Name));
        }

        /// <summary>
        /// Tests the add product null description.
        /// </summary>
        [Test]
        public void TestAdd_DescriptionIsNull_ReturnsErrorForDescription()
        {
            this.product.Description = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Description));
        }

        /// <summary>
        /// Tests the add product short description.
        /// </summary>
        [Test]
        public void TestAdd_DescriptionIsTooShort_ReturnsErrorForDescription()
        {
            this.product.Description = "E";

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Description));
        }

        /// <summary>
        /// Tests the add product long description.
        /// </summary>
        [Test]
        public void TestAdd_DescriptionIsTooLong_ReturnsErrorForDescription()
        {
            string longString = new string('*', 501);
            this.product.Description = longString;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Description));
        }

        /// <summary>
        /// Tests the add product null category.
        /// </summary>
        [Test]
        public void TestAdd_CategoryIsNull_ReturnsErrorForCategory()
        {
            this.product.Category = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Category));
        }

        /// <summary>
        /// Tests the name of the add product null category.
        /// </summary>
        [Test]
        public void TestAdd_CategoryNameIsNull_ReturnsErrorForCategoryName()
        {
            this.product.Category.Name = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, "Category.Name");
        }

        /// <summary>
        /// Tests the add auction with null offerer.
        /// </summary>
        [Test]
        public void TestAdd_HasNullOfferer_ReturnsErrorForOfferer()
        {
            this.product.Offerer = null;
            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Offerer));
        }

        /// <summary>
        /// Tests the add auction with invalid offerer.
        /// </summary>
        [Test]
        public void TestAdd_OffererHasNullName_ReturnsErrorForOffererName()
        {
            this.product.Offerer.Name = null;
            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
                this.productRepository.Expect(repo => repo.Get())
                    .IgnoreArguments()
                    .Return(new HashSet<Product> {this.product });
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, "Offerer.Name");
        }

        /// <summary>
        /// Tests the update valid product.
        /// </summary>
        [Test]
        public void TestUpdate_ValidProduct_ReturnsNoError()
        {
            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the name of the update product null.
        /// </summary>
        [Test]
        public void TestUpdate_HasNullName_ReturnsErrorForName()
        {
            this.product.Name = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Name));
        }

        /// <summary>
        /// Tests the short name of the update product.
        /// </summary>
        [Test]
        public void TestUpdate_NameIsTooShort__ReturnsErrorForName()
        {
            this.product.Name = "E";

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Name));
        }

        /// <summary>
        /// Tests the long name of the update product.
        /// </summary>
        [Test]
        public void TestUpdate_NameIsTooLong__ReturnsErrorForName()
        {
            string longString = new string('*', 101);
            this.product.Name = longString;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Name));
        }

        /// <summary>
        /// Tests the update product null description.
        /// </summary>
        [Test]
        public void TestUpdate_DescriptionIsNull_ReturnsErrorForDescription()
        {
            this.product.Description = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Description));
        }

        /// <summary>
        /// Tests the update product short description.
        /// </summary>
        [Test]
        public void TestUpdate_DescriptionIsTooShort_ReturnsErrorForDescription()
        {
            this.product.Description = "E";

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Description));
        }

        /// <summary>
        /// Tests the update product long description.
        /// </summary>
        [Test]
        public void TestUpdate_DescriptionIsTooLong_ReturnsErrorForDescription()
        {
            string longString = new string('*', 501);
            this.product.Description = longString;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Description));
        }

        /// <summary>
        /// Tests the update product null category.
        /// </summary>
        [Test]
        public void TestUpdate_CategoryIsNull_ReturnsErrorForCategory()
        {
            this.product.Category = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Category));
        }

        /// <summary>
        /// Tests the name of the update product null category.
        /// </summary>
        [Test]
        public void TestUpdate_CategoryNameIsNull_ReturnsErrorForCategoryName()
        {
            this.product.Category.Name = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors.First().PropertyName, "Category.Name");
        }

        /// <summary>
        /// Tests the delete product.
        /// </summary>
        [Test]
        public void TestDelete_ValidProduct_ReturnsNoError()
        {
            this.product.Category.Name = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Delete(this.product));
            }

            this.productService.Delete(this.product);
        }

        /// <summary>
        /// Tests the get products.
        /// </summary>
        [Test]
        public void TestGetAll_ReturnsCurrentProduct()
        {
            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Get()).Return(new HashSet<Product> { this.product });
            }

            var products = this.productService.GetAll();

            Assert.AreEqual(products.Count(), 1);
            Assert.AreEqual(products.First(), this.product);
        }

        /// <summary>
        /// Tests the get product by identifier.
        /// </summary>
        [Test]
        public void TestGetById_ReturnsCurrentProduct()
        {
            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.GetByID(10)).Return(this.product);
            }

            var product = this.productService.GetByID(10);

            Assert.AreEqual(product, this.product);
        }

        /// <summary>
        /// Tests the add product with identical description.
        /// </summary>
        //[Test]
        //public void TestAdd_DescriptionIsIdenticalWithAnotherOne_SameUser_ReturnsErrorForDescription()
        //{
        //    using (this.mocks.Record())
        //    {
        //        this.productRepository.Expect(repo => repo.Insert(this.product));
        //        this.productRepository.Expect(repo => repo.Get())
        //                              .IgnoreArguments()
        //                              .Return(new HashSet<Product>
        //                              {
        //                                  new Product
        //                                  {
        //                                      Id = 90,
        //                                      Description = this.product.Description,
        //                                  },
        //                              });
        //    }

        //    ValidationResult result = this.productService.Insert(this.product);

        //    Assert.IsFalse(result.IsValid);
        //    Assert.AreEqual(result.Errors.Count, 1);
        //    Assert.AreEqual(result.Errors.First().PropertyName, nameof(Product.Description));
        //}

        /// <summary>
        /// Tests the add product with similar description.
        /// </summary>
        //[Test]
        //public void TestAdd_DescriptionIsSimilarWithAnotherOne_SameUser_ReturnsNoError()
        //{
        //    using (this.mocks.Record())
        //    {
        //        //this.product.Auction = new Auction
        //        //{
        //        //    Offerer = new User(),
        //        //};
        //        this.product.Description = "hihi";
        //        this.productRepository.Expect(repo => repo.Insert(this.product));
        //        this.productRepository.Expect(repo => repo.Get())
        //                              .IgnoreArguments()
        //                              .Return(new HashSet<Product>
        //                              {
        //                                  new Product
        //                                  {
        //                                      Id = 90,
        //                                      Description = "haha",
        //                                  },
        //                              });
        //    }

        //    ValidationResult result = this.productService.Insert(this.product);

        //    Assert.IsTrue(result.IsValid);
        //}
    }
}
