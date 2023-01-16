// <copyright file="IUserSuspensionRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.DomainModel.RepositoryInterfaces
{
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;

    /// <summary>
    /// Interface used for defining the functionalities of a repository for the entity UserSuspension.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IRepository&lt;AuctionBackend.DomainLayer.DomainModel.UserSuspension&gt;" />
    public interface IUserSuspensionRepository : IRepository<UserSuspension>
    {
    }
}
