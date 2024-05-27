//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Project.Service.Data;
//using Project.Service.Mapping;
//using Project.Service.Model;
//using Project.Service.Repository;
//using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

using Ninject.Web.AspNetCore.Hosting;
using Ninject.Web.AspNetCore;
using Project.Service.Data;
using Project.Service.Mapping;
using Project.Service.Repository;
using Project.Service.Model;
using Microsoft.EntityFrameworkCore;

public class Startup : AspNetCoreStartupBase
{
    public Startup(IConfiguration configuration, IServiceProviderFactory<NinjectServiceProviderBuilder> providerFactory)
        : base(providerFactory)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddDbContext<ApplicationDBContext>
            (options => options.UseSqlServer(Configuration.GetConnectionString("ApplicationContext")));

        services.AddScoped<IMapping, MapperConfig>();

        services.AddScoped<IVehicleService
            <VehicleMake,
            VehicleMakeDTORead,
            VehicleMakeDTOInsert,
            VehicleMakeDTOReadWithoutID>, VehicleMakeService>();

        services.AddScoped<IVehicleService
            <VehicleModel,
            VehicleModelDTORead,
            VehicleModelDTOInsert,
            VehicleModelDTOReadWithoutID>, VehicleModelService>();


        // Add your services configuration HERE
    }

    public override void Configure(IApplicationBuilder app)
    {
        // For simplicitly, there is only one overload of Configure supported, so in order to get the additional
        // services, you can just resolve them with the service provider.
        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        
        // Add your application builder configuration HERE
    }
}



//public class Startup
//{
//    public Startup(IConfiguration configuration)
//    {
//        Configuration = configuration;
//    }

//    public IConfiguration Configuration { get; }
//    // This method gets called by the runtime. Use this method to add services to the container.

//    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
//    {
//        if (env.IsDevelopment())
//        {
//            app.UseDeveloperExceptionPage();
//        }
//        else
//        {
//            app.UseExceptionHandler("/Home/Error");
//        }

//        app.UseStaticFiles();
//        app.UseSession();

//    }
//}