// <copyright file="Bindings.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.Startup
{
    using AuctionBackend.DataLayer.DataAccessLayer.Impl;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.Config;
    using AuctionBackend.DomainLayer.ServiceLayer.Impl;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using Ninject.Modules;

    /// <summary>
    /// Bindings.
    /// </summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
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
            this.Bind<IUserService>().To<UserService>();
            this.Bind<IUserScoreService>().To<UserScoreService>();
            this.Bind<IAuctionService>().To<AuctionService>();
            this.Bind<IBidService>().To<BidService>();

            this.Bind<IConfiguration>().To<Configuration>();
        }

        /// <summary>
        /// Loads the repository layer.
        /// </summary>
        private void LoadRepositoryLayer()
        {
            this.Bind<ICategoryRepository>().To<CategoryRepository>();
            this.Bind<IProductRepository>().To<ProductRepository>();
            this.Bind<IUserRepository>().To<UserRepository>();
            this.Bind<IUserScoreRepository>().To<UserScoreRepository>();
            this.Bind<IAuctionRepository>().To<AuctionRepository>();
            this.Bind<IBidRepository>().To<BidRepository>();
        }
    }
}
