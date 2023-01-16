// <copyright file="IUserScoreRepository.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DataLayer.DataAccessLayer.Interfaces
{
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// Interface used for defining the functionalities of a repository for the entity UserScore.
    /// </summary>
    /// <seealso cref="AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IRepository&lt;AuctionBackend.DomainLayer.DomainModel.UserScore&gt;" />
    public interface IUserScoreRepository : IRepository<UserScore>
    {
    }
}
