// <copyright file="CategoryRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Impl
{
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// CategoryRepository.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Impl.BaseRepository&lt;AuctionBackend.DomainLayer.DomainModel.Category&gt;" />
    /// <seealso cref="AuctionBackend.DataLayer.DAL.Interfaces.ICategoryRepository" />
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
    }
}
