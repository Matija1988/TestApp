using Ninject;
using Ninject.Modules;

namespace Project.MVC
{
    public class NinjectServiceProviderFactory : IServiceProviderFactory<IKernel>
    {
        private readonly IKernel _kernel;

        public NinjectServiceProviderFactory() : this(new StandardKernel()) { }

        public NinjectServiceProviderFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IKernel CreateBuilder(IServiceCollection services)
        {
            _kernel.Bind<IServiceCollection>().ToConstant(services);
            return _kernel;
        }

        public IServiceProvider CreateServiceProvider(IKernel containerBuilder)
        {
            containerBuilder.Bind<IServiceProvider>().ToMethod(ctx => new NinjectServiceProvider(containerBuilder));
            return containerBuilder.Get<IServiceProvider>();
        }
    }
}
