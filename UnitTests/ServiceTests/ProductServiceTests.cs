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
                Category = this.category,
                Name = "TV",
                Description = "you can watch the tv :)",
            };
        }

        /// <summary>
        /// Tests the add valid product.
        /// </summary>
        [Test]
        public void TestAddValidProduct()
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
        public void TestAddProductNullName()
        {
            this.product.Name = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the short name of the add product.
        /// </summary>
        [Test]
        public void TestAddProductShortName()
        {
            this.product.Name = "E";

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the long name of the add product.
        /// </summary>
        [Test]
        public void TestAddProductLongName()
        {
            string longString = new string('*', 101);
            this.product.Name = longString;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add product null description.
        /// </summary>
        [Test]
        public void TestAddProductNullDescription()
        {
            this.product.Description = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add product short description.
        /// </summary>
        [Test]
        public void TestAddProductShortDescription()
        {
            this.product.Description = "E";

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add product long description.
        /// </summary>
        [Test]
        public void TestAddProductLongDescription()
        {
            string longString = new string('*', 501);
            this.product.Description = longString;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the add product null category.
        /// </summary>
        [Test]
        public void TestAddProductNullCategory()
        {
            this.product.Category = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the name of the add product null category.
        /// </summary>
        [Test]
        public void TestAddProductNullCategoryName()
        {
            this.product.Category.Name = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update valid product.
        /// </summary>
        [Test]
        public void TestUpdateValidProduct()
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
        public void TestUpdateProductNullName()
        {
            this.product.Name = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the short name of the update product.
        /// </summary>
        [Test]
        public void TestUpdateProductShortName()
        {
            this.product.Name = "E";

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = this.productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the long name of the update product.
        /// </summary>
        [Test]
        public void TestUpdateProductLongName()
        {
            string longString = new string('*', 101);
            this.product.Name = longString;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update product null description.
        /// </summary>
        [Test]
        public void TestUpdateProductNullDescription()
        {
            this.product.Description = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update product short description.
        /// </summary>
        [Test]
        public void TestUpdateProductShortDescription()
        {
            this.product.Description = "E";

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update product long description.
        /// </summary>
        [Test]
        public void TestUpdateProductLongDescription()
        {
            string longString = new string('*', 501);
            this.product.Description = longString;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the update product null category.
        /// </summary>
        [Test]
        public void TestUpdateProductNullCategory()
        {
            this.product.Category = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the name of the update product null category.
        /// </summary>
        [Test]
        public void TestUpdateProductNullCategoryName()
        {
            this.product.Category.Name = null;

            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = this.productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the delete product.
        /// </summary>
        [Test]
        public void TestDeleteProduct()
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
        public void TestGetProducts()
        {
            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.Get()).Return(new HashSet<Product> { this.product });
            }

            var products = this.productService.GetAll();

            Assert.AreEqual(products.ToList().Count, 1);
            Assert.AreEqual(products.ToList().First(), this.product);
        }

        /// <summary>
        /// Tests the get product by identifier.
        /// </summary>
        [Test]
        public void TestGetProductById()
        {
            using (this.mocks.Record())
            {
                this.productRepository.Expect(repo => repo.GetByID(10)).Return(this.product);
            }

            var product = this.productService.GetByID(10);

            Assert.AreEqual(product, this.product);
        }
    }
}
