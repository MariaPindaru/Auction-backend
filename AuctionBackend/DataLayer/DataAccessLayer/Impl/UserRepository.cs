// <copyright file="UserRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Impl
{
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// UserRepository.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Impl.BaseRepository&lt;AuctionBackend.DomainLayer.DomainModel.User&gt;" />
    /// <seealso cref="AuctionBackend.DataLayer.DAL.Interfaces.IUserRepository" />
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
    }
}
