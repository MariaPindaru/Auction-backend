using AuctionBackend.DomainLayer.DomainModel;
using System.Collections.Generic;

namespace AuctionBackend.DomainLayer.BL.Interfaces
{
    public interface ICategoryService : IService<Category>
    {
        IEnumerable<Category> GetCategoriesWithProducts();
    }
}
