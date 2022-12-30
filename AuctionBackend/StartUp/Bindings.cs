using AuctionBackend.DataLayer.DAL;
using AuctionBackend.DataLayer.DAL.Interfaces;
using AuctionBackend.DomainLayer.BL;
using AuctionBackend.DomainLayer.BL.Interfaces;
using Ninject.Modules;

namespace AuctionBackend.Startup
{
    class Bindings : NinjectModule
    {
        public override void Load()
        {
            // Here I should have a switch to change impl (Mocks / Real)

            LoadRepositoryLayer();
            LoadServicesLayer();
        }

        private void LoadServicesLayer()
        {
            Bind<ICategoryService>().To<CategoryService>();
        }

        private void LoadRepositoryLayer()
        {
            Bind<ICategoryRepository>().To<CategoryRepository>();
        }
    }
}
