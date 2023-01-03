using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;

using AuctionBackend.DomainLayer.DomainModel;
using AuctionBackend.DomainLayer.DomainModel.Validators;
using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
using AuctionBackend.Startup;
using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    class ProductService : BaseService<Product, IProductRepository>, IProductService
    {
        public ProductService()
        : base(Injector.Get<IProductRepository>(), new ProductValidator())
        {
        }
    }
}
