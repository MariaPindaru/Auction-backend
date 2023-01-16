// <copyright file="LevenshteinDistance.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Utils
{
    using System;

    /// <summary>
    /// Class that contains the algorithm for the levenshtein distance.
    /// </summary>
    public class LevenshteinDistance
    {
        /// <summary>
        /// Calculates the levenshtein distance between the two given sources.
        /// </summary>
        /// <param name="firstSource">The first source.</param>
        /// <param name="secondSource">The second source.</param>
        /// <returns> The levenshtein distance. </returns>
        public static int Calculate(string firstSource, string secondSource)
        {
            var firstSourceLength = firstSource.Length;
            var secondSourceLength = secondSource.Length;

            var matrix = new int[firstSourceLength + 1, secondSourceLength + 1];

            if (firstSourceLength == 0)
            {
                return secondSourceLength;
            }

            if (secondSourceLength == 0)
            {
                return firstSourceLength;
            }

            for (var i = 0; i <= firstSourceLength; matrix[i, 0] = i++)
            {
            }

            for (var j = 0; j <= secondSourceLength; matrix[0, j] = j++)
            {
            }

            for (var i = 1; i <= firstSourceLength; i++)
            {
                for (var j = 1; j <= secondSourceLength; j++)
                {
                    var cost = (secondSource[j - 1] == firstSource[i - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[firstSourceLength, secondSourceLength];
        }
    }
}
