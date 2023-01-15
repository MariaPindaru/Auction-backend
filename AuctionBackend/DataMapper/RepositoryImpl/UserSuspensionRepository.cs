// <copyright file="UserSuspensionRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataMapper.RepositoryImpl
{
    using System.Diagnostics.CodeAnalysis;
    using AuctionBackend.DataLayer.DataAccessLayer.Impl;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.RepositoryInterfaces;

    /// <summary>
    /// UserSuspensionRepository.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Impl.BaseRepository&lt;AuctionBackend.DomainLayer.DomainModel.UserSuspension&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.DomainModel.RepositoryInterfaces.IUserSuspensionRepository" />
    [ExcludeFromCodeCoverage]
    internal class UserSuspensionRepository : BaseRepository<UserSuspension>, IUserSuspensionRepository
    {
    }
}
