// <copyright file="Bindings.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.Startup
{
    using AuctionBackend.DataLayer.DataAccessLayer.Impl;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.ServiceLayer.Impl;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using Ninject.Modules;

    /// <summary>
    /// Bindings.
    /// </summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    public class Bindings : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.LoadRepositoryLayer();
            this.LoadServicesLayer();
        }

        /// <summary>
        /// Loads the services layer.
        /// </summary>
        private void LoadServicesLayer()
        {
            this.Bind<ICategoryService>().To<CategoryService>();
            this.Bind<IProductService>().To<ProductService>();
        }

        /// <summary>
        /// Loads the repository layer.
        /// </summary>
        private void LoadRepositoryLayer()
        {
            this.Bind<ICategoryRepository>().To<CategoryRepository>();
            this.Bind<IProductRepository>().To<ProductRepository>();
        }
    }
}
