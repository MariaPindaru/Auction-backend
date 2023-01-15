namespace AuctionBackend.DomainLayer.ServiceLayer.Impl
{
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.DomainModel.RepositoryInterfaces;
    using AuctionBackend.DomainLayer.DomainModel.Validators;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;

    /// <summary>
    /// UserSuspensionService.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Impl.BaseService&lt;AuctionBackend.DomainLayer.DomainModel.UserSuspension, AuctionBackend.DomainLayer.DomainModel.RepositoryInterfaces.IUserSuspensionRepository&gt;" />
    /// <seealso cref="AuctionBackend.DomainLayer.ServiceLayer.Interfaces.IUserSuspensionService" />
    internal class UserSuspensionService : BaseService<UserSuspension, IUserSuspensionRepository>, IUserSuspensionService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSuspensionService"/> class.
        /// </summary>
        public UserSuspensionService()
      : base(Injector.Get<IUserSuspensionRepository>(), new UserSuspensionValidator())
        {
        }
    }
}
