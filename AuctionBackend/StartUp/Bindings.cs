using AuctionBackend.DataLayer.DataAccessLayer.Impl;
using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
using AuctionBackend.DomainLayer.ServiceLayer.Impl;
using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
using Ninject.Modules;

namespace AuctionBackend.Startup
{
    class Bindings : NinjectModule
    {
        public override void Load()
        {
            LoadRepositoryLayer();
            LoadServicesLayer();
        }

        private void LoadServicesLayer()
        {
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IProductService>().To<ProductService>();
        }

        private void LoadRepositoryLayer()
        {
            Bind<ICategoryRepository>().To<CategoryRepository>();
            Bind<IProductRepository>().To<ProductRepository>();
        }
    }
}
