// <copyright file="IUserService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Interfaces
{
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// IUserService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IService&lt;AuctionBackend.DomainLayer.DomainModel.User&gt;" />
    public interface IUserService : IService<User>
    {
        /// <summary>
        /// Gets the seriosity score.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns> User's seriosity score. </returns>
        int? GetSeriosityScore(int userId);
    }
}
