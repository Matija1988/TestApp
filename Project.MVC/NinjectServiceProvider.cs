using Ninject.Modules;
using Ninject;

namespace Project.MVC
{
    public class NinjectServiceProvider : IServiceProvider
    {
        private readonly IKernel _kernel;

        public NinjectServiceProvider(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
    }

}
