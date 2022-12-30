using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionBackend.DataLayer.DAL.Interfaces;
using AuctionBackend.DomainLayer.BL;
using AuctionBackend.DomainLayer.BL.Interfaces;
using AuctionBackend.DomainLayer.DomainModel;
using AuctionBackend.DomainLayer.DomainModel.Validators;
using AuctionBackend.Startup;

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
