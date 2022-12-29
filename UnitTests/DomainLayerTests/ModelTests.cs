using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    using Auction.DomainLayer.DomainModel;
    class ModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateCategory()
        {
            Category category = new Category();
            category.Name = "hehe";
            Assert.NotNull(category);
        }
    }
}
