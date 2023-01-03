// <copyright file="AppDbContext.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer
{
    using System.Data.Entity;
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>Class used to declare the application db context.</summary>
    internal class AppDbContext : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="AppDbContext" /> class.</summary>
        public AppDbContext()
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

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the auctions.
        /// </summary>
        /// <value>
        /// The auctions.
        /// </value>
        public virtual DbSet<Auction> Auctions { get; set; }

        /// <summary>
        /// Gets or sets the bids.
        /// </summary>
        /// <value>
        /// The bids.
        /// </value>
        public virtual DbSet<Bid> Bids { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuilder, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>()
            //    .HasMany(p => p.Children)
            //    .WithMany()
            //    .Map(m =>
            //    {
            //        m.MapLeftKey("CategoryParentId");
            //        m.MapRightKey("CategoryChildId");
            //        m.ToTable("CategoryChildCategory");
            //    });
        }
    }
}
