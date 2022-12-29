﻿// <copyright file="AppContext.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace Auction.DataLayer
{
    using System.Data.Entity;
    using Auction.DomainLayer.DomainModel;

    /// <summary>Class used to declare the application db context.</summary>
    internal class AppContext : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="AppContext" /> class.</summary>
        public AppContext()
            : base("MyConnectionString")
        {
        }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public virtual DbSet<Category> Categories { get; set; }
    }
}
