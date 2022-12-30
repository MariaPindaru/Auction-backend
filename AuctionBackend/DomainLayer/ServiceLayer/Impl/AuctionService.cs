using AuctionBackend.DataLayer.DAL.Interfaces;
using AuctionBackend.DomainLayer.BL;
using AuctionBackend.DomainLayer.BL.Interfaces;
using AuctionBackend.DomainLayer.DomainModel;
using AuctionBackend.DomainLayer.DomainModel.Validators;
using AuctionBackend.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    class AuctionService : BaseService<Auction, IAuctionRepository>, IAuctionService
    {
        public AuctionService()
        : base(Injector.Get<IAuctionRepository>(), new AuctionValidator())
        {
        }
    }
}
