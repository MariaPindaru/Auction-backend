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
using AuctionBackend.Startup;
using AuctionBackend.DomainLayer.DomainModel.Validators;
using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;

namespace UnitTests.ServiceTests
{
    class ProductServiceTests
    {
        private IKernel kernel;
        private IProductService productService;

        private IProductRepository productRepository;
        private MockRepository mocks;

        private Product product;
        private Category category;


        [SetUp]
        public void Setup()
        {
            Injector.Inject();
            this.kernel = Injector.Kernel;

            this.mocks = new MockRepository();
            this.productRepository = mocks.StrictMock<IProductRepository>();

            this.kernel.Rebind<IProductRepository>().ToConstant(this.productRepository);
            this.productService = kernel.Get<IProductService>();

            this.category = new Category
            {
                Name = "Electronics"
            };
            this.product = new Product
            {
                Category = this.category,
                Name = "TV",
                Description = "you can watch the tv :)",
            };
        }

        [Test]
        public void TestAddValidProduct()
        {
            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = productService.Insert(this.product);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void TestAddProductNullName()
        {
            this.product.Name = null;

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestAddProductShortName()
        {
            this.product.Name = "E";

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestAddProductLongName()
        {
            string longString = new string('*', 101);
            this.product.Name = longString;

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestAddProductNullDescription()
        {
            this.product.Description = null;

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestAddProductShortDescription()
        {
            this.product.Description = "E";

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestAddProductLongDescription()
        {
            string longString = new string('*', 501);
            this.product.Description = longString;

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestAddProductNullCategory()
        {
            this.product.Category = null;

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestAddProductNullCategoryName()
        {
            this.product.Category.Name = null;

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestUpdateValidProduct()
        {
            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = productService.Update(this.product);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void TestUpdateProductNullName()
        {
            this.product.Name = null;

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestUpdateProductShortName()
        {
            this.product.Name = "E";

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Insert(this.product));
            }

            ValidationResult result = productService.Insert(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestUpdateProductLongName()
        {
            string longString = new string('*', 101);
            this.product.Name = longString;

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestUpdateProductNullDescription()
        {
            this.product.Description = null;

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestUpdateProductShortDescription()
        {
            this.product.Description = "E";

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestUpdateProductLongDescription()
        {
            string longString = new string('*', 501);
            this.product.Description = longString;

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestUpdateProductNullCategory()
        {
            this.product.Category = null;

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestUpdateProductNullCategoryName()
        {
            this.product.Category.Name = null;

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Update(this.product));
            }

            ValidationResult result = productService.Update(this.product);

            Assert.IsFalse(result.IsValid);
        }


        [Test]
        public void TestDeleteProduct()
        {
            this.product.Category.Name = null;

            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Delete(this.product));
            }

            productService.Delete(this.product);
        }

        [Test]
        public void TestGetProducts()
        {
            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.Get()).Return(new HashSet<Product> { this.product });
            }

            var products = productService.GetAll();

            Assert.AreEqual(products.ToList().Count, 1);
            Assert.AreEqual(products.ToList().First(), this.product);
        }

        [Test]
        public void TestGetProductById()
        {
            using (this.mocks.Record())
            {
                productRepository.Expect(repo => repo.GetByID(10)).Return(this.product);
            }

            var product = productService.GetByID(10);

            Assert.AreEqual(product, this.product);
        }
    }
}
