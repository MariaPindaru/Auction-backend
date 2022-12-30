using AuctionBackend.DataLayer.DAL.Interfaces;
using AuctionBackend.DomainLayer.BL.Interfaces;
using AuctionBackend.DomainLayer.DomainModel;
using AuctionBackend.DomainLayer.DomainModel.Validators;
using AuctionBackend.Startup;
using System.Collections.Generic;

namespace AuctionBackend.DomainLayer.BL
{
    public class CategoryService : BaseService<Category, ICategoryRepository>, ICategoryService
    {
        public CategoryService()
            : base(Injector.Get<ICategoryRepository>(), new CategoryValidator())
        {
        }

        public IEnumerable<Category> GetCategoriesWithProducts()
        {
            return _repository.Get(
                filter: category => category.Products.Count > 0,
                includeProperties: "Products");
        }
    }
}
