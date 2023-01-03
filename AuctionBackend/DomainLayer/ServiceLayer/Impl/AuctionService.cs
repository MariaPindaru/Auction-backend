using AuctionBackend.DomainLayer.DomainModel;
using AuctionBackend.DomainLayer.DomainModel.Validators;
using AuctionBackend.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    public class AuctionService : BaseService<Auction, IAuctionRepository>, IAuctionService
    {
        public AuctionService()
        : base(Injector.Get<IAuctionRepository>(), new AuctionValidator())
        {
        }
    }
}
