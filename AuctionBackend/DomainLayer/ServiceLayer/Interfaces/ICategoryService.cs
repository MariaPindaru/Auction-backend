// <copyright file="ICategoryService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Interfaces
{
    using System.Collections.Generic;
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// ICategoryService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IService&lt;AuctionBackend.DomainLayer.DomainModel.Category&gt;" />
    public interface ICategoryService : IService<Category>
    {
    }
}
