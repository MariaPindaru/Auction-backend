
namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using System;
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using FluentValidation.Results;

    /// <summary>
    /// UserScoreService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.UserScore, AuctionBackend.DataLayer.DataAccessLayer.Interfaces.IUserScoreRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IUserScoreService" />
    internal class UserScoreService : BaseService<UserScore, IUserScoreRepository>, IUserScoreService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserScoreService"/> class.
        /// </summary>
        public UserScoreService()
            : base(Injector.Get<IUserScoreRepository>(), new UserScoreValidator())
        {
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
            entity.Date = DateTime.Now;
            return base.Insert(entity);
        }
    }
}
