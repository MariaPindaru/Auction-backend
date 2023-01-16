// <copyright file="LevenshteinDistanceTests.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests
{
    using AuctionBackend.DomainLayer.ServiceLayer.Utils;
    using NUnit.Framework;

    /// <summary>
    /// Tests for LevenshteinDistance.
    /// </summary>
    internal class LevenshteinDistanceTests
    {
        /// <summary>
        /// Tests the identical strings.
        /// </summary>
        [Test]
        public void TestIdenticalStrings()
        {
            var stringValue = "String value";
            var result = LevenshteinDistance.Calculate(stringValue, stringValue);

            Assert.AreEqual(result, 0);
        }

        /// <summary>
        /// Tests the different strings.
        /// </summary>
        [Test]
        public void TestDifferentStrings()
        {
            var source = "apals";
            var target = "tgdbr2";
            var result = LevenshteinDistance.Calculate(source, target);

            Assert.AreEqual(result, target.Length);
        }

        /// <summary>
        /// Tests the similar strings.
        /// </summary>
        [Test]
        public void TestSimilarStrings()
        {
            var source = "haha";
            var target = "hihi";
            var result = LevenshteinDistance.Calculate(source, target);

            Assert.AreEqual(result, 2);
        }

        /// <summary>
        /// Tests the empty source.
        /// </summary>
        [Test]
        public void TestEmptySource()
        {
            var source = string.Empty;
            var target = "hihi";
            var result = LevenshteinDistance.Calculate(source, target);

            Assert.AreEqual(result, target.Length);
        }

        /// <summary>
        /// Tests the empty target.
        /// </summary>
        [Test]
        public void TestEmptyTarget()
        {
            var source = "haha";
            var target = string.Empty;
            var result = LevenshteinDistance.Calculate(source, target);

            Assert.AreEqual(result, source.Length);
        }
    }
}
