//using Microsoft.EntityFrameworkCore;
//using Ninject.Modules;
//using Ninject;
//using Project.Service.Data;

//namespace Project.MVC
//{
//    public class Class
//    {
//        // Assuming this is the namespace of your service library

//public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddControllersWithViews();
//            services.AddEndpointsApiExplorer();
//            services.AddSwaggerGen();
//            services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(Configuration.GetConnectionString("ApplicationContext")));
//        }

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//                app.UseSwagger();
//                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseStaticFiles();
//            app.UseRouting();
//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllerRoute(
//                    name: "default",
//                    pattern: "{controller=Home}/{action=Index}/{id?}");
//            });
//        }


//    }

//}
//}

//using Microsoft.Extensions.DependencyInjection;
//using Ninject;
//using System;

//public class NinjectServiceProviderFactory : IServiceProviderFactory<IKernel>
//{
//    private readonly IKernel _kernel;

//    public NinjectServiceProviderFactory() : this(new StandardKernel()) { }

//    public NinjectServiceProviderFactory(IKernel kernel)
//    {
//        _kernel = kernel;
//    }

//    public IKernel CreateBuilder(IServiceCollection services)
//    {
//        _kernel.Bind<IServiceCollection>().ToConstant(services);
//        return _kernel;
//    }

//    public IServiceProvider CreateServiceProvider(IKernel containerBuilder)
//    {
//        containerBuilder.Bind<IServiceProvider>().ToMethod(ctx => new NinjectServiceProvider(containerBuilder));
//        return containerBuilder.Get<IServiceProvider>();
//    }
//}

//public class NinjectServiceProvider : IServiceProvider
//{
//    private readonly IKernel _kernel;

//    public NinjectServiceProvider(IKernel kernel)
//    {
//        _kernel = kernel;
//    }

//    public object GetService(Type serviceType)
//    {
//        return _kernel.TryGet(serviceType);
//    } 
//}

