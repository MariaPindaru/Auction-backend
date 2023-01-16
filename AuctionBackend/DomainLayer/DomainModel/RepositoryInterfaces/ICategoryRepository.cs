// <copyright file="ICategoryRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Interfaces
{
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// Interface used for defining the functionalities of a repository for the entity Category.
    /// </summary>
    /// <seealso cref="IRepository&lt;Category&gt;" />
    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
