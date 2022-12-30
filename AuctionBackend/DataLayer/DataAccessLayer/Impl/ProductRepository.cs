using AuctionBackend.DataLayer.DAL.Interfaces;
using AuctionBackend.DomainLayer.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionBackend.DataLayer.DAL
{
    class ProductRepository: BaseRepository<Product>, IProductRepository
    {
    }
}
