using Ninject;
using System;
using System.Reflection;


namespace AuctionBackend.Startup
{
    public class Injector
    {
        private static IKernel _kernel;
        public static IKernel Kernel
        {
            get
            {
                if (_kernel == null)
                {
                    throw new ArgumentNullException("Injection method should be called first!");
                }

                return _kernel;
            }
        }

        public static void Inject()
        {
            _kernel = new StandardKernel(new Bindings());
            _kernel.Load(Assembly.GetExecutingAssembly());
        }

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
