// <copyright file="UserScoreRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Impl
{
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// Class that implements the functionality for IUserScoreRepository.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Impl.BaseRepository&lt;AuctionBackend.DomainLayer.DomainModel.UserScore&gt;" />
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IUserScoreRepository" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal class UserScoreRepository : BaseRepository<UserScore>, IUserScoreRepository
    {
    }
}
