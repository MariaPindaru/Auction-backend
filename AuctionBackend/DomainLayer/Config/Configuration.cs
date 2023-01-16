// <copyright file="Configuration.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.DomainLayer.Config
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// Class that implements the functionality for IConfiguration.
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
                this.GetFileContent().TryGetValue("MaxActiveAuctions", out string value);
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
                this.GetFileContent().TryGetValue("DefaultScore", out string value);
                return int.Parse(value);
            }
        }

        /// <summary>
        /// Gets the minimum score.
        /// </summary>
        /// <value>
        /// The minimum score.
        /// </value>
        public int MinimumScore
        {
            get
            {
                this.GetFileContent().TryGetValue("MinimumScore", out string value);
                return int.Parse(value);
            }
        }

        /// <summary>
        /// Gets the suspension days.
        /// </summary>
        /// <value>
        /// The suspension days.
        /// </value>
        public int SuspensionDays
        {
            get
            {
                this.GetFileContent().TryGetValue("SuspensionDays", out string value);
                return int.Parse(value);
            }
        }

        /// <summary>
        /// Gets the content of the configuration file.
        /// </summary>
        /// <returns> The deserialized configuration file as dictionary. </returns>
        private Dictionary<string, string> GetFileContent()
        {
            string filePath = ConfigurationManager.AppSettings.Get("ConfigurationFilePath");
            var text = File.ReadAllText(filePath);
            var valueKeyDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);

            return valueKeyDict;
        }
    }
}
