// <copyright file="Program.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend
{
    using System.Diagnostics.CodeAnalysis;
    using AuctionBackend.Startup;

    /// <summary>
    /// Main program.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            Injector.Inject();
            var kernel = Injector.Kernel;
        }
    }
}
