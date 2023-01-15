// <copyright file="Configuration.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.Config
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// Configuration.
    /// </summary>
    /// <seealso cref="AuctionBackend.DomainLayer.Config.IConfiguration" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
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
                string value;
                this.GetFileContent().TryGetValue("MaxActiveAuctions", out value);
                return int.Parse(value);
            }
        }

        /// <summary>
        /// Gets the default score.
        /// </summary>
        /// <value>
        /// The default score.
        /// </value>
        public int DefaultScore
        {
            get
            {
                string value;
                this.GetFileContent().TryGetValue("DefaultScore", out value);
                return int.Parse(value);

            }
        }

        private Dictionary<string, string> GetFileContent()
        {
            string filePath = ConfigurationManager.AppSettings.Get("ConfigurationFilePath");
            var text = File.ReadAllText(filePath);
            var valueKeyDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);

            return valueKeyDict;
        }
    }
}
