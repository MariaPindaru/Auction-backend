// <copyright file="IUserService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Interfaces
{
    using AuctionBackend.DomainLayer.DomainModel;

    /// <summary>
    /// Interface used for defining the functionalities for a service for the entity User.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IService&lt;AuctionBackend.DomainLayer.DomainModel.User&gt;" />
    public interface IUserService : IService<User>
    {
        /// <summary>
        /// Gets the seriousness score.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns> User's seriousness score. </returns>
        int? GetSeriousnessScore(int userId);
    }
}
