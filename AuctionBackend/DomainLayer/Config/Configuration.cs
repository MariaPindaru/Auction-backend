// <copyright file="Configuration.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.Config
{
    using System.Configuration;

    /// <summary>
    /// Configuration.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.Config.IConfiguration" />
    internal class Configuration : IConfiguration
    {
        /// <summary>
        /// Gets the maximum active auctions.
        /// </summary>
        /// <value>
        /// The maximum active auctions.
        /// </value>
        public int MaxActiveAuctions
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["MaxActiveAuctions"]);
            }
        }
    }
}
