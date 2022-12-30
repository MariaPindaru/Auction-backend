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
    class UserService : BaseService<User, IUserRepository>, IUserService
    {
        public UserService()
        : base(Injector.Get<IUserRepository>(), new UserValidator())
        {
        }
    }
}
