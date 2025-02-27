﻿// <copyright file="UserService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using System;
    using System.Linq;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.Config;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;

    /// <summary>
    /// Class that implements the functionalities for IUserService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.User, AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IUserRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IUserService" />
    internal class UserService : BaseService<User, IUserRepository>, IUserService
    {
        /// <summary>
        /// The application configuration.
        /// </summary>
        private IConfiguration appConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService()
        : base(Injector.Get<IUserRepository>(), new UserValidator())
        {
            this.appConfiguration = Injector.Get<IConfiguration>();
        }

        /// <summary>
        /// Gets the seriousness score.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// User's seriousness score.
        /// </returns>
        public int? GetSeriousnessScore(int userId)
        {
            var user = this.Repository.Get(
                filter: u => u.Id == userId,
                includeProperties: "UserScore").FirstOrDefault();

            if (user == null)
            {
                Logger.Error($"The user could not be found in the database.");
                return null;
            }
            else if (user.ReceivedUserScores.Count > 0)
            {
                var scores = user.ReceivedUserScores.Where(score => score.Date > DateTime.Now.AddMonths(-3));

                if (scores.Count() != 0)
                {
                    scores = scores.OrderBy(s => s.Score);
                    var scoresCount = scores.Count();
                    if (scoresCount % 2 == 1)
                    {
                        var middleIndex = scoresCount / 2;
                        return scores.ElementAt(middleIndex).Score;
                    }
                    else
                    {
                        var middleIndex = scoresCount / 2;
                        return (scores.ElementAt(middleIndex - 1).Score + scores.ElementAt(middleIndex).Score) / 2;
                    }
                }
            }

            return this.appConfiguration.DefaultScore;
        }
    }
}
