// <copyright file="UserScoreService.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.Config;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using FluentValidation.Results;

    /// <summary>
    /// Class that implements the functionalities for IUserScoreService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.UserScore, AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IUserScoreRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IUserScoreService" />
    internal class UserScoreService : BaseService<UserScore, IUserScoreRepository>, IUserScoreService
    {
        /// <summary>
        /// The user service.
        /// </summary>
        private IUserService userService;

        /// <summary>
        /// The user suspension service.
        /// </summary>
        private IUserSuspensionService usersuspensionService;

        /// <summary>
        /// The application configuration.
        /// </summary>
        private IConfiguration appConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserScoreService"/> class.
        /// </summary>
        public UserScoreService()
            : base(Injector.Get<IUserScoreRepository>(), new UserScoreValidator())
        {
            this.userService = Injector.Get<IUserService>();
            this.usersuspensionService = Injector.Get<IUserSuspensionService>();
            this.appConfiguration = Injector.Get<IConfiguration>();
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The validation result.
        /// </returns>
        public override ValidationResult Insert(UserScore entity)
        {
            ValidationResult validationResult = base.Insert(entity);

            if (validationResult.IsValid)
            {
                var scoredUser = entity.ScoredUser;
                var score = this.userService.GetSeriousnessScore(scoredUser.Id);
                if (score.HasValue && score.Value < this.appConfiguration.MinimumScore)
                {
                    this.usersuspensionService.AddSuspensionForUser(scoredUser);
                }
            }

            return validationResult;
        }
    }
}
