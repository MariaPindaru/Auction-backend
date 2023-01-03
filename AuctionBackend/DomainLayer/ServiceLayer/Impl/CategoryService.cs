// <copyright file="CategoryService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using System.Collections.Generic;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using AuctionBackend.Startup;
    using log4net;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;

    /// <summary>
    /// CategoryService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.Category, AuctionBackend.DataLayer.DataAccessLayer.Interfaces.ICategoryRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.ICategoryService" />
    public class CategoryService : BaseService<Category, ICategoryRepository>, ICategoryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        public CategoryService()
            : base(Injector.Get<ICategoryRepository>(), new CategoryValidator())
        {
        }

        /// <summary>
        /// Gets the categories with products.
        /// </summary>
        /// <returns>Categories with products.</returns>
        public IEnumerable<Category> GetCategoriesWithChildren()
        {
            return this.repository.Get(
                filter: category => category.Children.Count > 0,
                includeProperties: "Children");
        }

        /// <summary>
        /// Gets the categories with products.
        /// </summary>
        /// <returns>Categories with products.</returns>
        public IEnumerable<Category> GetCategoriesWithProducts()
        {
            return this.repository.Get(
                filter: category => category.Products.Count > 0,
                includeProperties: "Products");
        }
    }
}
