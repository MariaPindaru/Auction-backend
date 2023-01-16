// <copyright file="UserSuspensionService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using System;
    using System.Linq;
    using AuctionBackend.DomainLayer.Config;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.RepositoryInterfaces;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using FluentValidation.Results;

    /// <summary>
    /// Class that implements the functionalities for IUserSuspensionService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.UserSuspension, AuctionBackend.DomainLayer.DomainModel.RepositoryInterfaces.IUserSuspensionRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IUserSuspensionService" />
    internal class UserSuspensionService : BaseService<UserSuspension, IUserSuspensionRepository>, IUserSuspensionService
    {
        /// <summary>
        /// The application configuration.
        /// </summary>
        private IConfiguration appConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSuspensionService"/> class.
        /// </summary>
        public UserSuspensionService()
      : base(Injector.Get<IUserSuspensionRepository>(), new UserSuspensionValidator())
        {
            this.appConfiguration = Injector.Get<IConfiguration>();
        }

        /// <summary>
        /// Adds the suspension for user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The validation result on user.
        /// </returns>
        public ValidationResult AddSuspensionForUser(User user)
        {
            var suspension = new UserSuspension
            {
                User = user,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(this.appConfiguration.SuspensionDays),
            };

            return this.Insert(suspension);
        }

        /// <summary>
        /// Users the is suspended.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// true if the user is suspended, false otherwise.
        /// </returns>
        public bool UserIsSuspended(User user)
        {
            if (user != null)
            {
                var userActiveSuspensions = this.Repository.Get(
                    filter: s => s.User.Id == user.Id && s.EndDate < DateTime.Now);

                if (userActiveSuspensions.Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
