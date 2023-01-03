using AuctionBackend.DomainLayer.DomainModel;
using System.Collections.Generic;

namespace AuctionBackend.DomainLayer.ServiceLayer.Interfaces
{
    public interface ICategoryService : IService<Category>
    {
        IEnumerable<Category> GetCategoriesWithProducts();
        IEnumerable<Category> GetCategoriesWithChildren();
    }
}
