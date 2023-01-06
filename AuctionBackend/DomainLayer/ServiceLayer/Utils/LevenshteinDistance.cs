﻿// <copyright file="LevenshteinDistance.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Utils
{
    using System;

    /// <summary>
    /// LevenshteinDistance.
    /// </summary>
    public class LevenshteinDistance
    {
        /// <summary>
        /// Calculates the levenshtein distance between the two given sources.
        /// </summary>
        /// <param name="source1">The source1.</param>
        /// <param name="source2">The source2.</param>
        /// <returns> The levenshtein distance. </returns>
        public static int Calculate(string source1, string source2)
        {
            var source1Length = source1.Length;
            var source2Length = source2.Length;

            var matrix = new int[source1Length + 1, source2Length + 1];

            // First calculation, if one entry is empty return full length
            if (source1Length == 0)
            {
                return source2Length;
            }

            if (source2Length == 0)
            {
                return source1Length;
            }

            // Initialization of matrix with row size source1Length and columns size source2Length
            for (var i = 0; i <= source1Length; matrix[i, 0] = i++)
            {
            }

            for (var j = 0; j <= source2Length; matrix[0, j] = j++)
            {
            }

            // Calculate rows and collumns distances
            for (var i = 1; i <= source1Length; i++)
            {
                for (var j = 1; j <= source2Length; j++)
                {
                    var cost = (source2[j - 1] == source1[i - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[source1Length, source2Length];
        }
    }
}
