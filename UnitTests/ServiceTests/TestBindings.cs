// <copyright file="TestBindings.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace UnitTests.ServiceTests
{
    using AuctionBackend.DataLayer.DataAccessLayer.Interfaces;
    using AuctionBackend.DomainLayer.Config;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using Ninject.Modules;

    /// <summary>
    /// TestBindings.
    /// </summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    class TestBindings : NinjectModule
    {
        public override void Load()
        {
        }
    }
}
