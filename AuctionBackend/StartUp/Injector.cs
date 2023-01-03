// <copyright file="Injector.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend.Startup
{
    using System;
    using System.Reflection;
    using Ninject;

    /// <summary>
    /// Injector.
    /// </summary>
    public class Injector
    {
        private static IKernel kernel;

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        /// <value>
        /// The kernel.
        /// </value>
        /// <exception cref="System.ArgumentNullException">Injection method should be called first.</exception>
        public static IKernel Kernel
        {
            get
            {
                if (kernel == null)
                {
                    throw new ArgumentNullException("Injection method should be called first!");
                }

                return kernel;
            }
        }

        /// <summary>
        /// Injects this instance.
        /// </summary>
        public static void Inject()
        {
            kernel = new StandardKernel(new Bindings());

            // kernel.Load(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"> Type of entity. </typeparam>
        /// <returns> The entity. </returns>
        public static T Get<T>()
        {
            return kernel.Get<T>();
        }
    }
}
