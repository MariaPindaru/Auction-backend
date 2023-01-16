// <copyright file="IUserSuspensionService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Interfaces
{
    using AuctionBackend.DomainLayer.DomainModel;
    using FluentValidation.Results;

    /// <summary>
    /// Interface used for defining the functionalities for a service for the entity UserSuspension.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IService&lt;AuctionBackend.DomainLayer.DomainModel.UserSuspension&gt;" />
    public interface IUserSuspensionService : IService<UserSuspension>
    {
        /// <summary>
        /// Adds the suspension for user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns> The validation result on user. </returns>
        ValidationResult AddSuspensionForUser(User user);

        /// <summary>
        /// Users the is suspended.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns> true if the user is suspended, false otherwise. </returns>
        bool UserIsSuspended(User user);
    }
}
